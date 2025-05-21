namespace MyExcelRibbon.Views
{
    partial class FormExcel2PDF
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            AntdUI.StepsItem stepsItem1 = new AntdUI.StepsItem();
            AntdUI.StepsItem stepsItem2 = new AntdUI.StepsItem();
            AntdUI.StepsItem stepsItem3 = new AntdUI.StepsItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExcel2PDF));
            this.uploadDragger = new AntdUI.UploadDragger();
            this.label1 = new AntdUI.Label();
            this.label2 = new AntdUI.Label();
            this.btnStartConvert = new AntdUI.Button();
            this.steps = new AntdUI.Steps();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.excelTable = new AntdUI.Table();
            this.btnStopConvert = new AntdUI.Button();
            this.txtFolder = new AntdUI.Input();
            this.btnChooseFolder = new AntdUI.Button();
            this.btnLoadFiles = new AntdUI.Button();
            this.plChooseFiles = new AntdUI.Panel();
            this.plChooseFiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // uploadDragger
            // 
            this.uploadDragger.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.uploadDragger.Location = new System.Drawing.Point(0, 53);
            this.uploadDragger.Margin = new System.Windows.Forms.Padding(4);
            this.uploadDragger.Name = "uploadDragger";
            this.uploadDragger.Size = new System.Drawing.Size(1043, 160);
            this.uploadDragger.TabIndex = 4;
            this.uploadDragger.Text = "点击或拖拽文件到此处";
            this.uploadDragger.TextDesc = "支持Excel格式的单个文件或批量文件处理。";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.label1.Location = new System.Drawing.Point(13, 117);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(329, 33);
            this.label1.TabIndex = 2;
            this.label1.Text = "选择文件";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.label2.Location = new System.Drawing.Point(11, 378);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(329, 33);
            this.label2.TabIndex = 2;
            this.label2.Text = "文件转换";
            // 
            // btnStartConvert
            // 
            this.btnStartConvert.LoadingValue = 0F;
            this.btnStartConvert.LoadingWaveCount = 2;
            this.btnStartConvert.LoadingWaveValue = 0.2F;
            this.btnStartConvert.Location = new System.Drawing.Point(11, 418);
            this.btnStartConvert.Name = "btnStartConvert";
            this.btnStartConvert.Size = new System.Drawing.Size(104, 46);
            this.btnStartConvert.TabIndex = 5;
            this.btnStartConvert.Text = "开始转换";
            this.btnStartConvert.Type = AntdUI.TTypeMini.Primary;
            this.btnStartConvert.Click += new System.EventHandler(this.btnStartConvert_Click);
            // 
            // steps
            // 
            stepsItem1.Description = "选择待转换的Excel文件";
            stepsItem1.Title = "选择文件";
            stepsItem2.Description = "Excel文件转PDF文件";
            stepsItem2.Title = "文件转换";
            stepsItem3.Title = "转换完成";
            this.steps.Items.Add(stepsItem1);
            this.steps.Items.Add(stepsItem2);
            this.steps.Items.Add(stepsItem3);
            this.steps.Location = new System.Drawing.Point(130, 47);
            this.steps.Name = "steps";
            this.steps.Size = new System.Drawing.Size(808, 63);
            this.steps.TabIndex = 0;
            this.steps.Text = "steps1";
            // 
            // pageHeader1
            // 
            this.pageHeader1.BackColor = System.Drawing.SystemColors.Control;
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Margin = new System.Windows.Forms.Padding(4);
            this.pageHeader1.MaximizeBox = false;
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.Size = new System.Drawing.Size(1069, 40);
            this.pageHeader1.TabIndex = 0;
            this.pageHeader1.Text = "Excel转PDF";
            // 
            // excelTable
            // 
            this.excelTable.Location = new System.Drawing.Point(13, 470);
            this.excelTable.Name = "excelTable";
            this.excelTable.Size = new System.Drawing.Size(1045, 318);
            this.excelTable.TabIndex = 7;
            this.excelTable.Text = "table1";
            // 
            // btnStopConvert
            // 
            this.btnStopConvert.LoadingWaveCount = 2;
            this.btnStopConvert.LoadingWaveValue = 0.3F;
            this.btnStopConvert.Location = new System.Drawing.Point(121, 418);
            this.btnStopConvert.Name = "btnStopConvert";
            this.btnStopConvert.Size = new System.Drawing.Size(104, 46);
            this.btnStopConvert.TabIndex = 6;
            this.btnStopConvert.Text = "停止转换";
            this.btnStopConvert.Type = AntdUI.TTypeMini.Error;
            this.btnStopConvert.Click += new System.EventHandler(this.btnStopConvert_Click);
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(0, 0);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.PlaceholderText = "请选择或输入文件所在路径...";
            this.txtFolder.Size = new System.Drawing.Size(793, 46);
            this.txtFolder.TabIndex = 1;
            this.txtFolder.DoubleClick += new System.EventHandler(this.txtFolder_DoubleClick);
            // 
            // btnChooseFolder
            // 
            this.btnChooseFolder.BorderWidth = 2F;
            this.btnChooseFolder.Ghost = true;
            this.btnChooseFolder.IconSvg = resources.GetString("btnChooseFolder.IconSvg");
            this.btnChooseFolder.Location = new System.Drawing.Point(799, 0);
            this.btnChooseFolder.Name = "btnChooseFolder";
            this.btnChooseFolder.Size = new System.Drawing.Size(104, 46);
            this.btnChooseFolder.TabIndex = 2;
            this.btnChooseFolder.Text = "选择路径";
            this.btnChooseFolder.Type = AntdUI.TTypeMini.Primary;
            this.btnChooseFolder.Click += new System.EventHandler(this.btnChooseFolder_Click);
            // 
            // btnLoadFiles
            // 
            this.btnLoadFiles.Location = new System.Drawing.Point(909, 0);
            this.btnLoadFiles.Name = "btnLoadFiles";
            this.btnLoadFiles.Size = new System.Drawing.Size(104, 46);
            this.btnLoadFiles.TabIndex = 3;
            this.btnLoadFiles.Text = "加载文件";
            this.btnLoadFiles.Type = AntdUI.TTypeMini.Primary;
            this.btnLoadFiles.Click += new System.EventHandler(this.btnLoadFiles_Click);
            // 
            // plChooseFiles
            // 
            this.plChooseFiles.Controls.Add(this.txtFolder);
            this.plChooseFiles.Controls.Add(this.uploadDragger);
            this.plChooseFiles.Controls.Add(this.btnLoadFiles);
            this.plChooseFiles.Controls.Add(this.btnChooseFolder);
            this.plChooseFiles.Location = new System.Drawing.Point(13, 149);
            this.plChooseFiles.Name = "plChooseFiles";
            this.plChooseFiles.Size = new System.Drawing.Size(1045, 222);
            this.plChooseFiles.TabIndex = 9;
            this.plChooseFiles.Text = "panel1";
            // 
            // FormExcel2PDF
            // 
            this.AcceptButton = this.btnChooseFolder;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1069, 800);
            this.ControlBox = false;
            this.Controls.Add(this.plChooseFiles);
            this.Controls.Add(this.excelTable);
            this.Controls.Add(this.steps);
            this.Controls.Add(this.btnStopConvert);
            this.Controls.Add(this.btnStartConvert);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pageHeader1);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormExcel2PDF";
            this.Resizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormExcel2PDF";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormExcel2PDF_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormExcel2PDF_FormClosed);
            this.plChooseFiles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private AntdUI.UploadDragger uploadDragger;
        private AntdUI.Label label1;
        private AntdUI.Label label2;
        private AntdUI.Button btnStartConvert;
        private AntdUI.Steps steps;
        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Table excelTable;
        private AntdUI.Button btnStopConvert;
        private AntdUI.Input txtFolder;
        private AntdUI.Button btnChooseFolder;
        private AntdUI.Button btnLoadFiles;
        private AntdUI.Panel plChooseFiles;
    }
}