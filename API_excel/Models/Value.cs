namespace API_excel.Models
{
    public class Value
    {
        public int Id { get; set; }
        public int FileItemId { get; set; }
        public FileItem? file { get; set; }
        public DateTime? time { get; set; } = null;
        public int seconds { get; set; }
        public double indicator { get; set; }
    }
}
