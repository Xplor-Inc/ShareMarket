using Microsoft.EntityFrameworkCore;
using ShareMarket.WinApp.Store;

namespace ShareMarket.WinApp;

internal static class Program
{

    [STAThread]
    static void Main()
    {
        new ShareMarketContext().Database.Migrate();
        ApplicationConfiguration.Initialize();
        Application.Run(new Home());
    }
}