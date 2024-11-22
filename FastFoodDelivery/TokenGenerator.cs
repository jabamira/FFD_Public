using System.Security.Cryptography;


namespace FastFoodDelivery
{
    public static class TokenGenerator
    {
        public static string GenerateAccessToken()
        {
            // Use a cryptographically secure random number generator
            using (var rng = RandomNumberGenerator.Create())
            {
                // Generate a 32-byte (256-bit) random token
                var tokenBytes = new byte[32];
                rng.GetBytes(tokenBytes);

                // Convert the bytes to a hexadecimal string
                return Convert.ToHexString(tokenBytes);
            }
        }
    }
}
