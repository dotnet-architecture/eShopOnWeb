namespace BlazorAdmin.Services
{
    public class AuthResponse
    {
        public AuthResponse()
        {
        }
        public bool Result { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
