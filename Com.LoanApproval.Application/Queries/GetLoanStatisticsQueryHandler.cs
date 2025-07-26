using MediatR;
using Com.LoanApproval.Application.Interfaces;
using Com.LoanApproval.Application.Dtos;

namespace Com.LoanApproval.Application.Queries;

public class GetLoanStatisticsQueryHandler(ILoanStatisticsRepository repository) : IRequestHandler<GetLoanStatisticsQuery, LoanStatisticsDto>
{
    private readonly ILoanStatisticsRepository _repository = repository;

    // TODO : Write unit tests for this handler
    public Task<LoanStatisticsDto> Handle(GetLoanStatisticsQuery request, CancellationToken cancellationToken)
    {
        var records = _repository.GetAll();
        var approved = records.Where(r => r.Approved).ToList();
        var declined = records.Where(r => !r.Approved).ToList();
        var totalApprovedValue = approved.Sum(r => r.LoanAmount);
        // TODO : Double check this calculation logic with the domain experts. 
        // should the LTV for declined loans be included?
        // If declined loans should be included, change the logic to use records instead of approved
        // and adjust the average calculation accordingly.
        // If declined loans should not be included, keep the current logic.
        var avgLtv = approved.Any()
            ? approved.Average(r => r.AssetValue == 0 ? 0 : (r.LoanAmount / r.AssetValue) * 100)
            : 0m;

        return Task.FromResult(new LoanStatisticsDto
        {
            ApprovedCount = approved.Count,
            DeclinedCount = declined.Count,
            TotalApprovedValue = totalApprovedValue,
            AverageLTV = avgLtv
        });
    }
}

