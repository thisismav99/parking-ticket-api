namespace Application.Applications.User.DTO
{
    public class ResponseLoginDTO
    {
        public required string Token { get; set; }

        public required string RefreshToken { get; set; }
    }
}
