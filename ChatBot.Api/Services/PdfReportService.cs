using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

public class PdfReportService: IPdfReportService
{
    public byte[] GeneratePdf(List<DataModel> data)
    {
        using (var stream = new MemoryStream())
        {
            Document document = new Document();
            PdfWriter.GetInstance(document, stream);
            document.Open();

            var table = new PdfPTable(2); // Assuming 2 columns for simplicity
            table.AddCell("Category");
            table.AddCell("Value");
            foreach (var item in data)
            {
                table.AddCell(item.Category);
                table.AddCell(item.Value.ToString());
            }

            document.Add(table);
            document.Close();
            return stream.ToArray();
        }
    }
}