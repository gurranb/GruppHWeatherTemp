//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//namespace GruppHWeatherTemp.test
//{
//    internal class testar
//    {
//        Console.Write("Enter the date (yyyy-MM-dd): ");


//            string inputDate = Console.ReadLine();

//                if (DateTime.TryParse(inputDate, out DateTime selectedDate))
//                {
//                    string filePath = "../../../Files/tempdata.txt";

//        List<Reading> readings = ReadDataFromFile(filePath);

//                    if (readings.Count > 0)
//                    {
//                        CalculateAndDisplayAverages(selectedDate, readings);
//    }
//                    else
//                    {
//                        Console.WriteLine("No valid data found in the file.");
//                    }
//                }
//                else
//{
//    Console.WriteLine("Invalid date format. Please enter the date in the format yyyy-MM-dd.");
//}
//            }

//        static List<Reading> ReadDataFromFile(string filePath)
//{
//    List<Reading> readings = new List<Reading>();

//    try
//    {
//        string[] lines = File.ReadAllLines(filePath);

//        foreach (string line in lines)
//        {
//            Match match = Regex.Match(line, @"^(?<timestamp>\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}),(?<location>Inne|Ute),(?<temperature>\d+\.\d),(?<humidity>\d+)$");

//            if (match.Success)
//            {
//                if (DateTime.TryParse(match.Groups[1].Value, out DateTime timestamp) &&
//                    float.TryParse(match.Groups[3].Value, out float temperature) &&
//                    int.TryParse(match.Groups[4].Value, out int humidity))
//                {
//                    readings.Add(new Reading(timestamp, match.Groups[2].Value, temperature, humidity));
//                }
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Error reading data from file: {ex.Message}");
//    }

//    return readings;
//}

////static void DisplayAllReadings(List<Reading> readings)
////{
////    Console.WriteLine($"Readings for all dates:");

////    foreach (var reading in readings)
////    {
////        Console.WriteLine($"Timestamp: {reading.Timestamp}, Location: {reading.Location}, Temperature: {reading.Temperature}°C, Humidity: {reading.Humidity}%");
////    }
////}
//static void CalculateAndDisplayAverages(DateTime selectedDate, List<Reading> readings)
//{
//    var selectedDateReadings = readings
//        .Where(r => r.Timestamp.Date == selectedDate.Date)
//        .ToList();

//    if (selectedDateReadings.Count > 0)
//    {
//        var inneReadings = selectedDateReadings.Where(r => r.Location == "Inne").ToList();
//        var uteReadings = selectedDateReadings.Where(r => r.Location == "Ute").ToList();

//        if (inneReadings.Count > 0)
//        {
//            double averageInneTemperature = inneReadings.Average(r => r.Temperature);
//            double averageInneHumidity = inneReadings.Average(r => r.Humidity);
//            Console.WriteLine($"Average Temperature (Inne) on {selectedDate.ToShortDateString()}: {averageInneTemperature:F1} °C");
//            Console.WriteLine($"Average Humidity (Inne) on {selectedDate.ToShortDateString()}: {averageInneHumidity:F1}%");
//            Console.WriteLine();
//        }
//        else
//        {
//            Console.WriteLine($"No 'Inne' data available for {selectedDate.ToShortDateString()}");
//        }

//        if (uteReadings.Count > 0)
//        {
//            double averageUteTemperature = uteReadings.Average(r => r.Temperature);
//            double averageUteHumidity = uteReadings.Average(r => r.Humidity);
//            Console.WriteLine($"Average Temperature (Ute) on {selectedDate.ToShortDateString()}: {averageUteTemperature:F1} °C");
//            Console.WriteLine($"Average Humidity (Ute) on {selectedDate.ToShortDateString()}: {averageUteHumidity:F1}%");
//        }
//        else
//        {
//            Console.WriteLine($"No 'Ute' data available for {selectedDate.ToShortDateString()}");
//        }
//    }
//    else
//    {
//        Console.WriteLine($"No data available for {selectedDate.ToShortDateString()}");
//    }
//}
//    }
//}



//namespace testväderapp
//{
//    internal class Reading
//    {
//        public DateTime Timestamp { get; }
//        public string Location { get; }
//        public float Temperature { get; }
//        public int Humidity { get; }

//        public Reading(DateTime timestamp, string location, float temperature, int humidity)
//        {
//            Timestamp = timestamp;
//            Location = location;
//            Temperature = temperature;
//            Humidity = humidity;
//        }
//    }
//}

