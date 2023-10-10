using ClosedXML.Excel;
using LogicitApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicitApp.Shared
{
    public class Report
    {
        public static void Generate(Order order)
        {
            using var excel = new XLWorkbook();
            var worksheet = excel.AddWorksheet("Report");

            var titleCell = worksheet.Cell("A1");

            titleCell.Value = "Товарно-транспортная накладная №";
            titleCell.Style.Font.FontSize = 18;

            var numberCell = worksheet.Range("G1", "I1");
            numberCell.Merge();
            numberCell.Value = order.Id;

            var dateCell = worksheet.Range("J1", "L1");
            dateCell.Merge();
            dateCell.Value = DateTime.Today.Date;

            var senderCell = worksheet.Cell("A3");
            senderCell.Value = "Грузоотправитель:";

            var senderRange = worksheet.Range("C3", "L3");
            senderRange.Merge();
            senderRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

            var buyerCell = worksheet.Cell("A5");
            buyerCell.Value = "Заказчик (Плательщик):";

            var buyerRange = worksheet.Range("D5", "L5");
            buyerRange.Merge();
            buyerRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            buyerRange.Value = order.Client.OrganizationName;

            var buyer2Cell = worksheet.Cell("A7");
            buyer2Cell.Value = "Грузополучатель:";

            var buyer2Range = worksheet.Range("C7", "L7");
            buyer2Range.Merge();
            buyer2Range.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            buyer2Range.Value = order.Client.OrganizationName;

            var driverCell = worksheet.Cell("F9");
            driverCell.Value = "Водитель:";

            var driverRange = worksheet.Range("G9", "L9");
            driverRange.Merge();
            driverRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            driverRange.Value = order.Driver.FullName;

            excel.SaveAs("File.xlsx");

            var JUST_BREAKPOINT = string.Empty;
        }
    }
}
