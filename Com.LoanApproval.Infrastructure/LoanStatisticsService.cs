using Com.LoanApproval.Application.Queries;
using System.Collections.Concurrent;

namespace Com.LoanApproval.Infrastructure;

using Com.LoanApproval.Application.Interfaces;
using Com.LoanApproval.Application.ReadModels;

// This is a simple in-memory repository for demonstration purposes.
// In a real application, this would likely be replaced with a database-backed repository.
// TODO : Implement a proper database-backed repository.
// TODO : Use ef core with db context.

public class LoanStatisticsRepository : ILoanStatisticsRepository
{
    private readonly ConcurrentBag<LoanStatisticsReadModel> _records = new();
    public void AddRecord(decimal loanAmount, decimal assetValue, bool approved)
    {
        _records.Add(new LoanStatisticsReadModel
        {
            LoanAmount = loanAmount,
            AssetValue = assetValue,
            Approved = approved
        });
    }
    public IEnumerable<LoanStatisticsReadModel> GetAll() => _records.ToArray();
}