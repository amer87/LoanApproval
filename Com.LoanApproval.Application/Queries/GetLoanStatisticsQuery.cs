using Com.LoanApproval.Application.Dtos;
using MediatR;

namespace Com.LoanApproval.Application.Queries;

public record GetLoanStatisticsQuery : IRequest<LoanStatisticsDto>;

