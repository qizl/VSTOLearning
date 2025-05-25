// See https://aka.ms/new-console-template for more information
using Spire.Xls;
using System.Diagnostics;
using System.Drawing;

public partial class Program
{
    public static void Main(string[] args)
    {
        var excelFilePath = @"test.xlsx";
        var workbook = new Workbook();
        workbook.LoadFromFile(excelFilePath);

        //autoAdjustMergedCellsHeight(workbook);
        foreach (Worksheet sheet in workbook.Worksheets)
        {
            printSettings(sheet);
        }

        workbook.Save();

        Process.Start(new ProcessStartInfo
        {
            FileName = excelFilePath,
            UseShellExecute = true,
            Verb = "open"
        });
    }
    static void autoAdjustMergedCellsHeight(Workbook workbook)
    {
        // 计算合并单元格的高度
        // 1. 获取合并单元格的文本、字体、列宽
        // 2. 新建一个worksheet，将文本添加到单元格中，并设置单元格列宽为columnWidth、字体为font，然后设置单元格自动调整行高，返回单元格行高
        // 3. 设置合并单元格的行高为新高度/合并单元格的行数
        using (var tempWorkbook = new Workbook())
        {
            var tempSheet = tempWorkbook.Worksheets[0];
            double getHeight(string text, double columnWidth, ExcelFont font)
            {
                // 新建一个worksheet，将文本添加到单元格中，并设置单元格列宽为columnWidth、字体为font，然后设置单元格自动调整行高，返回单元格行高
                tempSheet.Range["A1"].Text = text;
                tempSheet.Range["A1"].Style.Font.FontName = font.FontName;
                tempSheet.Range["A1"].Style.Font.Size = font.Size;
                tempSheet.Range["A1"].ColumnWidth = columnWidth;
                tempSheet.Range["A1"].AutoFitRows();
                return tempSheet.Range["A1"].RowHeight;
            }

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
        }
    }

    static void printSettings(Worksheet worksheet)
    {
        worksheet.PageSetup.Orientation = PageOrientationType.Portrait;
        worksheet.PageSetup.FitToPagesWide = 1;
        worksheet.PageSetup.FitToPagesTall = 0;

        // 设置打印区域外边线
        var printAreaStr = "E4:I18";
        worksheet.PageSetup.PrintArea = printAreaStr;
        CellRange printArea = worksheet.Range[printAreaStr];
        void drawBorder(Spire.Xls.Collections.BordersCollection borders, BordersLineType lineType)
        {
            borders[lineType].LineStyle = LineStyleType.Thin;
            borders[lineType].Color = Color.Black;
        }
        var topRow = printArea.Row;
        drawBorder(printArea.Rows.First().Borders, BordersLineType.EdgeTop);
        drawBorder(printArea.Columns.Last().Borders, BordersLineType.EdgeRight);
        drawBorder(printArea.Rows.Last().Borders, BordersLineType.EdgeBottom);
        drawBorder(printArea.Columns.First().Borders, BordersLineType.EdgeLeft);

        //worksheet.PrintRange.BorderAround();
    }
}

