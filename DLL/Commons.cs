using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class Commons
    {
        public static class Roles
        {
            public const string ROLE_ADMIN = "VMSADMIN";
            public const string ROLE_TENANT = "VMSTENANT";
            public const string ROLE_GRO = "VMSGRO";
        }


        public class Passwords
        {
            public static void setPassword(DLL.Model.Users emp, string value)
            {
                // generate a 128-bit salt using a secure PRNG
                byte[] salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                // set salt
                emp.salt = Convert.ToBase64String(salt);

                emp.password = System.Text.Encoding.UTF8.GetString(getHash(value, emp.salt));
            }

            public static bool validate(DLL.Model.Users emp, string attemptedPassword)
            {

                string hashed = System.Text.Encoding.UTF8.GetString(getHash(attemptedPassword, emp.salt));

                return emp.password.Equals(hashed);
            }

            #region Helpers
            private static byte[] getHash(string password, string salt)
            {
                byte[] unhashedBytes = Encoding.Unicode.GetBytes(String.Concat(salt, password));

                SHA256Managed sha256 = new SHA256Managed();
                byte[] hashedBytes = sha256.ComputeHash(unhashedBytes);

                return hashedBytes;
            }

            private static bool compareHash(string attemptedPassword, byte[] hash, string salt)
            {
                string base64Hash = Convert.ToBase64String(hash);
                string base64AttemptedHash = Convert.ToBase64String(getHash(attemptedPassword, salt));

                return base64Hash == base64AttemptedHash;
            }
            #endregion
        }
    }
}
