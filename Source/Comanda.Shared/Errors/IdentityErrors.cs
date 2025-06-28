namespace Comanda.Shared.Errors;

public static class IdentityErrors
{
    public static Error EnrollmentFailed => new Error(
        Code: "#COMANDA-ERR-ID-400",
        Description: "Failed to enroll the user. Please try again or contact support."
    );

    public static Error UserAlreadyExists => new Error(
        Code: "#COMANDA-ERR-ID-409",
        Description: "The user already exists with the provided email."
    );

    public static Error UserNotFound => new Error(
        Code: "#COMANDA-ERR-ID-404",
        Description: "User not found."
    );

    public static Error FetchUsersFailed => new Error(
        Code: "#COMANDA-ERR-ID-500",
        Description: "Failed to fetch users from identity provider. Please try again later."
    );

    public static Error UnauthorizedAccess => new Error(
        Code: "#COMANDA-ERR-ID-401",
        Description: "Unauthorized access identity provider API."
    );

    public static Error ForbiddenAccess => new Error(
        Code: "#COMANDA-ERR-ID-403",
        Description: "Access forbidden: insufficient permissions to access identity provider API."
    );

    public static Error RateLimitExceeded => new Error(
        Code: "#COMANDA-ERR-ID-429",
        Description: "Rate limit exceeded while accessing identity provider API."
    );

    public static Error RoleDoesNotExist => new(
        "#COMANDA-ERR-IDT-445",
        $"The role was not found in the identity provider."
    );

    public static Error RoleAssignmentFailed => new(
        "#COMANDA-ERR-IDT-422",
        "Failed to assign role to the user."
    );

    public static Error CannotExtractUserId => new(
        "#COMANDA-ERR-IDT-500",
        "Failed to extract the user ID after enrollment."
    );

    public static Error GroupAssignmentFailed => new(
        "#COMANDA-ERR-IDT-423",
        "Failed to assign the user to the group in the identity provider."
    );

    public static Error GroupNotFound => new(
        "#COMANDA-ERR-IDT-404-GRP",
        "The specified group was not found in the identity provider."
    );
}
