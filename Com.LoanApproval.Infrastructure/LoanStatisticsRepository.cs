using System.Collections.Concurrent;
using Com.LoanApproval.Application.Interfaces;
using Com.LoanApproval.Application.ReadModels;

namespace Com.LoanApproval.Infrastructure;

// This is a simple in-memory repository for demonstration purposes.
// In a real application, this would likely be replaced with a database-backed repository.
// TODO : Implement a proper database-backed repository.
// TODO : Use ef core with db context.
// NOTE : Use different repositories or UOW for read and write operations.

public class LoanStatisticsRepository : ILoanStatisticsRepository
{
    private readonly ConcurrentBag<LoanStatisticsReadModel> _records = [];
    public void AddRecord(decimal loanAmount, decimal assetValue, bool approved)
    {
        _records.Add(new LoanStatisticsReadModel
        {
            LoanAmount = loanAmount,
            AssetValue = assetValue,
            Approved = approved
        });
    }

    public IEnumerable<LoanStatisticsReadModel> GetAll() => [.. _records];
}