using GruppHWeatherTemp.Models;
using System.Text.RegularExpressions;

namespace GruppHWeatherTemp.Methods
{
    internal class ReadWriteFile
    {
        public static string path = "../../../Files/";

        public static List<WeatherTools> ReadDataFromFile(string filePath)
        {
            List<WeatherTools> readings = new List<WeatherTools>();

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string lines;
                    string pattern5 = @"^(?<timestamp>\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}),(?<location>Inne|Ute),(?<temperature>-?\d+\.\d),(?<humidity>\d+)$";
                    while ((lines = sr.ReadLine()) != null)
                    {

                        Match match = Regex.Match(lines, pattern5);

                        if (match.Success)
                        {
                            string timestamps = match.Groups[1].Value;
                            string month = timestamps.Substring(5, 2);
                            string year = timestamps.Substring(0, 4);
                            string location = match.Groups["location"].Value;
                            double temp = double.Parse(match.Groups["temperature"].Value);

                            if (year != "2017" && month != "05" && temp < 40)
                            {
                                if (DateTime.TryParse(match.Groups[1].Value, out DateTime timestamp) &&
                                        double.TryParse(match.Groups["temperature"].Value, out double temperature) &&
                                        int.TryParse(match.Groups["humidity"].Value, out int humidity))
                                {
                                    readings.Add(new WeatherTools(timestamp, match.Groups[2].Value, temperature, humidity));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading data from file: {ex.Message}");
            }
            return readings;
        }

        public static void DisplayAllReadings(List<WeatherTools> readings)
        {
            Console.WriteLine($"Readings for all dates:");
            int rowCount = 1;
            foreach (var reading in readings)
            {
                Console.WriteLine(rowCount + ". " + reading.Timestamp.ToString() + " " + reading.Location + " " + reading.Temperature + "C" + " " + reading.Humidity + "%");
                rowCount++;
            }
        }

        public static void DisplayMoldRiskInsideMonth(List<WeatherTools> readings)
        {
            Console.WriteLine("Sorterat på minst till störst risk för att få mögel");
            Console.WriteLine("---------------------------------------------------");

            var uniqueDates = readings
                    .GroupBy(r => new { r.Timestamp.Year, r.Timestamp.Month })
                    .OrderByDescending(group => GetAverageMoldRisk(readings, group.Key.Year, group.Key.Month, "Inne"))
                    .ToList();

            foreach (var date in uniqueDates)
            {
                double averageMold = GetAverageMoldRisk(readings, date.Key.Year, date.Key.Month, "Inne");

                if (averageMold != double.MinValue)
                {
                    Console.WriteLine($"(Inne) - {new DateTime(date.Key.Year, date.Key.Month, 1).ToString("MMMM yyyy")}:  {averageMold:0.00}");
                    string logText = $"Mögel (Inne) {new DateTime(date.Key.Year, date.Key.Month, 1).ToString("MMMM yyyy")}:  {averageMold:0.00}";
                    TextToFile.MyDelegateStr saveDelegate = TextToFile.SaveToFile;
                    saveDelegate(logText);
                }

            }
        }

        public static void DisplayMoldRiskOutsideMonth(List<WeatherTools> readings)
        {
            Console.WriteLine("Sorterat på minst till störst risk för att få mögel");
            Console.WriteLine("---------------------------------------------------");

            var uniqueDates = readings
                    .GroupBy(r => new { r.Timestamp.Year, r.Timestamp.Month })
                    .OrderByDescending(group => GetAverageMoldRisk(readings, group.Key.Year, group.Key.Month, "Ute"))
                    .ToList();

            foreach (var date in uniqueDates)
            {
                double averageMold = GetAverageMoldRisk(readings, date.Key.Year, date.Key.Month, "Ute");

                if (averageMold != double.MinValue)
                {
                    Console.WriteLine($"(Ute) - {new DateTime(date.Key.Year, date.Key.Month, 1).ToString("MMMM yyyy")}:  {averageMold:0.00}");
                    string logText = $"Mögel (Ute) {new DateTime(date.Key.Year, date.Key.Month, 1).ToString("MMMM yyyy")}:  {averageMold:0.00}";
                    TextToFile.MyDelegateStr saveDelegate = TextToFile.SaveToFile;
                    saveDelegate(logText);
                }

            }
        }

        public static void DisplayInsideAveragesTempDay(List<WeatherTools> readings)
        {
            Console.WriteLine("Sorterat enligt varmast till kallaste dagen 'inomhus' enligt medeltemperatur per dag");
            Console.WriteLine("------------------------------------------------------------------------------------");

            var uniqueDates = readings
                .Select(r => r.Timestamp.Date)
                .Distinct()
                .OrderByDescending(date => GetAverageTemperatureDay(readings, date, "Inne"))
                .ToList();

            foreach (var date in uniqueDates)
            {
                double averageTemperature = GetAverageTemperatureDay(readings, date, "Inne");

                if (averageTemperature != double.MinValue)
                {
                    Console.WriteLine($"(Inne) - {date.ToShortDateString()}: {averageTemperature:F1} °C");
                }
            }
        }

        public static void DisplayInsideAveragesTempMonth(List<WeatherTools> readings)
        {
            Console.WriteLine("Sorterat på medeltemperatur inne per månad");
            Console.WriteLine("------------------------------------------");

            var uniqueDates = readings
                    .GroupBy(r => new { r.Timestamp.Year, r.Timestamp.Month })
                    .OrderByDescending(group => GetAverageTemperatureMonth(readings, group.Key.Year, group.Key.Month, "Inne"))
                    .ToList();

            foreach (var date in uniqueDates)
            {
                double averageTemperature = GetAverageTemperatureMonth(readings, date.Key.Year, date.Key.Month, "Inne");

                if (averageTemperature != double.MinValue)
                {
                    Console.WriteLine($"(Inne) - {new DateTime(date.Key.Year, date.Key.Month, 1).ToString("MMMM yyyy"),-15} {averageTemperature,5:F1} °C");

                    string logText = $"Temperatur (Inne) {new DateTime(date.Key.Year, date.Key.Month, 1).ToString("MMMM yyyy"),-15} {averageTemperature,5:F1} °C";
                    TextToFile.MyDelegateStr saveDelegate = TextToFile.SaveToFile;
                    saveDelegate(logText);
                }

            }
        }

        public static void DisplayOutsideAveragesTempMonth(List<WeatherTools> readings)
        {
            Console.WriteLine("Sorterat på medeltemperatur ute per månad");
            Console.WriteLine("------------------------------------------");

            var uniqueDates = readings
                    .GroupBy(r => new { r.Timestamp.Year, r.Timestamp.Month })
                    .OrderByDescending(group => GetAverageTemperatureMonth(readings, group.Key.Year, group.Key.Month, "Ute"))
                    .ToList();

            foreach (var date in uniqueDates)
            {
                double averageTemperature = GetAverageTemperatureMonth(readings, date.Key.Year, date.Key.Month, "Ute");

                if (averageTemperature != double.MinValue)
                {
                    Console.WriteLine($"(Ute) - {new DateTime(date.Key.Year, date.Key.Month, 1).ToString("MMMM yyyy"),-15} {averageTemperature,5:F1} °C");

                    string logText = $"Temperatur (Ute) {new DateTime(date.Key.Year, date.Key.Month, 1).ToString("MMMM yyyy"),-15} {averageTemperature,5:F1} °C";
                    TextToFile.MyDelegateStr saveDelegate = TextToFile.SaveToFile;
                    saveDelegate(logText);
                }

            }
        }

        public static void DisplayOutsideAveragesTempDay(List<WeatherTools> readings)
        {
            Console.WriteLine("Sorterat enligt varmast till kallaste dagen 'utomhus' enligt medeltemperatur per dag");
            Console.WriteLine("------------------------------------------------------------------------------------");

            var uniqueDates = readings
                .Select(r => r.Timestamp.Date)
                .Distinct()
                .OrderByDescending(date => GetAverageTemperatureDay(readings, date, "Ute"))
                .ToList();

            foreach (var date in uniqueDates)
            {
                double averageTemperature = GetAverageTemperatureDay(readings, date, "Ute");

                if (averageTemperature != double.MinValue)
                {
                    Console.WriteLine($"(Ute) - {date.ToShortDateString()}: {averageTemperature:F1} °C");
                }
            }
        }

        public static void DisplayOutsideAveragesHumidityDay(List<WeatherTools> readings)
        {
            Console.WriteLine("Sorterat enligt torrast till fuktigaste dagen 'utomhus' enligt medelluftfuktighet per dag");
            Console.WriteLine("-----------------------------------------------------------------------------------------");

            var uniqueDates = readings
                .Select(r => r.Timestamp.Date)
                .Distinct()
                .OrderBy(date => GetAverageHumidityDay(readings, date, "Ute"))
                .ToList();

            foreach (var date in uniqueDates)
            {
                double averageHumidity = GetAverageHumidityDay(readings, date, "Ute");

                if (averageHumidity != double.MinValue)
                {
                    Console.WriteLine($"(Ute) - {date.ToShortDateString()}: {averageHumidity:F1} %");
                }
            }
        }

        public static void DisplayInsideAveragesHumidityDay(List<WeatherTools> readings)
        {
            Console.WriteLine("Sorterat enligt torrast till fuktigaste dagen 'inomhus' enligt medelluftfuktighet per dag");
            Console.WriteLine("-----------------------------------------------------------------------------------------");

            var uniqueDates = readings
                .Select(r => r.Timestamp.Date)
                .Distinct()
                .OrderBy(date => GetAverageHumidityDay(readings, date, "Inne"))
                .ToList();

            foreach (var date in uniqueDates)
            {
                double averageHumidity = GetAverageHumidityDay(readings, date, "Inne");

                if (averageHumidity != double.MinValue)
                {
                    Console.WriteLine($"(Inne) - {date.ToShortDateString()}: {averageHumidity:F1} %");
                }
            }
        }

        public static void DisplayOutsideAveragesHumidityMonth(List<WeatherTools> readings)
        {
            Console.WriteLine("Sorterat på luftfuktighet ute per månad");
            Console.WriteLine("------------------------------------------");

            var uniqueDates = readings
                    .GroupBy(r => new { r.Timestamp.Year, r.Timestamp.Month })
                    .OrderByDescending(group => GetAverageHumidityMonth(readings, group.Key.Year, group.Key.Month, "Ute"))
                    .ToList();

            foreach (var date in uniqueDates)
            {
                double averageHumidity = GetAverageHumidityMonth(readings, date.Key.Year, date.Key.Month, "Ute");

                if (averageHumidity != double.MinValue)
                {
                    Console.WriteLine($"(Ute) - {new DateTime(date.Key.Year, date.Key.Month, 1).ToString("MMMM yyyy"),-15} {averageHumidity,5:F1} %");
                    string logText = $"Luftfuktighet (Ute) {new DateTime(date.Key.Year, date.Key.Month, 1).ToString("MMMM yyyy"),-15} {averageHumidity,5:F1} %";
                    TextToFile.MyDelegateStr saveDelegate = TextToFile.SaveToFile;
                    saveDelegate(logText);
                }

            }
        }

        public static void DisplayInsideAveragesHumidityMonth(List<WeatherTools> readings)
        {
            Console.WriteLine("Sorterat på luftfuktighet inne per månad");
            Console.WriteLine("------------------------------------------");

            var uniqueDates = readings
                    .GroupBy(r => new { r.Timestamp.Year, r.Timestamp.Month })
                    .OrderByDescending(group => GetAverageHumidityMonth(readings, group.Key.Year, group.Key.Month, "Inne"))
                    .ToList();

            foreach (var date in uniqueDates)
            {
                double averageHumidity = GetAverageHumidityMonth(readings, date.Key.Year, date.Key.Month, "Inne");

                if (averageHumidity != double.MinValue)
                {
                    Console.WriteLine($"(Inne) - {new DateTime(date.Key.Year, date.Key.Month, 1).ToString("MMMM yyyy"),-15} {averageHumidity,5:F1} %");

                    string logText = $"Luftfuktighet (Inne) {new DateTime(date.Key.Year, date.Key.Month, 1).ToString("MMMM yyyy"),-15} {averageHumidity,5:F1} %";
                    TextToFile.MyDelegateStr saveDelegate = TextToFile.SaveToFile;
                    saveDelegate(logText);
                }

            }
        }

        private static double GetAverageTemperatureMonth(List<WeatherTools> readings, int year, int month, string location)
        {
            var monthReadings = readings
            .Where(r => r.Timestamp.Year == year && r.Timestamp.Month == month && r.Location == location);
            return monthReadings.Any() ? monthReadings.Average(r => r.Temperature) : double.MinValue;
        }

        private static double GetAverageTemperatureDay(List<WeatherTools> readings, DateTime date, string location)
        {
            var locationReadings = readings
                .Where(r => r.Timestamp.Date == date && r.Location == location);

            return locationReadings.Any() ? locationReadings.Average(r => r.Temperature) : double.MinValue;
        }

        private static double GetAverageHumidityMonth(List<WeatherTools> readings, int year, int month, string location)
        {
            var monthReadings = readings
            .Where(r => r.Timestamp.Year == year && r.Timestamp.Month == month && r.Location == location);
            return monthReadings.Any() ? monthReadings.Average(r => r.Humidity) : double.MinValue;
        }

        private static double GetAverageHumidityDay(List<WeatherTools> readings, DateTime date, string location)
        {
            var locationReadings = readings
                .Where(r => r.Timestamp.Date == date && r.Location == location);

            return locationReadings.Any() ? locationReadings.Average(r => r.Humidity) : double.MinValue;
        }

        private static double GetAverageMoldRisk(List<WeatherTools> readings, int year, int month, string location)
        {
                        var monthReadings = readings
                        .Where(r => r.Timestamp.Year == year && r.Timestamp.Month == month && r.Location == location);

            if (monthReadings.Any())
            {
                double averageTemperature = monthReadings.Average(r => r.Temperature);
                double averageHumidity = monthReadings.Average(r => r.Humidity);

                // Williamson Etheridge formula
                double averageMoldRisk = (averageTemperature - 0.55) * (averageHumidity / 100) * 10;
                return averageMoldRisk;
            }
            else
            {
                return double.MinValue;
            }
        }

        public static void InputDate(List<WeatherTools> readings)
        {
            Console.Write("Skriv in datum från 2016-06-01 till 2016-12-23 med format (yyyy-MM-dd): ");

            string inputDate = Console.ReadLine();

            if (DateTime.TryParse(inputDate, out DateTime selectedDate))
            {

                if (readings.Count > 0)
                {
                    ReadWriteFile.CalculateAndDisplayAverages(selectedDate, readings);
                }
                else
                {
                    Console.WriteLine("Ingen data hittad.");
                }
            }
            else
            {
                Console.WriteLine("Fel format. Skriv in i detta format yyyy-MM-dd.");
            }
        }

        public static void CalculateAndDisplayAverages(DateTime selectedDate, List<WeatherTools> readings)
        {


            var selectedDateReadings = readings
                .Where(r => r.Timestamp.Date == selectedDate.Date)
                .ToList();

            if (selectedDateReadings.Count > 0)
            {
                var inneReadings = selectedDateReadings.Where(r => r.Location == "Inne").ToList();
                var uteReadings = selectedDateReadings.Where(r => r.Location == "Ute").ToList();

                if (inneReadings.Count > 0)
                {
                    double averageInneTemperature = inneReadings.Average(r => r.Temperature);
                    double averageInneHumidity = inneReadings.Average(r => r.Humidity);
                    Console.WriteLine($"Medeltemperatur (Inne) - {selectedDate.ToShortDateString()}: {averageInneTemperature:F1} °C");
                    Console.WriteLine($"Medel luftfuktighet (Inne) - {selectedDate.ToShortDateString()}: {averageInneHumidity:F1}%");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"Ingen 'Inne' data finns för {selectedDate.ToShortDateString()}");
                }

                if (uteReadings.Count > 0)
                {
                    double averageUteTemperature = uteReadings.Average(r => r.Temperature);
                    double averageUteHumidity = uteReadings.Average(r => r.Humidity);
                    Console.WriteLine($"Medeltemperatur (Ute) - {selectedDate.ToShortDateString()}: {averageUteTemperature:F1} °C");
                    Console.WriteLine($"Medel luftfuktighet (Ute) - {selectedDate.ToShortDateString()}: {averageUteHumidity:F1}%");
                }
                else
                {
                    Console.WriteLine($"Ingen 'Ute' data finns för {selectedDate.ToShortDateString()}");
                }
            }
            else
            {
                Console.WriteLine($"Ingen data finns för {selectedDate.ToShortDateString()}");
            }
        }      
    }
}