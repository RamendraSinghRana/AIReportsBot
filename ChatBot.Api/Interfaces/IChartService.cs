public interface IChartService
{
    byte[] GenerateChart(string chartType, List<DataModel> data);
}