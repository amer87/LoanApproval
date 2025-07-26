namespace Com.LoanApproval.Domain.Models;

public record LoanApplication
{
    public decimal LoanAmount { get; init; }
    public decimal AssetValue { get; init; }
    public int CreditScore { get; init; }
}
