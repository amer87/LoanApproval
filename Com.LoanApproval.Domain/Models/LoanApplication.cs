namespace Com.LoanApproval.Domain.Models;

public class LoanApplication
{
    public decimal LoanAmount { get; set; }
    public decimal AssetValue { get; set; }
    public int CreditScore { get; set; }
}
