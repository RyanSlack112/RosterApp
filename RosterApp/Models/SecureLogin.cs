using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RosterApp.Models
{
    class SecureLogin
    {
        // The number of iterations used in the PBKDF2 algorithm
        private const int Iterations = 10000;
        // The size of the salt in bytes
        private const int SaltSize = 16;

        // Hash a password using PBKDF2 with a random salt
        public static string HashPassword(string password)
        {
            // Generate a random salt
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Create the Rfc2898DeriveBytes object with the password and salt
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations: Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(20); // 20 is the size of the hash (in bytes)

                // Combine the salt and hash
                byte[] hashBytes = new byte[SaltSize + 20];
                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(hash, 0, hashBytes, SaltSize, 20);

                // Convert the combined bytes to a string and return it
                string hashedPassword = Convert.ToBase64String(hashBytes);
                return hashedPassword;
            }
        }

        // Verify a password against a hashed password
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Extract the salt and hash from the hashed password
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Compute the hash of the provided password using the same salt and parameters as during hashing
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations: Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(20); // 20 is the size of the hash (in bytes)

                // Compare the computed hash with the hashed password
                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + SaltSize] != hash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
