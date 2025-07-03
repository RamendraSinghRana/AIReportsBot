public interface IPdfReportService
{
    byte[] GeneratePdf(List<DataModel> data);
}