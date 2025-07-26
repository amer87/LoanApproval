namespace Com.LoanApproval.Domain.Rules;

public class RuleResult
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public static RuleResult Success() => new() { IsSuccess = true };
    public static RuleResult Failure(string message) => new() { IsSuccess = false, Message = message };
}

