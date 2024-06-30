namespace BaseAuthApp_BAL.Models
{
    public static class UserError
    {
        public static readonly Error UserExists = new("User.Exists", "Username is already taken");
    }
}
