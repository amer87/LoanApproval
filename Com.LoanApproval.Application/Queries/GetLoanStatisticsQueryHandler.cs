using MediatR;
using Com.LoanApproval.Application.Interfaces;
using Com.LoanApproval.Application.ReadModels;

namespace Com.LoanApproval.Application.Queries;

public class GetLoanStatisticsQueryHandler : IRequestHandler<GetLoanStatisticsQuery, LoanStatisticsDto>
{
    private readonly ILoanStatisticsRepository _repository;
    public GetLoanStatisticsQueryHandler(ILoanStatisticsRepository repository)
    {
        _repository = repository;
    }
    public Task<LoanStatisticsDto> Handle(GetLoanStatisticsQuery request, CancellationToken cancellationToken)
    {
        var records = _repository.GetAll();
        var approved = records.Where(r => r.Approved).ToList();
        var declined = records.Where(r => !r.Approved).ToList();
        var totalApprovedValue = approved.Sum(r => r.LoanAmount);
        var avgLtv = approved.Any() ? approved.Average(r => r.LoanAmount / r.AssetValue) : 0m;
        return Task.FromResult(new LoanStatisticsDto
        {
            ApprovedCount = approved.Count,
            DeclinedCount = declined.Count,
            TotalApprovedValue = totalApprovedValue,
            AverageLTV = avgLtv
        });
    }
}

