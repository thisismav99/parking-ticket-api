using DotNetEnv;

namespace ParkingTicketingAPI.Utilities.Helpers
{
    public static class ENVFiles
    {
        public static void Exists(string FileName)
        {
            if (File.Exists(FileName))
            {
                Env.Load(FileName);
            }
        }
    }
}
