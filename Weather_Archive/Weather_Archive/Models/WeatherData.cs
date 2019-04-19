using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weather_Archive.Models
{
    public class WeatherData
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public double Temperature { get; set; }
        public double AirHumidity { get; set; }
        public double DewPoint { get; set; }
        public double Pressure { get; set; }
        public string DirectionOfTheWind { get; set; }
        public string WindSpeed { get; set; }
        public string Cloudiness { get; set; }
        public string CloudBase { get; set; }
        public string HorizontalVisibility { get; set; }
        public string WeatherConditions { get; set; }
    }
}