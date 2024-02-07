using GruppHWeatherTemp.Methods;
using GruppHWeatherTemp.Models;
using System.Collections.Generic;
namespace GruppHWeatherTemp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "../../../Files/tempdata.txt";

            List<WeatherTools> readings = ReadWriteFile.ReadDataFromFile(filePath);
            ReadWriteFile.DisplayAllReadings(readings, filePath);
            while (true)
            {
               
                Console.Write("Enter the date (yyyy-MM-dd): ");


                string inputDate = Console.ReadLine();

                if (DateTime.TryParse(inputDate, out DateTime selectedDate))
                {
                    //string filePath = "../../../Files/tempdata.txt";
                    
                    //List<WeatherTools> readings = ReadWriteFile.ReadDataFromFile(filePath);

                    if (readings.Count > 0)
                    {
                        ReadWriteFile.CalculateAndDisplayAverages(selectedDate, readings);
                    }
                    else
                    {
                        Console.WriteLine("No valid data found in the file.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter the date in the format yyyy-MM-dd.");
                }
            }
            //Methods.ReadWriteFile.MonthlyTemp("tempdata.txt");
            //Methods.ReadWriteFile.DailyMidTemp("tempdata.txt");

            //Methods.ReadWriteFile.SortData("tempdata.txt");
            //Console.ReadKey(true);


        }
    }
}
