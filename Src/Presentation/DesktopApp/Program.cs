using AutoMapper;
using ShareMarket.DesktopApp.Entities;
using ShareMarket.DesktopApp.Models.MutualFunds;

namespace ShareMarket.DesktopApp;

internal static class Program
{
    public static IMapper Mapper = default!;
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<MFSchemes, MFContent>().ReverseMap();
            cfg.CreateMap<MFSchemes, MFHoldingData>().ReverseMap();
        });
        Mapper = config.CreateMapper();
        Application.Run(new Home());
    }
}