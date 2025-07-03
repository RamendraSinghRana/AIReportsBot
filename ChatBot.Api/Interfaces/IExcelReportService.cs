public interface IExcelReportService
{
    byte[] GenerateExcel(List<DataModel> data);
}