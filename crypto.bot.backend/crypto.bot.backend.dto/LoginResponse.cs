namespace crypto.bot.backend.dto
{
    public class LoginResponse
    {
        public string Jwt { get; set; }
        
        public string Error { get; set; }
    }
}