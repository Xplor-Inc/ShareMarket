using ClosedXML.Excel;
using ShareMarket.Core.Models.Services.Groww;

namespace ShareMarket.WinApp.Utilities;

public class ExcelUtlity
{
    public static void CreateExcelFromList(List<TradeBook> trades, List<TradeBook> caps, string filePath)
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
        worksheet.Cell(1, 14).Value = "CapitalUsed";
        worksheet.Cell(1, 15).Value = "Quantity";
        worksheet.Cell(1, 16).Value = "BuyValue";
        worksheet.Cell(1, 17).Value = "SLDate";
        worksheet.Cell(1, 18).Value = "LTP30Days";

        int row = 2;
        foreach (var person in trades)
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
            worksheet.Cell(row, 14).Value = person.CapitalUsed;
            worksheet.Cell(row, 15).Value = person.Quantity;
            worksheet.Cell(row, 16).Value = person.BuyValue;
            worksheet.Cell(row, 17).Value = person.SLDate?.ToShortDateString();
            worksheet.Cell(row, 18).Value = person.LTP30Days;
            row++;
        }

        // Capital
        var worksheet1 = workbook.Worksheets.Add("Capital");
        worksheet1.Cell(1, 1).Value = "Buy Date";
        worksheet1.Cell(1, 2).Value = "Buy Value";
        worksheet1.Cell(1, 3).Value = "Sell Value";
        worksheet1.Cell(1, 4).Value = "Capital";

        int row1 = 2;
        foreach (var person in caps)
        {
            worksheet1.Cell(row1, 1).Value = person.BuyDate.ToShortDateString();
            worksheet1.Cell(row1, 2).Value = person.BuyRate;
            worksheet1.Cell(row1, 3).Value = person.SellRate;
            worksheet1.Cell(row1, 4).Value = person.CapitalUsed;
            row1++;
        }

        // Save the workbook to the file path
        workbook.SaveAs(filePath);
    }
}
