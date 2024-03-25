using Domain.Transactions.ValueObject;

namespace Repository.Interfaces;
public interface ICreditCardBrandRepository
{
     public CreditCardBrand GetById(int id);
}