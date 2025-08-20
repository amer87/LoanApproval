using Com.LoanApproval.Infrastructure;

namespace Com.LoanApproval.Tests;

public class LoanStatisticsRepositoryTests
{
    [Fact]
    public void AddRecord_StoresRecordCorrectly()
    {
        var repo = new LoanStatisticsRepository();
        repo.AddRecord(100000, 200000, true);
        var records = repo.GetAll().ToList();
        Assert.Single(records);
        Assert.Equal(100000, records[0].LoanAmount);
        Assert.Equal(200000, records[0].AssetValue);
        Assert.True(records[0].Approved);
    }

    [Fact]
    public void GetAll_ReturnsAllRecords()
    {
        var repo = new LoanStatisticsRepository();
        repo.AddRecord(100000, 200000, true);
        repo.AddRecord(200000, 300000, false);
        var records = repo.GetAll().ToList();
        Assert.Equal(2, records.Count);
    }
}