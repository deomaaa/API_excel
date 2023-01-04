using Microsoft.AspNetCore.Mvc;
using API_excel.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API_excel.CsvService
{
    public interface ICsvService
    {
        Task<ActionResult> PostCsvFile(IFormFile file, HttpContext httpContext);
        Task<ResultsJSON?> GetResults_FileName(string fileName);
        Task<List<ResultsJSON?>> GetResults_MiddleIndicator(MiddleIndicatorInterval middleIndicatorInterval);
        Task<List<ResultsJSON?>> GetResults_MiddleTime(MiddleTimeInterval middleTimeInterval);
        Task<List<ResultsJSON?>> GetResults_TimeReceipt(TimeInterval timeInterval);
        Task<List<ValueJSON?>> GetValues(string fileName);
    }
}