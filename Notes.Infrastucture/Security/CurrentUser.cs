using Microsoft.EntityFrameworkCore;
using Notes.Domain.Models;
using Notes.Infrastructure.ApplicationContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Infrastucture.Security
{
    public static class CurrentUser
    {
        public static async Task<string> GetEmailAsync(ClaimsPrincipal claimsPrincipal)
        {
            return await Task.FromResult(claimsPrincipal?.FindFirst(ClaimTypes.Email)?.Value ?? "");
        }

        public static string GetEmail(ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.FindFirst(ClaimTypes.Email)?.Value ?? "";
        }

        public static async Task<User?> GetUserAsync(EFContext context, string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public static async Task<User?> GetUserAsync(EFContext context, ClaimsPrincipal claimsPrincipal)
        {
            string email = await GetEmailAsync(claimsPrincipal);
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public static User? GetUser(EFContext context, ClaimsPrincipal claimsPrincipal)
        {
            string email = GetEmail(claimsPrincipal);
            return context.Users.Include(user => user.Person).FirstOrDefault(u => u.Email == email);
        }
    }
}
