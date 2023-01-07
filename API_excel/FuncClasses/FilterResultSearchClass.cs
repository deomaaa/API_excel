using API_excel.CsvService;
using API_excel.Models;

namespace API_excel.FuncClasses
{
    public static class FilterResultSearchClass
    {
        //filtering search results 
        public static async Task<List<ResultsJSON?>> GetFilterResult(ICsvService _csvService,FilterResultSearchModel filterResultSearch)
        {
            var results = await _csvService.GetAllResultJSON();

            if (filterResultSearch.fileName != null)
            {
                if (await _csvService.GetResults_FileName(filterResultSearch.fileName) != null)
                {
                    results = results?.Intersect(await _csvService.GetResults_FileName(filterResultSearch.fileName)).ToList();
                }
                else
                    results = null;
            }
            if (filterResultSearch.DateStart != null && filterResultSearch.DateEnd != null)
            {
                if (await _csvService.GetResults_TimeReceipt(filterResultSearch.DateStart, filterResultSearch.DateEnd) != null)
                {
                    results = results?.Intersect(await _csvService.GetResults_TimeReceipt(filterResultSearch.DateStart, filterResultSearch.DateEnd)).ToList();
                }
                else
                    results = null;
            }
            if (filterResultSearch.MiddleIndicatorStart != null && filterResultSearch.MiddleIndicatorEnd != null)
            {
                if (await _csvService.GetResults_MiddleIndicator(filterResultSearch.MiddleIndicatorStart, filterResultSearch.MiddleIndicatorEnd) != null)
                {
                    results = results?.Intersect(await _csvService.GetResults_MiddleIndicator(filterResultSearch.MiddleIndicatorStart, filterResultSearch.MiddleIndicatorEnd)).ToList();
                }
                else
                    results = null;
            }
            if (filterResultSearch.MiddleTimeStart != null && filterResultSearch.MiddleTimeEnd != null)
            {
                if (await _csvService.GetResults_MiddleTime(filterResultSearch.MiddleTimeStart, filterResultSearch.MiddleTimeEnd) != null)
                {
                    results = results?.Intersect(await _csvService.GetResults_MiddleTime(filterResultSearch.MiddleTimeStart, filterResultSearch.MiddleTimeEnd)).ToList();
                }
                else
                    results = null;
            }

            return results;
        }
    }
}
