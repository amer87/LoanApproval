namespace Com.LoanApproval.Application.Dtos;

public class LoanStatisticsDto
{
    public int ApprovedCount { get; set; }
    public int DeclinedCount { get; set; }
    public decimal TotalApprovedValue { get; set; }
    public decimal AverageLTV { get; set; }
}
