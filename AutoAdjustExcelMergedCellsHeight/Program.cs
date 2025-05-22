// See https://aka.ms/new-console-template for more information
using Spire.Xls;


var excelFilePath = @"test.xlsx";
var workbook = new Workbook();
workbook.LoadFromFile(excelFilePath);

var sheet = workbook.Worksheets[0];
var mergedCells = sheet.MergedCells;
foreach (var mergedCell in mergedCells)
{
    var text = mergedCell.DisplayedText;
    var font = mergedCell.Style.Font;
    var columnWidth = mergedCell.Columns.Sum(m => m.ColumnWidth);
    var newHeight = getHeight(text, columnWidth, font);
    mergedCell.RowHeight = newHeight / mergedCell.RowCount;
}
workbook.Save();

double getHeight(string text, double columnWidth, ExcelFont font)
{
    // 新建一个worksheet，将文本添加到单元格中，并设置单元格列宽为columnWidth、字体为font，然后设置单元格自动调整行高，返回单元格行高
    var tempWorkbook = new Workbook();
    var tempSheet = tempWorkbook.Worksheets[0];
    tempSheet.Range["A1"].Text = text;
    tempSheet.Range["A1"].Style.Font.FontName = font.FontName;
    tempSheet.Range["A1"].Style.Font.Size = font.Size;
    tempSheet.Range["A1"].ColumnWidth = columnWidth;
    tempSheet.Rows[0].AutoFitRows();
    return tempSheet.Rows[0].RowHeight;
}