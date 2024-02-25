using Domain.Transactions.Agreggates;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Domain.Transactions.ValueObject;
public enum CardBrand
{
    Invalid = 99,
    Visa = 1,
    Mastercard = 2,
    Amex = 3,
    Discover = 4,
    DinersClub = 5,
    JCB = 6
}
public record CreditCardBrand
{
    [NotMapped]
    public CardBrand CardBrand { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }        
    public bool IsValid { get; }
    public virtual IList<Card> Cards { get; set; } = new List<Card>();
    
    public CreditCardBrand(int id, string name)
    {
        Id = id;
        Name = name;
        CardBrand = (CardBrand)id;
    }

    private CreditCardBrand(CardBrand brand, string name, bool isValid)
    {
        CardBrand = brand;            
        IsValid = isValid;
    }

    public static CreditCardBrand IdentifyCard(string creditCardNumber)
    {
        string cleanedNumber = RemoveNonNumericCharacters(creditCardNumber);
        bool isValid = IsCreditCardValid(cleanedNumber);

        if (Regex.IsMatch(cleanedNumber, @"^4[0-9]{12}(?:[0-9]{3})?$"))
        {
            return new CreditCardBrand(CardBrand.Visa, "Visa", isValid);
        }
        else if (Regex.IsMatch(cleanedNumber, @"^5[1-5][0-9]{14}$|^2(?:2(?:2[1-9]|[3-9][0-9])|[3-6][0-9][0-9]|7(?:[01][0-9]|20))[0-9]{12}$"))
        {
            return new CreditCardBrand(CardBrand.Mastercard, "Mastercard", isValid);
        }
        else if (Regex.IsMatch(cleanedNumber, @"^3[47][0-9]{13}$"))
        {
            return new CreditCardBrand(CardBrand.Amex, "Amex", isValid);
        }
        else if (Regex.IsMatch(cleanedNumber, @"^65[4-9][0-9]{13}|64[4-9][0-9]{13}|6011[0-9]{12}|(622(?:12[6-9]|1[3-9][0-9]|[2-8][0-9][0-9]|9[01][0-9]|92[0-5])[0-9]{10})$"))
        {
            return new CreditCardBrand(CardBrand.Discover, "Discover", isValid);
        }
        else if (Regex.IsMatch(cleanedNumber, @"^3(?:0[0-5]|[68][0-9])[0-9]{11}$"))
        {
            return new CreditCardBrand(CardBrand.DinersClub, "Diners Club", isValid);
        }
        else if (Regex.IsMatch(cleanedNumber, @"^(?:2131|1800|35[0-9]{3})[0-9]{11}$"))
        {
            return new CreditCardBrand(CardBrand.JCB, "JCB", isValid);
        }
        else
        {
            return new CreditCardBrand(CardBrand.Invalid, "com bandeira desconhecida.", false);
        }
    }
    private static bool IsCreditCardValid(string numeroCartao)
    {
        int soma = 0;
        bool alternar = false;

        for (int i = numeroCartao.Length - 1; i >= 0; i--)
        {
            int digito = numeroCartao[i] - '0';

            if (alternar)
            {
                digito *= 2;

                if (digito > 9)
                {
                    digito -= 9;
                }
            }

            soma += digito;
            alternar = !alternar;
        }

        return soma % 10 == 0;
    }

    private static string RemoveNonNumericCharacters(string input)
    {
        return new string(input.Where(char.IsDigit).ToArray());
    }

}
