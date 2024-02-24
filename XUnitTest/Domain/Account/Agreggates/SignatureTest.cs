using Domain.Account.Agreggates;
using __mock__;

namespace Domain.Account;
public class SignatureTest
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Should_Set_Properties_Correctly_Signature(bool active)
    {
        // Arrange
        var fakeFlat = MockFlat.GetFaker();
        var dtActivation = DateTime.Now;

        // Act
        var signature = new Signature
        {
            Flat = fakeFlat,
            Active = active,
            DtActivation = dtActivation
        };

        // Assert
        Assert.Equal(fakeFlat, signature.Flat);
        Assert.Equal(active, signature.Active);
        Assert.Equal(dtActivation.Date, signature.DtActivation.Date);
    }
}