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

namespace API_excel.CsvService
{
    public class CsvService : ICsvService
    {
        private readonly ApplicationContext db;

        public CsvService(ApplicationContext context)
        {
            db = context;
        }

        public async Task<ActionResult> PostCsvFile(IFormFile file, HttpContext httpContext) // Request.Form.Files
        {
            if (db.FileItems.FirstOrDefault(f => f.FileName == file.FileName) != null)
            {
                db.FileItems.Remove(db.FileItems.FirstOrDefault(f => f.FileName == file.FileName));
                await db.SaveChangesAsync();
            }

            FileItem _file = new FileItem { FileName = file.FileName };//Add file in db, table FileItems 

            var config = new CsvConfiguration(CultureInfo.GetCultureInfo("de-DE"))
            {
                HasHeaderRecord = false,
                Delimiter = ";",
            };
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                using var csv = new CsvReader(reader, config);

                int validStrCount = 0;//count valid strings in file 

                await foreach (var val in csv.GetRecordsAsync<ValueRead>(httpContext.RequestAborted))
                {                    
                    //Fail fast - return the first error encountered
                    /*controllerBase.ModelState.Clear();*/
                    if (/*controllerBase.TryValidateModel(val) && */validStrCount <= 10000)
                    {
                        validStrCount++;
                        _file.Values.Add(new Value
                        {
                            seconds = val.seconds,
                            indicator = val.indicator,
                            time = DateTime.ParseExact(val.time, "yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture),
                            file = _file
                        });
                    }
                    else
                    {
                        if (validStrCount >= 10000)
                        {
                            break;
                        }
                        continue;
                    }
                }
                if (validStrCount == 0)
                {
                    return new BadRequestResult();
                }
                await db.SaveChangesAsync();
            }
           
            _file.TimeEnd = DateTime.Now;
            await db.FileItems.AddAsync(_file);
            await db.SaveChangesAsync();

            _file.Results = new Result(_file.Values);
            await db.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<ResultsJSON?> GetResults_FileName(string fileName)
        {
            var file = db.FileItems.Include(f => f.Results).FirstOrDefault(f => f.FileName == fileName);
            var result = file.Results;

            if (file != null)
            {
                var resultJSON = new ResultsJSON(result);

                return resultJSON;
            }
            return null; 
        }

        public async Task<List<ResultsJSON?>> GetResults_MiddleIndicator(MiddleIndicatorInterval middleIndicatorInterval)
        {
            var results = db.Results.Where(r => r.MiddleIndicator >= middleIndicatorInterval.MiddleIndicatorStart && r.MiddleIndicator <= middleIndicatorInterval.MiddleIndicatorEnd).ToList();
            if (results != null)
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

        public async Task<List<ResultsJSON?>> GetResults_MiddleTime(MiddleTimeInterval middleTimeInterval)
        {
            var results = db.Results.Where(r => r.MiddleTime >= middleTimeInterval.MiddleTimeStart && r.MiddleTime <= middleTimeInterval.MiddleTimeEnd).ToList();
            if (results != null)
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

        public async Task<List<ResultsJSON?>> GetResults_TimeReceipt(TimeInterval timeInterval)
        {
            var results = db.Results.Where(r => r.MinTime >= timeInterval.TimeStart && r.MinTime <= timeInterval.TimeEnd).ToList();
            if (results != null)
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
