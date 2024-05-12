using Domain.Core.ValueObject;

namespace Repository.Interfaces;
public interface IUserTypeRepository
{
     public Perfil GetById(int id);
}