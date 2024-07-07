namespace BaseAuthApp_BAL.Models
{
    public static class UserError
    {
        public static readonly Error UserExists = new("UserError", "Username is already taken");
    }
}
