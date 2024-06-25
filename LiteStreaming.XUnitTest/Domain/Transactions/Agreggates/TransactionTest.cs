using Domain.Transactions.Agreggates;
using __mock__;

namespace Domain.Transactions;
public class TransactionTest
{
    [Fact]
    public void Transaction_Should_Set_Properties_Correctly()
    {
        // Arrange
        var expectedTransaction = MockTransaction.GetFaker();
        var actualTransaction = new Transaction
        {
            Id = expectedTransaction.Id,
            DtTransaction = expectedTransaction.DtTransaction,
            Monetary = expectedTransaction.Monetary,
            Description = expectedTransaction.Description,
            Customer = expectedTransaction.Customer
        };

        // Assert
        Assert.NotNull(actualTransaction);
        Assert.Equal(expectedTransaction.Id, actualTransaction.Id);
        Assert.Equal(expectedTransaction.DtTransaction, actualTransaction.DtTransaction);
        Assert.Equal(expectedTransaction.Monetary, actualTransaction.Monetary);
        Assert.Equal(expectedTransaction.Description, actualTransaction.Description);
        Assert.Equal(expectedTransaction.Customer, actualTransaction.Customer);
    }
}
