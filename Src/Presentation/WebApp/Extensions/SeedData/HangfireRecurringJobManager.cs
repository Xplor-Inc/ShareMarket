﻿using Hangfire;
using ShareMarket.Core.Interfaces.Conductors.EquitiesConductors;

namespace ShareMarket.WebApp.Extensions.SeedData;

public static class HangfireRecurringJobManager
{
    public static void StartJobs(this IApplicationBuilder _, IServiceScope serviceScope)
    {
        var option = new RecurringJobOptions { TimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time") };
        var jobManager = serviceScope.ServiceProvider.GetService<IRecurringJobManager>() ?? throw new InvalidOperationException("context");
        jobManager.AddOrUpdate<IEquityDailyPriceSyncConductor>("SyncEquityLTPAsync", x => x.SyncEquityLTPAsync(), "30 9 * * 1-5", options: option);
    }
}