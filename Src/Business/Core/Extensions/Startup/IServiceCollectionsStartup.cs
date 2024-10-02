using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ShareMarket.Core.Conductors;
using ShareMarket.Core.Conductors.EmailClient;
using ShareMarket.Core.Interfaces.Conductors;
using ShareMarket.Core.Interfaces.Emails.EmailClient;
using ShareMarket.Core.Interfaces.Utility.Security;
using ShareMarket.Core.Utilities.Security;

namespace ShareMarket.Core.Extensions.Startup;
public static class IServiceColletionsStartup
{
    public static void AddUtilityResolver(this IServiceCollection services)
    {
        services.AddScoped<IEncryption,             Encryption>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IEmailClient, EmailClient>();


        #region Repository Conductor
        services.AddScoped(typeof(IRepositoryConductor<>), typeof(RepositoryConductor<>));
        #endregion
    }
}