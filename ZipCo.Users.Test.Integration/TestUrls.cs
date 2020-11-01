namespace ZipCo.Users.Test.Integration
{
    public  static class TestUrls
    {
        public static readonly string Base = "/api/v1.0";
        public static readonly string Member = $"{Base}/member";
        public static readonly string Account = $"{Base}/account";
        public static readonly string ListMembers = $"{Member}/list";
        public static readonly string ListAccounts = $"{Account}/list";
    }
}
