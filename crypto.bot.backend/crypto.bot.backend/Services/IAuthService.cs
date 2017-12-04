namespace crypto.bot.backend.Services
{
    public interface IAuthService
    {
        string GenerateJwt(long telegramUserId);
    }
}