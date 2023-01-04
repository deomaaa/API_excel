using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace API_excel.Models
{
    public class ValueRead : IValidatableObject //Класс, в который считываются данные с csv файла 
    {
        [Index(0)]
        public string time { get; set; }
       
        [Index(1)]
        [Range(0, int.MaxValue)]
        public int seconds { get; set; }
     
        [Range(0.0, double.MaxValue)]
        [Index(2)]
        public double indicator { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            DateTime time_date = DateTime.ParseExact(time, "yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture);
            DateTime min_date = new DateTime(2000, 1, 1, 0, 0, 0);
            if(time_date > DateTime.Now || time_date < min_date)
            {
                yield return new ValidationResult("Error");
            }
        }
    }
}