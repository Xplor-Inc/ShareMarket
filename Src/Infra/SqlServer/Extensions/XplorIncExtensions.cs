using ShareMarket.Core.Enumerations;
using ShareMarket.Core.Interfaces.Utility.Security;

namespace ShareMarket.SqlServer.Extensions;
public static class ShareMarketExtensions
{
    public static void AddInitialData(this ShareMarketContext context, IEncryption encryption)
    {
        context.SeedUsers(encryption);
    }

    private static void SeedUsers(this ShareMarketContext context, IEncryption encryption)
    {
        if (!context.Users.Any())
        {
            var salt = encryption.GenerateSalt();
            var user = new User
            {
                CreatedById = SystemConstant.SystemUserId,
                CreatedOn = DateTimeOffset.Now.ToIst(),
                EmailAddress = "test@app.com",
                FirstName = "Admin",
                LastName = "User",
                IsActivated = true,
                IsActive = true,
                PasswordHash = encryption.GenerateHash("1qazxsw2", salt),
                PasswordSalt = salt,
                Role = UserRole.Admin,
                SecurityStamp = $"{Guid.NewGuid():N}",
            };
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}