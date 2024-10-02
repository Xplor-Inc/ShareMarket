using Microsoft.EntityFrameworkCore;
using ShareMarket.WinApp.Store;

namespace ShareMarket.WinApp;

internal static class Program
{
    public static ShareMarketContext DbContext { get; set; } = new();

    [STAThread]
    static void Main()
    {
        DbContext.Database.Migrate();
        ApplicationConfiguration.Initialize();
        Application.Run(new Home());
    }
}