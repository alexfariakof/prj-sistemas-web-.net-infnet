using Domain.Account.ValueObject;

namespace Repository.Interfaces;
public interface IUserTypeRepository
{
     public UserType GetById(int id);
}