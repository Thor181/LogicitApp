using ClosedXML.Excel;
using LogicitApp.Data.Models;
using Microsoft.Win32;
using Spire.Xls;
using System.IO;

namespace LogicitApp.Shared
{
    public class Report
    {
        private const int LargeFontSize = 18;

        public static void Generate(Order order)
        {
            using var excel = new XLWorkbook();
            var worksheet = excel.AddWorksheet("Report");

            var titleCell = worksheet.Cell("A1");

            titleCell.Value = "Товарно-транспортная накладная №";
            titleCell.Style.Font.FontSize = LargeFontSize;

            var numberCell = worksheet.Range("G1", "I1");
            numberCell.Merge();
            numberCell.Style.Font.FontSize = LargeFontSize;
            numberCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            numberCell.Value = order.Id;

            var dateCell = worksheet.Range("J1", "L1");
            dateCell.Merge();
            dateCell.Style.Font.FontSize = LargeFontSize;
            dateCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            dateCell.Value = DateTime.Today.Date;

            CreateFieldWithUnderlining(worksheet, "A3", "C3", "L3", "Грузоотправитель:", "");
            CreateFieldWithUnderlining(worksheet, "A5", "D5", "L5", "Заказчик (Плательщик):", order.Client.OrganizationName);
            CreateFieldWithUnderlining(worksheet, "A7", "C7", "L7", "Грузополучатель:", order.Client.OrganizationName);
            CreateFieldWithUnderlining(worksheet, "F9", "G9", "L9", "Водитель:", order.Driver.FullName);
            CreateFieldWithUnderlining(worksheet, "A11", "C11", "F11", "Автомобиль:", $"{order.Transport.Brand} {order.Transport.Model}");
            CreateFieldWithUnderlining(worksheet, "G11", "I11", "L11", "Гос. номер:", $"{order.Transport.PlateNumber}");
            CreateFieldWithUnderlining(worksheet, "A13", "C13", "F13", "Пункт погрузки:", "");
            CreateFieldWithUnderlining(worksheet, "G13", "I13", "L13", "Пункт разгрузки:", order.DeliveryAddress);

            CreateTableHeaderCell(worksheet, "A15", "A15", "№ п/п");
            CreateTableHeaderCell(worksheet, "B15", "F15", "Наименование продукции (груза)");
            CreateTableHeaderCell(worksheet, "G15", "H15", "Цена за ед.");
            CreateTableHeaderCell(worksheet, "I15", "J15", "Ед. изм.");
            CreateTableHeaderCell(worksheet, "K15", "L15", "Кол-во");

            var groupedProducts = order.OrderProducts.GroupBy(x => x.Product, (x, y) => new { Product = x, Count = y.Count() });
            var groupedProductsCount = groupedProducts.Count();

            CreateFieldWithUnderlining(worksheet, "A18", "D18", "D18", "Всего наименований:", groupedProductsCount.ToString());
            CreateFieldWithUnderlining(worksheet, "A20", "C20", "F20", "Итого стоимость:", order.OrderProducts.Sum(x => x.Product.Price).ToString());
            worksheet.Cell("G20").Value = "руб.";
            CreateFieldWithUnderlining(worksheet, "E18", "G18", "G18", "Масса груза (нетто):", order.OrderProducts.Sum(x => x.Product.Weight).ToString());
            CreateFieldWithUnderlining(worksheet, "A22", "C22", "F22", "Отпуск произвел:", "");

            worksheet.Column("L").Width = 25;

            var mp1 = worksheet.Cell("F23");
            mp1.Value = "М.П.";
            mp1.Style.Font.Bold = true;

            worksheet.Range("G18", "G22").Style.Border.RightBorder = XLBorderStyleValues.Medium;

            CreateFieldWithUnderlining(worksheet, "H18", "L18", "L18", "Груз к перевозке принял водитель:", order.Driver.FullName);
            CreateFieldWithUnderlining(worksheet, "H20", "L20", "L20", "Груз сдал водитель:", order.Driver.FullName);
            CreateFieldWithUnderlining(worksheet, "H22", "L22", "L22", "Груз получил грузополучатель:", "");

            var mp2 = worksheet.Cell("L23");
            mp2.Value = "М.П.";
            mp2.Style.Font.Bold = true;
            mp2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            var rowIndex = 16;
            worksheet.Cell(rowIndex - 1, "A").WorksheetRow().InsertRowsBelow(groupedProductsCount + 1);

            for (int i = 0; i < groupedProductsCount; i++)
            {
                var currentProduct = groupedProducts.ElementAt(i);

                var tableNumberCell = worksheet.Cell("A" + rowIndex);
                tableNumberCell.Value = i + 1;
                tableNumberCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                SetAllBorder(XLBorderStyleValues.Thin, tableNumberCell);

                var nameRange = worksheet.Range("B" + rowIndex, "F" + rowIndex);
                nameRange.Merge();
                nameRange.Value = currentProduct.Product.Name;
                nameRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                SetAllBorder(XLBorderStyleValues.Thin, nameRange);

                var priceRange = worksheet.Range("G" + rowIndex, "H" + rowIndex);
                priceRange.Merge();
                priceRange.Value = currentProduct.Product.Price;
                priceRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                SetAllBorder(XLBorderStyleValues.Thin, priceRange);

                var unitRange = worksheet.Range("I" + rowIndex, "J" + rowIndex);
                unitRange.Merge();
                unitRange.Value = currentProduct.Product.Unit;
                unitRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                SetAllBorder(XLBorderStyleValues.Thin, unitRange);

                var countRange = worksheet.Range("K" + rowIndex, "L" + rowIndex);
                countRange.Merge();
                countRange.Value = currentProduct.Count;
                countRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                SetAllBorder(XLBorderStyleValues.Thin, countRange);

                rowIndex++;
            }

            var sumRange = worksheet.Range("A" + rowIndex, "J" + rowIndex).Merge();
            sumRange.Value = "ИТОГО:";
            sumRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            worksheet.Range("K" + rowIndex, "L" + rowIndex).Merge().Value = groupedProducts.Sum(x => x.Count);

            worksheet.PageSetup.PrintAreas.Clear();
            worksheet.PageSetup.Scale = 70;
            worksheet.PageSetup.PrintAreas.Add(worksheet.FirstCellUsed().Address, worksheet.LastCellUsed().Address);

            excel.SaveAs("x.xlsx");

            using var ms = new MemoryStream();
            excel.SaveAs(ms);

            var wb = new Workbook();
            wb.LoadFromStream(ms);

            var sfd = new SaveFileDialog
            {
                Filter = "*.pdf|*.pdf"
            };

            if (sfd.ShowDialog() == true)
            {
                wb.SaveToFile(sfd.FileName);
            }
        }

        private static void CreateTableHeaderCell(IXLWorksheet worksheet, string firstCellAddress, string lastCellAddress, string value)
        {
            var range = worksheet.Range(firstCellAddress, lastCellAddress);
            range.Merge();
            range.Value = value;
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            SetAllBorder(XLBorderStyleValues.Medium, range);
        }

        private static void SetAllBorder(XLBorderStyleValues style, IXLRange range)
        {
            var cells = range.Cells();
            foreach (var item in cells)
                SetAllBorder(style, item);
        }

        private static void SetAllBorder(XLBorderStyleValues style, IXLCell cell)
        {
            cell.Style.Border.LeftBorder = style;
            cell.Style.Border.TopBorder = style;
            cell.Style.Border.RightBorder = style;
            cell.Style.Border.BottomBorder = style;
        }

        private static void CreateFieldWithUnderlining(IXLWorksheet worksheet, string firstCellAddress,
            string underliningStartCellAddress, string underliningEndCellAddress, string firstCellValue, string underliningValue)
        {
            var firstCell = worksheet.Cell(firstCellAddress);
            firstCell.Value = firstCellValue;

            var range = worksheet.Range(underliningStartCellAddress, underliningEndCellAddress);
            range.Merge();
            range.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            range.Value = underliningValue;
        }
    }
}
