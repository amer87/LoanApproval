using Com.LoanApproval.Application.ReadModels;

namespace Com.LoanApproval.Application.Common.Interfaces;

public interface ILoanStatisticsRepository
{
    void AddRecord(decimal loanAmount, decimal assetValue, bool approved);
    IEnumerable<LoanStatisticsReadModel> GetAll();
}
