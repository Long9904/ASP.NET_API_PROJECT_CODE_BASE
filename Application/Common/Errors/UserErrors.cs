namespace Application.Common.Errors
{
    // Combine errors with their domain: UserErrors - User in domain
    public static class UserErrors
    {
        public static readonly Error NotFound =
            new("User.NotFound", "User not found", ErrorType.NotFound);

        public static readonly Error EmailAlreadyExists =
            new("User.EmailAlreadyExists", "Email already exists", ErrorType.Conflict);

        public static readonly Error InvalidPassword =
            new("User.InvalidPassword", "Password is invalid", ErrorType.Validation);
    }
}
