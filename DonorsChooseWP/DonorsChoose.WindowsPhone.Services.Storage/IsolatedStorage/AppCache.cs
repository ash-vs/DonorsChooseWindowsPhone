using System;
using System.Collections.Generic;
using DonorsChoose.WindowsPhone.Models;

namespace DonorsChoose.WindowsPhone.Services.Storage.IsolatedStorage
{
    public static class AppCache
    {
        // Cache Last Viewed projects for 1 year (which is effectively "forever")
        public static readonly CacheItem<Dictionary<string, Project>> LastViewedProjects =
            new CacheItem<Dictionary<string, Project>>("LastViewedProjects", null, new TimeSpan(365, 0, 0, 0, 0));

        // Cache the Most Urgent projects for 1 hour
        public static readonly CacheItem<Dictionary<string, Project>> MostUrgentProjects =
            new CacheItem<Dictionary<string, Project>>("MostUrgentProjects", null, new TimeSpan(1, 0, 0));
    }
}
