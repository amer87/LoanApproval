using Com.LoanApproval.Application.Common.Dtos;
using MediatR;

namespace Com.LoanApproval.Application.Queries;

public record GetLoanStatisticsQuery : IRequest<LoanStatisticsDto>;

