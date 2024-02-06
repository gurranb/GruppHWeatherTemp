using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruppHWeatherTemp.Models
{
    internal class WeatherTools
    {
        public WeatherTools(bool location, double humidity,double temperature)
        {
            Location = location;
            Humidity = humidity;
            Temperature = temperature;
        }

        public bool Location { get; set; }
        public double Humidity { get; set; }
        public double Temperature { get; set;}



    }
}
