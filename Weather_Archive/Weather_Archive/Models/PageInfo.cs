using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weather_Archive.Models
{
    public class PageInfoMonth
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; } 
        public int TotalPages { get; } = 12;
    }

    public class PageInfoYear
    {
        public int currentYear { get; set; }
        public static int chosenYear { get; set; } = 0;
    }

    public class InfoViewModel
    {
        public IEnumerable<WeatherData> weather { get; set; }
        public PageInfoMonth PageInfoMonth { get; set; }
        public PageInfoYear PageInfoYear { get; set; }
    }
}