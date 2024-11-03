namespace Task_Management_System_API.Helpers
{
    public class JWT
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public float ExpireAfterInMinute { get; set; }
    }




}
