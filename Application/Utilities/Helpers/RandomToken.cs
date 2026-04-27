using System.Security.Cryptography;

namespace Application.Utilities.Helpers
{
    internal static class RandomToken
    {
        public static string Generate()
        {
            var randomNumber = new byte[64];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);

            string token = Convert.ToBase64String(randomNumber);

            return token;
        }
    }
}
