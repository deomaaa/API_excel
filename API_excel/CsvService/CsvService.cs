using API_excel.Models;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations.Schema;
using API_excel.CsvService;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using API_excel.FuncClasses;

namespace API_excel.CsvService
{
    public class CsvService : ICsvService
    {
        private readonly ApplicationContext db;

        public CsvService(ApplicationContext context)
        {
            db = context;
        }

        public async Task<List<ResultsJSON?>> GetAllResultJSON()
        {        
            var result = new List<ResultsJSON>();
            foreach (var r in db.Results.ToList())
            {
                result.Add(new ResultsJSON (r));
            }
            
            if(result.Count != 0)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<IActionResult> PostCsvFile(IFormFile file, HttpContext httpContext) // Request.Form.Files
        {
            FileFuncClass.OverWriteFileInDB(db, file);

            FileItem _file = new FileItem { FileName = file.FileName };//Add file in db, table FileItems 

            var config = FileFuncClass.CreateConfig();
 
            return await FileFuncClass.AnalysisReadCsvFile(file, _file, db, config, httpContext);
        }

        public async Task<List<ResultsJSON?>> GetResults_FileName(string fileName)
        {
            var file = db.FileItems.Include(f => f.Results).FirstOrDefault(f => f.FileName == fileName);
           
            if (file != null)
            {
                var result = file.Results;
                var resultJSON = new ResultsJSON(result);
                var listResultsJSON = new List<ResultsJSON>();
                listResultsJSON.Add(resultJSON);

                return listResultsJSON;
            }
            return null; 
        }

        public async Task<List<ResultsJSON?>> GetResults_MiddleIndicator(double? MiddleIndicatorStart, double? MiddleIndicatorEnd)
        {
            var results = db.Results.Where(r => r.MiddleIndicator >= MiddleIndicatorStart && r.MiddleIndicator <= MiddleIndicatorEnd).ToList();
            
            if (results.Count != 0)
            {
                var resultJSON = new List<ResultsJSON>();
                foreach (var r in results)
                {
                    resultJSON.Add(new ResultsJSON(r));
                }
                return resultJSON;
            }
            return null;
        }

        public async Task<List<ResultsJSON?>> GetResults_MiddleTime(double? MiddleTimeStart, double? MiddleTimeEnd)
        {
            var results = db.Results.Where(r => r.MiddleTime >= MiddleTimeStart && r.MiddleTime <= MiddleTimeEnd).ToList();
            
            if (results.Count != 0)
            {
                var resultJSON = new List<ResultsJSON>();
                foreach (var r in results)
                {
                    resultJSON.Add(new ResultsJSON(r));
                }
                return resultJSON;
            }
            return null;
        }

        public async Task<List<ResultsJSON?>> GetResults_TimeReceipt(DateTime? DateStart, DateTime? DateEnd)
        {
            var results = db.Results.Where(r => r.MinTime >= DateStart && r.MinTime <= DateEnd).ToList();
            if (results.Count != 0)
            {
                var resultJSON = new List<ResultsJSON>();
                foreach (var r in results)
                {
                    resultJSON.Add(new ResultsJSON(r));
                }
                return resultJSON;
            }
            return null;
        }

        public async Task<List<ValueJSON?>> GetValues(string fileName)
        {
            var file = db.FileItems.Include(f => f.Values).FirstOrDefault(f => f.FileName == fileName);
            var fileJSON = new List<ValueJSON>();

            if (file != null)
            {
                foreach (var v in file.Values)
                {
                    fileJSON.Add(new ValueJSON(v));
                }
                return fileJSON;
            }
            return null;
        }
    }
}