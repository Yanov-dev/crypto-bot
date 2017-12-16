namespace crypto.bot.backend.Services.Auth
{
    public interface IAuthService
    {
        string GenerateJwt(long telegramUserId);
    }
}