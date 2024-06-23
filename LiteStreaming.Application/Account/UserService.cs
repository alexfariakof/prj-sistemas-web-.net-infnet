using Application.Streaming.Dto;
using Application.Streaming.Interfaces;
using Application.Shared.Dto;
using AutoMapper;
using Domain.Account.Agreggates;
using EasyCryptoSalt;
using Repository.Interfaces;

namespace Application.Streaming;
public class UserService : IUserService
{
    private readonly ICrypto _crypto;
    private readonly IRepository<User> _userRepository;
    public UserService(IMapper mapper, IRepository<User> userRepository,  ICrypto crypto)
    {
        _crypto = crypto;
        _userRepository = userRepository;
    }

    public AuthenticationDto Authentication(LoginDto dto)
    {
        bool credentialsValid = false;

        var user = _userRepository.Find(u => u.Login.Email.Equals(dto.Email)).FirstOrDefault();
        if (user == null)
            throw new ArgumentException("Usuário inexistente!");
        else
        {
            credentialsValid = !String.IsNullOrEmpty(user.Login.Password) && !String.IsNullOrEmpty(user.Login.Email) && (_crypto.IsEquals(dto?.Password ?? "", user.Login.Password));
        }

        if (credentialsValid && user is not null)
        {
            return new AuthenticationDto
            {
                Authenticated = true,                
                UserType = user.PerfilType.Description
            };
        }
        throw new ArgumentException("Usuário Inválido!");
    }
}