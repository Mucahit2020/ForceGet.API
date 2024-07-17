using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ForceGetCase.Shared.Helper
{
    public static class PasswordHelper
    {
        // Şifreyi hash'leyen metot
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                var dd = builder.ToString();

                return builder.ToString();
            }


        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            var hashedPasswordToCheck = HashPassword(password);
            return hashedPasswordToCheck == hashedPassword;
        }


    }
}
