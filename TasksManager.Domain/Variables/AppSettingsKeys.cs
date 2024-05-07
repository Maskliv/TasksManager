﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksManager.Domain.Variables
{
    public static class AppSettingsKeys
    {
        public static string SALT_INTERNAL { get { return "App:InternalSaltKey"; } }
        public static string JWT_SECRET { get { return "Jwt:Secret"; } }
        public static string API_KEY_ID { get { return "ApiKeyId"; } }
        public static string CORS_NAME { get { return "TasksAppSecurityPolicy"; } }
        public static string DB_CONNECTION { get { return "DbConnection"; } }
    }
}
