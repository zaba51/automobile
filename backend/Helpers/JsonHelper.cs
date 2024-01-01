using System.Security.Claims;
using System.Text;
using System.Text.Json;
using backend.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace backend.Helpers
{
    public static class JsonHelper
    {
        public static string GetAppUserToken(User user){
            string jsonString = JsonSerializer.Serialize(new {
                sub = user.Id,
                email = user.Email,
                role = user.Role,
                exp = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 1000 * 60,
            });

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonString));
        }
    }
}