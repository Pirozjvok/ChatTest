using Microsoft.AspNetCore.Identity;

namespace ChatTest.Extensions
{
    public static class PasswordHasherExtensions
    {
        public static bool Verify<TUser>(this IPasswordHasher<TUser> hasher, TUser user, string hashed, string pwd) where TUser : class
        {
            return hasher.VerifyHashedPassword(user, hashed, pwd) != PasswordVerificationResult.Failed;
        }
    }
}
