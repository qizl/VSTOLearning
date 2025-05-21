using AntdUI;
using MyExcelRibbon.Helper;
using MyExcelRibbon.Models;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyExcelRibbon.Views
{
    public partial class FormExcel2PDF : AntdUI.Window
    {
        #region Members
        private static FormExcel2PDF handler = null;
        private static bool isShow = false;

        private AntList<ExcelFile> _excelFiles;
        private AntList<ExcelFile> _excelFilesError;
        private ExcelFile _currentFile;

        private CancellationTokenSource _convertCancellationTokenSource = new CancellationTokenSource();
        private TaskCompletionSource<bool> _taskCompletionSource;
        private TaskCompletionSource<bool> _convertTaskCompletionSource;
        #endregion

        #region Methods - Public
        public FormExcel2PDF()
        {
            InitializeComponent();

            this.initialize();
        }

        public static void ShowWindow()
        {
            if (isShow)
            {
                if (handler.WindowState == FormWindowState.Minimized)
                {
                    handler.WindowState = FormWindowState.Normal;
                }
                handler.Activate();
                handler.Location = new Point(
                    (Screen.PrimaryScreen.WorkingArea.Width - handler.Width) / 2,
                    (Screen.PrimaryScreen.WorkingArea.Height - handler.Height) / 2
                );
                return;
            }

            handler = new FormExcel2PDF();
            handler.Show();
            isShow = true;
        }

        public void SetTaskRuningStatus()
        {
            this._taskCompletionSource = new TaskCompletionSource<bool>();
        }
        public void SetTaskCompletedStatus()
        {
            this._taskCompletionSource.SetResult(true);
        }
        public bool IsTaskRunning()
        {
            if (this._taskCompletionSource != null && !this._taskCompletionSource.Task.IsCompleted)
            {
                AntdUI.Modal.open(new AntdUI.Modal.Config(this, "警告", "当前有任务正在执行，请等待任务完成后再操作！")
                {
                    Icon = TType.Warn,
                    //内边距
                    Padding = new Size(24, 20),
                });
                return true;
            }

            return false;
        }
        #endregion

        #region Methods - Private
        private void initialize()
        {
            this.btnStartConvert.Enabled = false;
            this.btnStopConvert.Enabled = false;

            this.initTableColumns();

            this.bindEventHandler();

            this.updateStep();
        }

        private void bindEventHandler()
        {
            //1.7.14开始,uploadDragger自带点击打开文件选择框
            uploadDragger.DragChanged += uploadDragger_DragChanged;
            uploadDragger.Multiselect = true;//允许多选文件
            uploadDragger.Filter = "Excel 文件 (*.xls;*.xlsx)|*.xls;*.xlsx";//文件筛选
            uploadDragger.HandDragFolder = true;//是否支持拖拽，默认为true

            excelTable.CellButtonClick += excelTable_CellButtonClick;
        }

        private void updateStep(int index = 0, TStepState status = TStepState.Process)
        {
            steps.Current = index;
            steps.Status = status;
        }
        #endregion

        #region Methods - 选择文件
        private void txtFolder_DoubleClick(object sender, EventArgs e)
        {
            this.btnChooseFolder_Click(sender, e);
        }

        private void btnChooseFolder_Click(object sender, EventArgs e)
        {
            var dialog = new AntdUI.FolderBrowserDialog();
            //dialog.Description = "请选择Excel文件所在路径";
            //dialog.ShowNewFolderButton = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtFolder.Text = dialog.DirectoryPath;

                this.btnLoadFiles_Click(sender, e);
            }
        }

        private void btnLoadFiles_Click(object sender, EventArgs e)
        {
            string[] files = null;

            if (string.IsNullOrEmpty(this.txtFolder.Text))
            {
                AntdUI.Modal.open(new AntdUI.Modal.Config(this, "选择文件", "请选择Excel文件所在路径！")
                {
                    Icon = TType.Warn,
                    //内边距
                    Padding = new Size(24, 20),
                });
            }
            else if (!System.IO.Directory.Exists(this.txtFolder.Text))
            {
                AntdUI.Modal.open(new AntdUI.Modal.Config(this, "选择文件", "无效的路径！")
                {
                    Icon = TType.Error,
                    //内边距
                    Padding = new Size(24, 20),
                });
            }
            else
            {
                var folderPath = this.txtFolder.Text;
                var excelExtensions = new[] { ".xls", ".xlsx" };
                files = Directory.GetFiles(folderPath)
                    .Where(f => excelExtensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
                    .ToArray();

                if (files.Length == 0)
                {
                    AntdUI.Modal.open(new AntdUI.Modal.Config(this, "选择文件", "未读取到文件！")
                    {
                        Icon = TType.Error,
                        //内边距
                        Padding = new Size(24, 20),
                    });
                }
            }

            this.loadFiles(files);
        }

        private void uploadDragger_DragChanged(object sender, StringsEventArgs e)
        {
            this.loadFiles(e.Value);
        }
        #endregion

        #region Methods - 文件转换
        private void btnStartConvert_Click(object sender, EventArgs e)
        {
            Task.Run(this.startConvertAsync);
        }
        private async Task startConvertAsync()
        {
            this.SetTaskRuningStatus();

            this._convertTaskCompletionSource = new TaskCompletionSource<bool>();

            // 1.更新启动按钮进度
            this.Invoke(() =>
            {
                this.updateStep(1, TStepState.Process);
                this.plChooseFiles.Enabled = false;

                this.btnStartConvert.Text = "正在转换";
                this.btnStartConvert.Loading = true;
                this.btnStartConvert.LoadingWaveValue = .1f;

                // 启用停止转换按钮
                this.btnStopConvert.Enabled = true;
            });

            void updateProgress(ExcelFile file)
            {
                var index = this._excelFiles.IndexOf(file);
                var total = this._excelFiles.Count;
                this.Invoke(() =>
                {
                    this.btnStartConvert.LoadingWaveValue = (float)index / total;
                    this.btnStartConvert.Text = $"正在转换{index + 1}/{total}";
                });
            }

            // 2.转换文件
            foreach (var file in this._excelFiles)
            {
                if (this._convertCancellationTokenSource.IsCancellationRequested)
                    break;

                var pdfPath = Path.Combine(Path.GetDirectoryName(file.Path), $"{Path.GetFileNameWithoutExtension(file.Path)}.pdf");
                if (PDFHelper.AsposeExcelToPdf(file.Path, pdfPath))
                {
                    file.PDFPath = pdfPath;
                }
                else
                {
                    if (this._excelFilesError == null)
                        this._excelFilesError = new AntList<ExcelFile>();
                    this._excelFilesError.Add(file);

                    this.Invoke(() =>
                    {
                        AntdUI.Notification.error(this, "文件转换", $"文件转换失败！{file.Path}", autoClose: 3, align: TAlignFrom.TR);
                    });
                }

                updateProgress(file);

                await Task.Delay(500); // 模拟延迟
            }

            // 3.转换完成
            if (this._convertCancellationTokenSource.IsCancellationRequested)
            {
                this.Invoke(() =>
                {
                    this.btnStartConvert.Loading = false;
                    //this.btnStartConvert.LoadingWaveValue = 0;
                    this.btnStartConvert.Text = "转换停止";
                    this.btnStartConvert.Enabled = false;
                });
            }
            else
            {
                this.Invoke(() =>
                {
                    this.btnStartConvert.LoadingWaveValue = 1;
                    this.btnStartConvert.Text = "转换完成";
                });
                await Task.Delay(500); // 模拟延迟
                this.Invoke(() =>
                {
                    this.btnStartConvert.Loading = false;
                    this.btnStartConvert.Enabled = false;
                    this.btnStartConvert.Text = "转换完成";

                    // 禁用停止转换按钮
                    this.btnStopConvert.Enabled = false;
                });

                this.SetTaskCompletedStatus();
                this.updateStep(2, TStepState.Finish);

                if (this._excelFilesError != null && this._excelFilesError.Any())
                {
                    var errorFiles = string.Join("，", this._excelFilesError.Select(m => m.Name));
                    this.Invoke(() =>
                    {
                        AntdUI.Modal.open(new AntdUI.Modal.Config(this, "文件转换", $"以下文件（{this._excelFilesError.Count}个）转换失败：{errorFiles}")
                        {
                            Icon = TType.Error,
                            //内边距
                            Padding = new Size(24, 20),
                        });
                    });
                }
            }
            this._convertTaskCompletionSource.TrySetResult(true);
        }

        private void btnStopConvert_Click(object sender, EventArgs e)
        {
            if (AntdUI.Modal.open(new AntdUI.Modal.Config(this, "文件转换", "确认停止文件转换？停止后将撤销所有文件修改。")
            {
                Icon = TType.Warn,
                //内边距
                Padding = new Size(24, 20),
            }) == DialogResult.OK)
            {
                Task.Run(this.stopConvertAsync);
            }
        }
        private async Task stopConvertAsync()
        {
            this.SetTaskRuningStatus();

            // 1.取消任务
            this._convertCancellationTokenSource.Cancel();

            // 2.等待任务完成
            if (this._convertTaskCompletionSource != null)
                await this._convertTaskCompletionSource.Task;

            // 3.更新UI
            this.Invoke(() =>
            {
                this.btnStopConvert.Text = "正在停止";
                this.btnStopConvert.Loading = true;
                this.btnStopConvert.LoadingWaveValue = .1f;
            });

            // 2.撤销文件修改
            var pdfFiles = this._excelFiles.Where(m => m.PDFPath != null).ToList();
            void updateProgress(ExcelFile file)
            {
                var index = pdfFiles.IndexOf(file);
                var total = pdfFiles.Count;
                this.Invoke(() =>
                {
                    this.btnStopConvert.LoadingWaveValue = (float)index / total;
                    this.btnStopConvert.Text = $"正在撤销{index + 1}/{total}";
                });
            }
            foreach (var file in pdfFiles)
            {
                if (File.Exists(file.PDFPath))
                {
                    File.Delete(file.PDFPath);
                }
                file.PDFPath = null;

                updateProgress(file);

                await Task.Delay(500); // 模拟延迟
            }

            // 3.停止完成
            this.Invoke(() =>
            {
                this.btnStopConvert.LoadingWaveValue = 1;
                this.btnStopConvert.Text = $"已撤销";
            });
            await Task.Delay(500); // 模拟延迟
            this.Invoke(() =>
            {
                this.btnStopConvert.Loading = false;
                this.btnStopConvert.Enabled = false;
                this.btnStopConvert.Text = $"已停止";
            });

            this.updateStep(2, TStepState.Error);
            this.SetTaskCompletedStatus();
        }

        private void excelTable_CellButtonClick(object sender, TableButtonEventArgs e)
        {
            var buttontext = e.Btn.Text;

            if (e.Record is ExcelFile file)
            {
                _currentFile = file;
                //AntdUI.Message.error(this, $"暂不支持...", autoClose: 1);
                AntdUI.Notification.warn(this, "文件操作", "暂不支持...", autoClose: 3, align: TAlignFrom.TR);
                //switch (buttontext)
                //{
                //    //暂不支持进入整行编辑，只支持指定单元格编辑，推荐使用弹窗或抽屉编辑整行数据
                //    case "开始":
                //        //var form = new UserEdit(this, file) { Size = new Size(500, 300) };
                //        //AntdUI.Drawer.open(new AntdUI.Drawer.Config(this, form)
                //        //{
                //        //    OnLoad = () =>
                //        //    {
                //        //        AntdUI.Message.info(this, "进入编辑", autoClose: 1);
                //        //    },
                //        //    OnClose = () =>
                //        //    {
                //        //        AntdUI.Message.info(this, "结束编辑", autoClose: 1);
                //        //    }
                //        //});
                //        break;
                //    case "停止":
                //        //var result = Modal.open(this, "删除警告！", "确认要删除选择的数据吗？", TType.Warn);
                //        //if (result == DialogResult.OK)
                //        //    excelFiles.Remove(file);
                //        //AntdUI.Message.info(this, file.CellLinks.FirstOrDefault().Id, autoClose: 1);
                //        break;
                //    case "删除":
                //        AntdUI.Message.info(this, $"暂不支持...", autoClose: 1);
                //        break;
                //}
            }
        }

        private void initTableColumns()
        {
            excelTable.Columns = new ColumnCollection() {
                new Column("Index", "序号", ColumnAlign.Center)
                {
                    Width="80",
                    Fixed = true,//冻结列
                },
                new Column("Name", "文件名", ColumnAlign.Left)
                {
                    Width="550"
                },
                new Column("CellProgress", "转换进度",ColumnAlign.Center)
                {
                    //Width="200"
                },
                new Column("CellLinks", "操作", ColumnAlign.Center)
                {
                    Width="200",
                    Fixed = true,//冻结列
                },
            };
        }

        private void loadFiles(string[] files)
        {
            // 清空原有数据
            if (_excelFiles is null)
                _excelFiles = new AntList<ExcelFile>();
            //else
            //    excelFiles.Clear();

            if (files is null || files.Length == 0)
            {

            }
            else
            {
                this.updateStep(0, TStepState.Finish);

                AntdUI.Notification.success(this, "选择文件", $"读取到文件{files.Length}个！", autoClose: 3, align: TAlignFrom.TR);

                // 添加新读取的Excel文件
                foreach (var file in files)
                {
                    var id = Guid.NewGuid().ToString();
                    if (!_excelFiles.Any(m => m.Path == file))
                    {
                        _excelFiles.Add(new ExcelFile
                        {
                            Id = id,
                            Path = file,
                            Index = _excelFiles.Count + 1,
                            Name = Path.GetFileName(file),
                            CellProgress = new CellProgress(0f) { Size = new Size { Width = 180, Height = 10 } },
                            CellLinks = new CellLink[] {
                                new CellButton(id,"开始",TTypeMini.Primary),
                                new CellButton(id,"停止", TTypeMini.Success),
                                new CellButton(id,"删除",TTypeMini.Error)
                            },
                        });
                    }
                }

                this.btnStartConvert.Enabled = true;
            }

            // 绑定到表格
            excelTable.Binding(_excelFiles);
        }
        #endregion

        #region Events
        private void FormExcel2PDF_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormExcel2PDF.isShow = false;
        }

        private void FormExcel2PDF_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.IsTaskRunning())
            {
                // 当前有任务正在运行，禁止关闭窗体
                e.Cancel = true;
                return;
            }
        }
        #endregion
    }
}
