using Domain.Administrative.ValueObject;

namespace Application.Administrative.Dto;

public enum PerfilDto
{
    Todos = (int)Perfil.UserType.Invalid,
    Admin = (int)Perfil.UserType.Admin,
    Normal = (int)Perfil.UserType.Normal,    
}
