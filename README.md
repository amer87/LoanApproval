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

LoanApprovalConsole/
│
├── Models/
│ └── LoanApplication.cs
│
├── Rules/
│ ├── ILoanRule.cs
│ ├── RuleResult.cs
│ ├── LoanAmountRangeRule.cs
│ ├── HighValueLoanRule.cs
│ └── LowValueLoanRule.cs
│
├── Application/
│ ├── Commands/
│ │ └── EvaluateLoanCommand.cs
│ │ └── EvaluateLoanCommandHandler.cs
│ ├── Queries/
│ │ └── GetLoanStatisticsQuery.cs
│ │ └── GetLoanStatisticsQueryHandler.cs
│
└── Program.cs
