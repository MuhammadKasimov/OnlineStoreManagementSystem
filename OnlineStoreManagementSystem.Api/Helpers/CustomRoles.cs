namespace OnlineStoreManagementSystem.Api.Helpers
{
    public static class CustomRoles
    {
        private const string Admin = "Admin";
        private const string User = "User";

        public const string UserRole = User + "," + Admin;
        public const string AdminRole = Admin;

    }
}