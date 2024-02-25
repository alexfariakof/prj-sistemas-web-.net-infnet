using Domain.Core.Aggreggates;
using System.Text.RegularExpressions;

namespace Domain.Account.ValueObject;
public class Login
{
    private string _email = string.Empty;
    public string Email
    { 
        get => _email; 
        set =>  _email = IsValidEmail(value);  
    }

    private string _password = string.Empty;
    public string Password
    {
        get => _password;
        set => _password =  Crypto.GetInstance.Encrypt(value);
    }
    private string IsValidEmail(string email)
    {
        if (email.Length > 256)
            throw new ArgumentException("Email inválido!");

        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        Regex regex = new Regex(pattern);
        
        if (!regex.IsMatch(email))
            throw new ArgumentException("Email inválido!");
        
        return email;
    }
}
