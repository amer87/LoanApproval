namespace Com.LoanApproval.Domain.Rules;

public class RuleResult
{
    public bool IsSuccess { get; }
    public string Message { get; }

    private RuleResult(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public static RuleResult Success(string message = "") => new(true, message);

    public static RuleResult Failure(string message) => new(false, message);
}

