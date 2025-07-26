using Com.LoanApproval.Application.ReadModels;

namespace Com.LoanApproval.Application.Interfaces;

public interface ILoanStatisticsRepository
{
    void AddRecord(decimal loanAmount, decimal assetValue, bool approved);
    IEnumerable<LoanStatisticsReadModel> GetAll();
}
