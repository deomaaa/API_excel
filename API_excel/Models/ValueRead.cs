using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace API_excel.Models
{
    public class ValueRead : IValidatableObject //The class to which data is read from the csv file 
    {
        [Index(0)]
        public string? time { get; set; }
       
        [Index(1)]
        public int? seconds { get; set; }

        [Index(2)]
        public double? indicator { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            DateTime? time_date = DateTime.ParseExact(time, "yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture);
            DateTime? min_date = new DateTime(2000, 1, 1, 0, 0, 0);
            if((time_date > DateTime.Now || time_date < min_date) || time_date == null)
            {
                yield return new ValidationResult("Error time");
            }

            if((seconds<0 || seconds>int.MaxValue) || seconds == null)
            {
                yield return new ValidationResult("Error seconds");
            }

            if((indicator<0 || indicator>double.MaxValue) || indicator == null)
            {
                yield return new ValidationResult("Error indicator");
            }
        }
    }
}