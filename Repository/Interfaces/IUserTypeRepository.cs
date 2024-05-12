using Domain.Account.ValueObject;

namespace Repository.Interfaces;
public interface IUserTypeRepository
{
     public PerfilUser GetById(int id);
}