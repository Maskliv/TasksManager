namespace TasksManager.Domain.Variables
{
    public static class AppSettingsKeys
    {
        public static string SALT_INTERNAL { get { return "App:InternalSaltKey"; } }
        public static string API_KEY { get { return "App:ApiKey"; } }
        public static string JWT_SECRET { get { return "Jwt:Secret"; } }
        public static string API_KEY_ID { get { return "ApiKeyId"; } }
        public static string CORS_NAME { get { return "TasksAppSecurityPolicy"; } }
        public static string DB_CONNECTION { get { return "DbConnection"; } }
    }
}
