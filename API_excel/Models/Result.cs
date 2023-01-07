using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;

namespace API_excel.Models
{
    public class Result
    {
        public int Id { get; set; }     
        public int FileItemId { get; set; }//Внешний ключ
        public FileItem? file { get; set; }//Навигационное свойство 
        public int? AllTime { get; set; }
        public DateTime? MinTime { get; set; } = null;
        public double? MiddleTime { get; set; }
        public double? MiddleIndicator { get; set; }
        public double? MedianIndicator { get; set; }
        public double? MaxIndicator { get; set; }
        public double? MinIndicator { get; set; }
        public int StrCount { get; set; }
       
        public Result()
        {

        }
        public Result(List<Value> values)
        {
            StrCount = values.Count;
            file = values[0].file;

            //Indicator
            MiddleIndicator = values.Average(v => v.indicator);
            MinIndicator = values.Min(v => v.indicator);
            MaxIndicator = values.Max(v => v.indicator);
            
            var sortedValuesList = values.OrderBy(v => v.indicator).ToList();
            MedianIndicator = (values.Count % 2 != 0) ? (double)sortedValuesList[sortedValuesList.Count / 2].indicator : ((double)sortedValuesList[sortedValuesList.Count / 2].indicator + (double)sortedValuesList[sortedValuesList.Count / 2].indicator) / 2;

            //Time
            AllTime = values.Max(v => v.seconds) - values.Max(v => v.seconds);
            MinTime = values[0].file.TimeReceipt;
            MiddleTime = values.Average(v => v.seconds);
        }
    }
}
