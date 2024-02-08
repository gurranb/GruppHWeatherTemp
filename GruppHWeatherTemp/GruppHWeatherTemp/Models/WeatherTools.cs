using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruppHWeatherTemp.Models
{
    internal class WeatherTools
    {
        public WeatherTools(DateTime timestamp, string location, double temperature, double humidity)
        {
            Timestamp = timestamp;
            Location = location;
            Humidity = humidity;
            Temperature = temperature;
        }
        public DateTime Timestamp { get; set; }
        public string Location { get; set; }
        public double Humidity { get; set; }
        public double Temperature { get; set;}
        public double MoldRisk { get; set; }



    }
}
