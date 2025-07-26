namespace Com.LoanApproval.Application.ReadModels;

public class LoanStatisticsReadModel
{
    public decimal LoanAmount { get; set; }
    public decimal AssetValue { get; set; }
    public bool Approved { get; set; }
}
