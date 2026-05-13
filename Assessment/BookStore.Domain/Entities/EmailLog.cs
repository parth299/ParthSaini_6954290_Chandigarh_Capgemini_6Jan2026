namespace BookStore.Domain.Entities;

public class EmailLog
{
    public int EmailLogId { get; set; }
    public string ToEmail { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public DateTime SentDate { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Sent, Failed
}
