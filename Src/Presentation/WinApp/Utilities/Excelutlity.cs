using ClosedXML.Excel;
using ShareMarket.Core.Models.Services.Groww;

namespace ShareMarket.WinApp.Utilities;

public class ExcelUtlity
{
    public static void CreateExcelFromList(List<TradeBook> people, string filePath)
    {
        using var workbook = new XLWorkbook();
        
        var worksheet = workbook.Worksheets.Add("Data");
        worksheet.Cell(1, 1).Value = "Name";
        worksheet.Cell(1, 2).Value = "Code";
        worksheet.Cell(1, 3).Value = "BuyDate";
        worksheet.Cell(1, 4).Value = "BuyRate";
        worksheet.Cell(1, 5).Value = "LTP";
        worksheet.Cell(1, 6).Value = "SellDate";
        worksheet.Cell(1, 7).Value = "Target";
        worksheet.Cell(1, 8).Value = "High";
        worksheet.Cell(1, 9).Value = "Diff";
        worksheet.Cell(1, 10).Value = "Holding";
        worksheet.Cell(1, 11).Value = "Rank";
        worksheet.Cell(1, 12).Value = "StopLoss";
        worksheet.Cell(1, 13).Value = "SameDay";
        worksheet.Cell(1, 14).Value = "CapitalUsed";

        int row = 2;
        foreach (var person in people)
        {
            worksheet.Cell(row, 1).Value = person.Name;
            worksheet.Cell(row, 2).Value = person.Code;
            worksheet.Cell(row, 3).Value = person.BuyDate.ToShortDateString();
            worksheet.Cell(row, 4).Value = person.BuyRate;
            worksheet.Cell(row, 5).Value = person.LTP;
            worksheet.Cell(row, 6).Value = person.SellDate?.ToShortDateString();
            worksheet.Cell(row, 7).Value = person.Target;
            worksheet.Cell(row, 8).Value = person.High;
            worksheet.Cell(row, 9).Value = person.Diff;
            worksheet.Cell(row, 10).Value = person.Holding;
            worksheet.Cell(row, 11).Value = person.Rank;
            worksheet.Cell(row, 12).Value = person.StopLoss;
            worksheet.Cell(row, 13).Value = person.SameDay;
            worksheet.Cell(row, 14).Value = person.CapitalUsed;
            row++;
        }

        // Save the workbook to the file path
        workbook.SaveAs(filePath);
    }
}
