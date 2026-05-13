using BookStore.Application.Interfaces;

namespace BookStore.Application.Services;

public class ReportService
    : IReportService
{
    private readonly IOrderRepository _orderRepo;

    public ReportService(
        IOrderRepository orderRepo)
    {
        _orderRepo = orderRepo;
    }

    public async Task<decimal>
        GetTotalSalesAsync()
    {
        var orders =
            await _orderRepo.GetAllAsync();

        return orders.Sum(
            x => x.TotalAmount);
    }
}