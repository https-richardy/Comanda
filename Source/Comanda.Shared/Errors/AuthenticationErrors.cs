namespace Comanda.Shared.Errors;

public static class AuthenticationErrors
{
    public static Error InvalidCredentials => new Error(
        Code: "#COMANDA-ERR-AUT-401",
        Description: "Invalid credentials, please check your email and password"
    );

    public static Error InvalidClientCredentials => new Error(
        Code: "#COMANDA-ERR-AUT-402",
        Description: "Invalid client credentials, please check your client id and secret"
    );

    public static Error InvalidRefreshToken => new Error(
        Code: "#COMANDA-ERR-AUT-405",
        Description: "The provided refresh token is invalid, expired, or has already been used."
    );

    public static Error LogoutFailed => new Error(
        Code: "#COMANDA-ERR-AUT-409",
        Description: "Logout failed: the refresh token is invalid, expired, or has already been used."
    );
}