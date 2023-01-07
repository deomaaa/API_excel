namespace API_excel.Models
{
    public class FilterResultSearchModel
    {
        public string? fileName { get; set; }
        public double? MiddleIndicatorStart { get; set; }
        public double? MiddleIndicatorEnd { get; set; }
        public double? MiddleTimeStart { get; set; }
        public double? MiddleTimeEnd { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
    }
}