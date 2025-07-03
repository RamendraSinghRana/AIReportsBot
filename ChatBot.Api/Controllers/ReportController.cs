using Microsoft.AspNetCore.Mvc;

namespace ChatBot.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IExcelReportService _excelReportService;
        private readonly IPdfReportService _reportGenerator;
        private IChartService _chartService;
        public ReportController(IExcelReportService excelReportService, IPdfReportService reportGenerator, IChartService chartService)
        {
            _chartService = chartService;
            _excelReportService = excelReportService;
            _reportGenerator = reportGenerator;
        }

        [HttpPost("excel")]
        public IActionResult Excel([FromBody] List<DataModel> data)
        {
            var file = _excelReportService.GenerateExcel(data);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
        }

        [HttpGet("pdf")]
        public IActionResult Pdf([FromBody] List<DataModel> data)
        {
            var file = _reportGenerator.GeneratePdf(data);
            return File(file, "application/pdf", "report.pdf");
        }

        [HttpPost("chart")]
        public IActionResult Chart([FromBody] ReportRequest request)
        {
            var chart = _chartService.GenerateChart(request.ChartType, request.Data);
            return File(chart, "image/png", "chart.png");
        }

    }
}