using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace crypto.bot.backend.Extensions
{
    public static class ControllerExtensions
    {
        public static long GetTelegramUserId(this Controller controller)
        {
            var claim = controller.User.FindFirst(ClaimsIdentity.DefaultNameClaimType);
            if (claim == null)
                throw new Exception("can't find DefaultNameClaimType in user claims");

            if (!long.TryParse(claim.Value, out var userId))
                throw new Exception("Telegram user id is not number");

            return userId;
        }
    }
}