using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TasksManager.Domain.Variables;

namespace TasksManager.Application
{
    public static class Encryption
    {
        public static bool ValidateEncodedPassword(this string pwd, string hashedPwd, IConfiguration config)
        {
            byte[] pwdBytes = Encoding.UTF8.GetBytes(pwd);
            byte[] saltBytes = Encoding.UTF8.GetBytes(config[AppSettingsKeys.SALT_INTERNAL] ?? throw new Exception("Internal Salt key to encrypt passwords has not been setted yet"));
            byte[] saltedValue = pwdBytes.Concat(saltBytes).ToArray();
            using (SHA256 sha256Hash = SHA256.Create())
            {
                string saltedHash = Convert.ToBase64String(sha256Hash.ComputeHash(saltedValue));
                byte[] hashedPwdBytes = Encoding.UTF8.GetBytes(hashedPwd);
                byte[] saltedHashBytes = Encoding.UTF8.GetBytes(saltedHash);
                return hashedPwdBytes.SequenceEqual(saltedHashBytes);
            }
        }
        public static string GenerateHash(this string pwd, IConfiguration config)
        {
            byte[] pwdBytes = Encoding.UTF8.GetBytes(pwd);
            byte[] saltBytes = Encoding.UTF8.GetBytes(config[AppSettingsKeys.SALT_INTERNAL] ?? throw new Exception("Internal Salt key to encrypt passwords has not been setted yet"));
            byte[] saltedValue = pwdBytes.Concat(saltBytes).ToArray();
            using (SHA256 sha256Hash = SHA256.Create())
                return Convert.ToBase64String(sha256Hash.ComputeHash(saltedValue));
        }
    }
}
