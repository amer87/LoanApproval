# 🏦 Loan Approval Console Application (C#)

This is a simple and extensible console application that evaluates loan applications based on configurable business rules. It supports clean rule separation, easy testing, and future extension without modifying core logic.

---

## ✅ Features

- Accepts user input for:
  - Loan amount (GBP)
  - Asset value the loan is secured against (GBP)
  - User Credit score (1–999)
- Evaluates loan application based on predefined business rules
- Displays approval decision and explanation for any declined applications
- Tracks aggregate statistics:
  - Number of approved/declined applications
  - Total value of loans approved
  - Average Loan to Value (LTV) across all applications

---

## ⚙️ Business Rules (Out of the Box)

| Condition                                      | Outcome                             |
| ---------------------------------------------- | ----------------------------------- |
| Loan < £100,000 or > £1,500,000                | Decline                             |
| Loan ≥ £1M requires LTV ≤ 60% AND credit ≥ 950 | Decline if not met                  |
| Loan < £1M                                     | Evaluated using LTV & Credit score: |

- LTV < 60% ➜ credit ≥ 750
- LTV < 80% ➜ credit ≥ 800
- LTV < 90% ➜ credit ≥ 900
- LTV ≥ 90% ➜ Decline

---

## 🏗️ Project Structure

```Com.LoanApproval.sln
Com.LoanApproval.Application/
│   Com.LoanApproval.Application.csproj
│   Commands/
│       EvaluateLoanCommand.cs
│       EvaluateLoanCommandHandler.cs
│   Dtos/
│       LoanStatisticsDto.cs
│   Interfaces/
│       ILoanStatisticsRepository.cs
│   Queries/
│       GetLoanStatisticsQuery.cs
│       GetLoanStatisticsQueryHandler.cs
│   ReadModels/
│       LoanStatisticsReadModel.cs
Com.LoanApproval.Console/
│   Com.LoanApproval.Console.csproj
│   Program.cs
Com.LoanApproval.Domain/
│   Com.LoanApproval.Domain.csproj
│   Models/
│       LoanApplication.cs
│   Rules/
│       HighValueLoanRule.cs
│       ILoanRule.cs
│       LoanAmountRangeRule.cs
│       LowValueLoanRule.cs
│       RuleResult.cs
│   ServiceRegistration.cs
Com.LoanApproval.Infrastructure/
│   Com.LoanApproval.Infrastructure.csproj
│   LoanStatisticsRepository.cs
│   ServiceRegistration.cs
Com.LoanApproval.Tests/
│   Com.LoanApproval.Tests.csproj
│   LoanRuleTests.cs
│   LoanStatisticsRepositoryTests.cs
README.md```

---

## 🚀 How to Build and Run

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later installed

### 1. Restore NuGet Packages

Open a terminal in the project root and run:

```
dotnet restore
```

### 2. Build the Solution

```
dotnet build
```

### 3. Run the Console Application

```
dotnet run --project Com.LoanApproval.Console
```

### 4. Using the Application

- Enter the requested loan details when prompted.
- The application will display the loan decision and statistics after each entry.
- Type 'y' to continue or any other key to exit.

### 5. Run Unit Tests

To run all unit tests:

```
dotnet test Com.LoanApproval.Tests
```

---

**Note for Visual Studio users:**

- You can open `Com.LoanApproval.sln` in Visual Studio.
- Use the built-in Test Explorer to run and view unit tests.
- Press F5 or use the Run button to start the console application.

---

## 💡 Future Improvements & TODOs

- **Input Validation & Error Handling:**
  - Add robust validation for user input (loan amount, asset value, credit score) to handle invalid or empty entries gracefully.
  - Address the `// FIXME: Add input validation for decimal parsing` in `Program.cs`.
  - Add fluent validation for commands (`// TODO : Add fluent validation for this command` in `EvaluateLoanCommand.cs`).
- **Unit & Integration Tests:**
  - Continue maintaining high test coverage and add tests for error scenarios and statistics calculations.
  - Write unit tests for statistics query handler (`// TODO : Write unit tests for this handler` in `GetLoanStatisticsQueryHandler.cs`).
- **Configuration:**
  - Move business rule thresholds (e.g., LTV, credit score) to a config file or database for easier updates.
- **Persistence:**
  - Implement a proper database-backed repository (`// TODO : Implement a proper database-backed repository` in `LoanStatisticsRepository.cs`).
  - Use EF Core with DbContext (`// TODO : Use ef core with db context` in `LoanStatisticsRepository.cs`).
- **Extensibility:**
  - Use a proper rule engine for loan rules (`// TODO : Use proper rule engine` in `ILoanRule.cs`).
  - Support for additional loan types, more flexible rule composition, or plugin architecture.
- **Logging & Monitoring:**
  - Add structured logging and error reporting for production use.
- **API/Web Interface:**
  - Expose the functionality via a REST API or web UI for broader accessibility.
- **Domain Logic Review:**
  - Confirm and double-check statistics calculation logic with domain experts (`// TODO : Confirm this calculation logic with the domain experts`, `// TODO : Double check this calculation logic with the domain experts` in `GetLoanStatisticsQueryHandler.cs`).

---
