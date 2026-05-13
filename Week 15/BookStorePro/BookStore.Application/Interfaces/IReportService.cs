namespace BookStore.Application.Interfaces;

public interface IReportService
{
    Task<decimal>
        GetTotalSalesAsync();
}