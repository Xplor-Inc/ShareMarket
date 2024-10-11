using ClosedXML.Excel;
using ShareMarket.Core.Entities.Tradings;

namespace ShareMarket.WinApp.Utilities;

public class ExcelUtlity
{
    public static void CreateExcelFromList(List<VirtualTrade> trades, List<VirtualTrade> CapUsed, string filePath)
    {
        using var workbook = new XLWorkbook();
        
        var worksheet = workbook.Worksheets.Add("Trades");
        worksheet.Cell(1, 1).Value = "Name";
        worksheet.Cell(1, 2).Value = "Code";
        worksheet.Cell(1, 3).Value = "Buy Date";
        worksheet.Cell(1, 4).Value = "Buy Rate";
        worksheet.Cell(1, 5).Value = "Buy Value";
        worksheet.Cell(1, 6).Value = "Holding";
        worksheet.Cell(1, 7).Value = "LTP";
        worksheet.Cell(1, 8).Value = "PL";
        worksheet.Cell(1, 9).Value = "Quantity";
        worksheet.Cell(1, 10).Value = "SellDate";
        worksheet.Cell(1, 11).Value = "SellRate";
        worksheet.Cell(1, 12).Value = "SellValue";
        worksheet.Cell(1, 13).Value = "SLRate";
        worksheet.Cell(1, 14).Value = "SellAction";
        worksheet.Cell(1, 15).Value = "Target";
        worksheet.Cell(1, 16).Value = "Target P";

        int row = 2;
        foreach (var person in trades)
        {
            worksheet.Cell(row, 1).Value = person.Name;
            worksheet.Cell(row, 2).Value = person.Code;
            worksheet.Cell(row, 3).Value = new DateTime(person.BuyDate, new TimeOnly());
            worksheet.Cell(row, 4).Value = person.BuyRate;
            worksheet.Cell(row, 5).Value = person.BuyValue;
            worksheet.Cell(row, 6).Value = person.Holding;
            worksheet.Cell(row, 7).Value = person.LTP;
            worksheet.Cell(row, 8).Value = person.ReleasedPL;
            worksheet.Cell(row, 9).Value = person.Quantity;
            if (person.SellDate.HasValue)
                worksheet.Cell(row, 10).Value = new DateTime(person.SellDate.Value, new TimeOnly()); //$"{person.SellDate}";
            worksheet.Cell(row, 11).Value = person.SellRate;
            worksheet.Cell(row, 12).Value = person.SellValue;
            worksheet.Cell(row, 13).Value = person.StopLoss;
            worksheet.Cell(row, 14).Value = $"{person.SellAction}";
            worksheet.Cell(row, 15).Value = person.Target;
            worksheet.Cell(row, 16).Value = person.TargetPer;
            row++;
        }

        var CapitalUsed = workbook.Worksheets.Add("CapitalUsed");
        CapitalUsed.Cell(1, 1).Value = "Buy Date";
        CapitalUsed.Cell(1, 2).Value = "Buy Value";
        CapitalUsed.Cell(1, 3).Value = "Sell Value";
        CapitalUsed.Cell(1, 4).Value = "Used Cap";

        int row1 = 2;
        foreach (var cap in CapUsed)
        {
            CapitalUsed.Cell(row1, 1).Value = new DateTime(cap.BuyDate, new TimeOnly()); ;
            CapitalUsed.Cell(row1, 2).Value = cap.BuyValue;
            CapitalUsed.Cell(row1, 3).Value = cap.SellValue;
            CapitalUsed.Cell(row1, 4).Value = cap.ReleasedPL;
            row1++;
        }
        // Save the workbook to the file path
        workbook.SaveAs(filePath);
    }
}
