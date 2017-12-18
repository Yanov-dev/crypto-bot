using System;

namespace crypto.bot.backend.Models.CryptoTrigger
{
    public class CryptoTrigger
    {
        public Guid Id { get; set; }

        public long TelegramUserId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}