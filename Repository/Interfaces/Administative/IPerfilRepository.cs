using Domain.Administrative.ValueObject;

namespace Repository.Interfaces.Administrative;
public interface IPerfilRepository
{
     public Perfil GetById(int id);
}