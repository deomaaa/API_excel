using API_excel.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace API_excel.FuncClasses
{
    public static class FileFuncClass//class for working with .csv file
    {
        public static CsvConfiguration CreateConfig()
        {
            var config = new CsvConfiguration(CultureInfo.GetCultureInfo("de-DE"))
            {
                HasHeaderRecord = false,
                Delimiter = ";",
            };

            return config;
        }

        public static async void OverWriteFileInDB(ApplicationContext db, IFormFile file)//overwriting data if a file with the same name exists
        {
            if (db.FileItems.FirstOrDefault(f => f.FileName == file.FileName) != null)
            {
                db.FileItems.Remove(db.FileItems.FirstOrDefault(f => f.FileName == file.FileName));
                await db.SaveChangesAsync();
            }
        }
        //we read the file and add the data to the db
        public static async Task<IActionResult> AnalysisReadCsvFile(IFormFile file, FileItem _file, ApplicationContext db, CsvConfiguration config, HttpContext httpContext)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                using var csv = new CsvReader(reader, config);

                int validStrCount = 0;//count valid strings in file 

                await foreach (var val in csv.GetRecordsAsync<ValueRead>(httpContext.RequestAborted))
                {
                    if (ValidateClass.CheckValidateModel(val) && validStrCount <= 10000)
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
                        if (validStrCount++ > 10000)
                        {
                            break;
                        }
                        continue;
                    }
                }
                if (validStrCount == 0 || validStrCount > 10000)
                {
                    return new BadRequestResult();
                }
                await db.SaveChangesAsync();                
            }

            await db.FileItems.AddAsync(_file);
            await db.SaveChangesAsync();

            _file.Results = new Result(_file.Values);
            await db.SaveChangesAsync();

            return new OkResult();
        }
    }
}
