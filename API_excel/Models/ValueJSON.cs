namespace API_excel.Models
{
    public class ValueJSON
    {
        public int Id { get; set; }
        public int FileItemId { get; set; }
        public DateTime? time { get; set; } = null;
        public int? seconds { get; set; }
        public double? indicator { get; set; }
        public ValueJSON(Value value)
        {
            Id = value.Id;
            FileItemId = value.FileItemId;
            time = value.time;
            seconds = value.seconds;
            indicator = value.indicator;
        }
    }
}
