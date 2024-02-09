using GruppHWeatherTemp.Methods;
using GruppHWeatherTemp.Models;
using System.Collections.Generic;
namespace GruppHWeatherTemp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string filePath = "../../../Files/tempdata.txt";

            List<WeatherTools> readings = ReadWriteFile.ReadDataFromFile(filePath);
            


            while (true)
            {
                Console.Clear();
                List<string> startText = new List<string> {"[1] Visa all data", "[2] Visa TempData", "[3] Visa luftfuktighet",
                    "[4] Visa mögelrisk", "[5] Skriv in datum","[6] Spara månadsdata till log.txt", "[E] Exit" };
                var loginWindow = new Window("VäderData", 0, 1, startText);
                loginWindow.DrawWindow();

                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        ReadWriteFile.DisplayAllReadings(readings);
                        break;
                    case '2':
                        Console.Clear();
                        Switches.TemperatureSwitch(readings);
                        break;
                    case '3':
                        Console.Clear();
                        Switches.HumiditySwitch(readings);
                        break;
                    case '4':
                        Console.Clear();
                        Switches.MoldRiskSwitch(readings);
                        break;
                    case '5':
                        Console.Clear();
                        ReadWriteFile.InputDate(readings);
                        Console.ReadKey(true);
                        break;
                    case '6':
                        Console.Clear();
                        ReadWriteFile.DisplayMoldRiskInsideMonth(readings);
                        ReadWriteFile.DisplayMoldRiskOutsideMonth(readings);
                        ReadWriteFile.DisplayInsideAveragesTempMonth(readings);
                        ReadWriteFile.DisplayOutsideAveragesTempMonth(readings);
                        ReadWriteFile.DisplayOutsideAveragesHumidityMonth(readings);
                        ReadWriteFile.DisplayInsideAveragesHumidityMonth(readings);
                        Console.WriteLine("\nData sparat i log.txt!");
                        Console.ReadKey(true);
                        break;
                    case '8':
                        Console.Clear();
                        ReadWriteFile.FindConsecutiveDays(readings);
                        Console.ReadKey(true);
                        break;
                    case 'e':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Wrong Input");
                        Console.ReadKey(true);
                        break;
                }
            }
        }
    }
}
