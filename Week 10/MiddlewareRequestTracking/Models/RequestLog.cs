// Models/RequestLog.cs
namespace StudentPortal.Models
{
    public class RequestLog
    {
        public string Url { get; set; }
        public long ExecutionTimeMs { get; set; }
        public DateTime Time { get; set; }
    }
}