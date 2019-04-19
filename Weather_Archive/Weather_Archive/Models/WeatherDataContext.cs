namespace Weather_Archive.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class WeatherDataContext : DbContext
    {
        public DbSet<WeatherData> weatherDatas { get; set; }
    }
}