using OfficeOpenXml;
using System;

public class ExcelReportService: IExcelReportService
{
    public byte[] GenerateExcel(List<DataModel> data)
    {
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Report");

            // Add headers
            worksheet.Cells[1, 1].Value = "Category";
            worksheet.Cells[1, 2].Value = "Value";

            // Add data
            for (int i = 0; i < data.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = data[i].Category;
                worksheet.Cells[i + 2, 2].Value = data[i].Value;
            }

            // Auto-fit columns
            worksheet.Cells.AutoFitColumns();

            return package.GetAsByteArray();
        }
    }
}