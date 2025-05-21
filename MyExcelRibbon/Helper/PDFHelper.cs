namespace MyExcelRibbon.Helper
{
    internal class PDFHelper
    {
        /// <summary>
        /// Aspose excel转pdf
        /// </summary>
        /// <param name="path">文件地址</param>
        /// <param name="newFilePath">转换后的文件地址</param>
        /// <returns></returns>
        public static bool AsposeExcelToPdf(string path, string newFilePath)
        {
            try
            {
                Aspose.Cells.Workbook excel = new Aspose.Cells.Workbook(path);
                excel.Save(newFilePath, Aspose.Cells.SaveFormat.Pdf);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
