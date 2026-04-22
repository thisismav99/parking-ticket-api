using DotNetEnv;

namespace ParkingTicketingAPI.Utilities.Helpers
{
    public static class EnvFiles
    {
        public static void Configure(string environment)
        {
            if (environment == "Development") 
            { 
                Exists(".env.development");
            }
            else
            {
                Exists(".env.production");
            }
        }

        private static void Exists(string fileName)
        {
            if (File.Exists(fileName))
            {
                Env.Load(fileName);
            }

            throw new FileNotFoundException($"The specified environment file '{fileName}' was not found.");
        }
    }
}
