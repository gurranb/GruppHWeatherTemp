using GruppHWeatherTemp.Methods;
using GruppHWeatherTemp.Models;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
namespace GruppHWeatherTemp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "../../../Files/tempdata.txt";

            List<WeatherTools> readings = ReadWriteFile.ReadDataFromFile(filePath);
            
            while (true)
            {
                Console.Clear();        
                List<string> startText = new List<string> {"[1] Visa all data", "[2] Medeltemperatur (inomhus)", "[3] Medeltemperatur (utomhus)",
                    "[4] Medel luftfuktighet (inomhus)", "[5] Medel luftfuktighet (utomhus)", "[E] Exit" };
                var loginWindow = new Window("VäderData", 0, 1, startText);
                loginWindow.DrawWindow();

                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case '1':
                        ReadWriteFile.DisplayAllReadings(readings, filePath);
                        break;
                    case '2':
                        Console.Clear();
                        ReadWriteFile.DisplayInsideAveragesTemp(readings);
                        Console.ReadKey(true);
                        break;
                    case '3':
                        Console.Clear();
                        ReadWriteFile.DisplayOutsideAveragesTemp(readings);
                        Console.ReadKey(true);
                        break;
                    case '4':
                        Console.Clear();
                        ReadWriteFile.DisplayInsideAveragesHumidity(readings);
                        Console.ReadKey(true);
                        break;
                    case '5':
                        Console.Clear();
                        ReadWriteFile.DisplayOutsideAveragesHumidity(readings);
                        Console.ReadKey(true);
                        break;
                    case 'e':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Wrong Input");
                        Console.ReadKey();
                        break;

                }
                
                //Console.Write("Enter the date (yyyy-MM-dd): ");


                //string inputDate = Console.ReadLine();

                //if (DateTime.TryParse(inputDate, out DateTime selectedDate))
                //{
                //    //string filePath = "../../../Files/tempdata.txt";

                //    //List<WeatherTools> readings = ReadWriteFile.ReadDataFromFile(filePath);

                //    if (readings.Count > 0)
                //    {
                //        ReadWriteFile.CalculateAndDisplayAverages(selectedDate, readings);
                //    }
                //    else
                //    {
                //        Console.WriteLine("No valid data found in the file.");
                //    }
                //}
                //else
                //{
                //    Console.WriteLine("Invalid date format. Please enter the date in the format yyyy-MM-dd.");
                //}
            }
        }
    }
}
