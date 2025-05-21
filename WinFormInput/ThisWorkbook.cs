namespace WinFormInput
{
    public partial class ThisWorkbook
    {
        private void ThisWorkbook_Startup(object sender, System.EventArgs e)
        {
            this.Open += ThisWorkbook_Open;
        }

        private void ThisWorkbook_Open()
        {
            GetInputString inputForm = new GetInputString();
            inputForm.ShowDialog();
        }
        public void WriteStringToCell(string formData)
        {
            Globals.Sheet1.formInput.Value2 = formData;
        }

        private void ThisWorkbook_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO 设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisWorkbook_Startup);
            this.Shutdown += new System.EventHandler(ThisWorkbook_Shutdown);
        }

        #endregion

    }
}
