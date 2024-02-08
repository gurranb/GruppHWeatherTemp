using GruppHWeatherTemp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace GruppHWeatherTemp.Methods
{
    internal class Switches
    {

        public static void TemperatureSwitch(List<WeatherTools> readings)
        {      
            Console.Clear();
            List<string> startText = new List<string> {"[1] Medeltemperatur per dag (inomhus)", "[2] Medeltemperatur per dag (utomhus)", 
                "[3] Medeltemperatur per månad(inomhus)", "[4] Medeltemperatur per månad (utomhus)", "[E] Exit" };
            var loginWindow = new Window("Temperaturdata", 0, 1, startText);
            loginWindow.DrawWindow();

            var key = Console.ReadKey(true);

            switch (key.KeyChar)
            {
                case '1':
                    Console.Clear();
                    ReadWriteFile.DisplayInsideAveragesTempDay(readings);
                    Console.ReadKey(true);
                    Console.Clear();
                    TemperatureSwitch(readings);
                    break;
                case '2':
                    Console.Clear();
                    ReadWriteFile.DisplayOutsideAveragesTempDay(readings);
                    Console.ReadKey(true);
                    Console.Clear();
                    TemperatureSwitch(readings);
                    break;
                case '3':
                    Console.Clear();
                    ReadWriteFile.DisplayInsideAveragesTempMonth(readings);
                    Console.ReadKey(true);
                    Console.Clear();
                    TemperatureSwitch(readings);
                    break;
                case '4':
                    Console.Clear();
                    ReadWriteFile.DisplayOutsideAveragesTempMonth(readings);
                    Console.ReadKey(true);
                    Console.Clear();
                    TemperatureSwitch(readings);
                    break;
                case 'e':
                    return;
                default:
                    Console.WriteLine("Wrong Input");
                    Console.ReadKey(true);
                    break;
            }
        }

        public static void HumiditySwitch(List<WeatherTools> readings)
        {
            Console.Clear();
            List<string> startText = new List<string> { "[1] Medel luftfuktighet per dag (inomhus)", "[2] Medel luftfuktighet per dag (utomhus)", 
                "[3] Medel luftfuktighet per månad (inomhus)", "[4] Medel luftfuktighet per månad (utomhus)", "[E] Exit" };
            var loginWindow = new Window("Luftfuktighet", 0, 1, startText);
            loginWindow.DrawWindow();

            var key = Console.ReadKey(true);

            switch (key.KeyChar)
            {
                case '1':
                    Console.Clear();
                    ReadWriteFile.DisplayInsideAveragesHumidityDay(readings);
                    Console.ReadKey(true);
                    Console.Clear();
                    HumiditySwitch(readings);
                    break;
                case '2':
                    Console.Clear();
                    ReadWriteFile.DisplayOutsideAveragesHumidityDay(readings);
                    Console.ReadKey(true);
                    Console.Clear();
                    HumiditySwitch(readings);
                    break;
                case '3':
                    Console.Clear();
                    ReadWriteFile.DisplayInsideAveragesHumidityMonth(readings);
                    Console.ReadKey(true);
                    Console.Clear();
                    HumiditySwitch(readings);
                    break;
                case '4':
                    Console.Clear();
                    ReadWriteFile.DisplayOutsideAveragesHumidityMonth(readings);
                    Console.ReadKey(true);
                    Console.Clear();
                    HumiditySwitch(readings);
                    break;
                case 'e':
                    return;
                default:
                    Console.WriteLine("Wrong Input");
                    Console.ReadKey();
                    break;
            }
        }

        public static void MoldRiskSwitch(List<WeatherTools> readings)
        {
            Console.Clear();
            List<string> startText = new List<string> { "[1] Mögelrisk per månad (inomhus)", "[2] Mögelrisk per månad (utomhus)", "[E] Exit" };
            var loginWindow = new Window("Mögelrisk", 0, 1, startText);
            loginWindow.DrawWindow();

            var key = Console.ReadKey(true);

            switch (key.KeyChar)
            {
                case '1':
                    Console.Clear();
                    ReadWriteFile.DisplayMoldRiskInsideMonth(readings);
                    Console.ReadKey(true);
                    Console.Clear();
                    MoldRiskSwitch(readings);
                    break;
                case '2':
                    Console.Clear();
                    ReadWriteFile.DisplayMoldRiskOutsideMonth(readings);
                    Console.ReadKey(true);
                    Console.Clear();
                    MoldRiskSwitch(readings);
                    break;
                case 'e':
                    return;
                default:
                    Console.WriteLine("Wrong Input");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
