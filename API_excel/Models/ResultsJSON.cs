using System;

namespace API_excel.Models
{
    public class ResultsJSON
    {
        public int Id { get; set; }
        public int FileItemId { get; set; }
        public int? AllTime { get; set; }
        public DateTime? MinTime { get; set; } = null;
        public double? MiddleTime { get; set; }
        public double? MiddleIndicator { get; set; }
        public double? MedianIndicator { get; set; }
        public double? MaxIndicator { get; set; }
        public double? MinIndicator { get; set; }
        public int StrCount { get; set; }
        
        public ResultsJSON()
        {

        }
        public ResultsJSON(Result result)
        {
            Id = result.Id;
            FileItemId = result.FileItemId;
            AllTime = result.AllTime;
            MinTime = result.MinTime;
            MiddleTime = result.MiddleTime;
            MiddleIndicator = result.MiddleIndicator;
            MedianIndicator = result.MedianIndicator;
            MaxIndicator = result.MaxIndicator;
            MinIndicator = result.MinIndicator;
            StrCount = result.StrCount;
        }
        
        public override bool Equals(object? obj)
        {
            if (obj is ResultsJSON resultsJSON) return Id == resultsJSON.Id;
            return false;
        }
        public override int GetHashCode() => Id.GetHashCode();
    }
}
