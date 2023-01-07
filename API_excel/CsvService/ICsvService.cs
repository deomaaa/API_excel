using Microsoft.AspNetCore.Mvc;
using API_excel.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API_excel.CsvService
{
    public interface ICsvService
    {
        Task<List<ResultsJSON?>> GetAllResultJSON();
        Task<IActionResult> PostCsvFile(IFormFile file, HttpContext httpContext);//we read the data from the .csv file, process it and add it to the table
        Task<List<ResultsJSON?>> GetResults_FileName(string fileName);
        Task<List<ResultsJSON?>> GetResults_MiddleIndicator(double? MiddleIndicatorStart, double? MiddleIndicatorEnd);
        Task<List<ResultsJSON?>> GetResults_MiddleTime(double? MiddleTimeStart, double? MiddleTimeEnd);
        Task<List<ResultsJSON?>> GetResults_TimeReceipt(DateTime? DateStart, DateTime? DateEnd);
        Task<List<ValueJSON?>> GetValues(string fileName);
    }
}