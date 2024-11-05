namespace Core.Constants
{
    public static class GeneralConsts
    {
        public const string CorsPolicyName = "MyCorsPolicy";
        public const string RefreshTokenKey = "RefereshToken";
        public enum ProjectTaskStatus
        {
            ToDo=1,
            InProgress,
            Completed,
            Blocked
        }
    }
}
