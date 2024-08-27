namespace Application.Auth;
public interface IAuthManager
{
    string Login(AuthLoginRequest authLoginRequest);
}