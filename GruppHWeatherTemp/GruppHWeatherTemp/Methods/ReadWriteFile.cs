using GruppHWeatherTemp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GruppHWeatherTemp.Methods
{
    internal class ReadWriteFile
    {
        public static string path = "../../../Files/";



        public static void SortData(string fileName)
        {
            //List<string> lines = ReadFileToList(path,"tempdata.txt");
            using (StreamReader reader = new StreamReader(path + fileName))
            {

                //string pattern = @"(?<year>\d{4})-(?<month>\d{2})-(?<day>\d{2})";
                //string pattern2 = @"(\d{4}-\d{2}-\d{2}).(\d{2}:\d{2}:\d{2}).(\b(?:[iI]nne|[uU]te)\b).(\d{2}.\d{1}),(\d{1,2}|100)";
                //string pattern3 = @"(\d{4}-[0-1][0-9]-[0-3][0-9]).(?<hour>[0-2][0-9]):([0-5][0-9]):([0-5][0-9]).(\b(?:[iI]nne|[uU]te)\b),([0-9]?[0-9][.][0-9]),(\d{1,2}|100)";
                //string pattern4 = @"(?<year>\d{4})-(?<month>[0-1][0-9])-(?<day>[0-3][0-9]).(?<hour>[0-2][0-9]):(?<minute>[0-5][0-9]):(?<second>[0-5][0-9]).(\b(?:Inne|Ute)\b),(?<temperature>[0-9]?[0-9][.][0-9]),(?<humidity>\d{1,2}|100)";
                string pattern5 = @"(?<year>\d{4})-(?<month>0[0-9]|1[0-2])-(?<day>0[0-9]|[12][0-9]|3[01]).(?<hour>[0-2][0-9]):(?<minute>[0-5][0-9]):(?<second>[0-5][0-9]).(?<location>(?<=\b)(?:Inne|Ute)(?=\b)),(?<temperature>[0-9]?[0-9][.][0-9]),(?<humidity>\d{1,2}|100)";
                int rowCount = 1;

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();                    

                    List<int> years = new List<int>();
                    List<int> months = new List<int>();
                    List<int> days = new List<int>();
                    List<int> hours = new List<int>();
                    List<int> minutes = new List<int>();
                    List<int> seconds = new List<int>();                     
                    List<decimal> temperatures = new List<decimal>();
                    List<WeatherTools> tools = new List<WeatherTools>();                 



                    foreach (var text in line)
                    {
                        Match match = Regex.Match(line, pattern5);

                        if (match.Success)
                        {
                            int year = int.Parse(match.Groups["year"].Value);
                            int month = int.Parse(match.Groups["month"].Value);
                            int day = int.Parse(match.Groups["day"].Value);
                            int hour = int.Parse(match.Groups["hour"].Value);
                            int minute = int.Parse(match.Groups["minute"].Value);
                            int second = int.Parse(match.Groups["second"].Value);
                            bool location = match.Groups["location"].Value.ToLower() == "Inne";
                            double temperature = double.Parse(match.Groups["temperature"].Value);
                            double humidity = double.Parse(match.Groups["humidity"].Value);

                            years.Add(year);
                            tools.Add(new WeatherTools(location,humidity,temperature));
                            //string result = ((humidity - 78) * (temperature / 15)) / 0.22;
                            if (year == 2017) 
                            {
                                Console.WriteLine( ((humidity - 78) * (temperature / 15)) / 0.22);
                            }

                            //else
                            //{
                            //    Console.WriteLine("Error");
                            //}
                            rowCount++;

                           
                        }
                        
                    }
                    
                }
            }
        }
        static List<string> ReadFileToList(string path,string fileName)
        {
            List<string> lines = new List<string>();

            try
            {
                using (StreamReader reader = new StreamReader(path + fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"An error occurred while reading the file: {e.Message}");
            }

            return lines;
        }
        public static List<WeatherTools> Tools(string fileName)
        {
            List<WeatherTools> tools = new List<WeatherTools>();
            using (StreamReader reader = new StreamReader(path + fileName))
            {
                //string pattern = @"(?<year>\d{4})-(?<month>\d{2})-(?<day>\d{2})";
                //string pattern2 = @"(\d{4}-\d{2}-\d{2}).(\d{2}:\d{2}:\d{2}).(\b(?:[iI]nne|[uU]te)\b).(\d{2}.\d{1}),(\d{1,2}|100)";
                //string pattern3 = @"(\d{4}-[0-1][0-9]-[0-3][0-9]).(?<hour>[0-2][0-9]):([0-5][0-9]):([0-5][0-9]).(\b(?:[iI]nne|[uU]te)\b),([0-9]?[0-9][.][0-9]),(\d{1,2}|100)";
                //string pattern4 = @"(?<year>\d{4})-(?<month>[0-1][0-9])-(?<day>[0-3][0-9]).(?<hour>[0-2][0-9]):(?<minute>[0-5][0-9]):(?<second>[0-5][0-9]).(\b(?:Inne|Ute)\b),(?<temperature>[0-9]?[0-9][.][0-9]),(?<humidity>\d{1,2}|100)";
                string pattern5 = @"(?<year>\d{4})-(?<month>0[0-9]|1[0-2])-(?<day>0[0-9]|[12][0-9]|3[01]).(?<hour>[0-2][0-9]):(?<minute>[0-5][0-9]):(?<second>[0-5][0-9]).(?<location>(?:Inne|Ute)\b),(?<temperature>[0-9]?[0-9][.][0-9]),(?<humidity>\d{1,2}|100)";
                int rowCount = 1;
                while (!reader.EndOfStream)
                {

                    string line = reader.ReadLine();
                    MatchCollection matches = Regex.Matches(line, pattern5);                              


                    foreach (Match match in matches)
                    {
                        if (match.Success)
                        {
                            int year = int.Parse(match.Groups["year"].Value);
                            int month = int.Parse(match.Groups["month"].Value);
                            int day = int.Parse(match.Groups["day"].Value);
                            int hour = int.Parse(match.Groups["hour"].Value);
                            int minute = int.Parse(match.Groups["minute"].Value);
                            int second = int.Parse(match.Groups["second"].Value);
                            bool location = match.Groups["location"].Value.ToLower() == "Inne";
                            double temperature = double.Parse(match.Groups["temperature"].Value);
                            double humidity = double.Parse(match.Groups["humidity"].Value);

                        
                            tools.Add(new WeatherTools(location, humidity,temperature));

                            if (day < 31)
                            {
                                Console.WriteLine(rowCount + " " + line);
                            }
                        }
                        else
                        {
                            Console.WriteLine(rowCount + " " + line + "ERRORRRRRRRRRRRR");
                            Console.ReadKey(true);
                        }
                    }
                    rowCount++;
                }
            } return tools;         




        }
        public static void MonthlyTemp(string fileName)
        {
            string pattern4 = @"(?<year>\d{4})-(?<month>0[0-9]|1[0-2])-(?<day>0[0-9]|[12][0-9]|3[01]).(?<hour>[0-2][0-9]):(?<minute>[0-5][0-9]):(?<second>[0-5][0-9]).(?<location>(?:Inne|Ute)\b),(?<temperature>[0-9]?[0-9][.][0-9]),(?<humidity>\d{1,2}|100)";
            //string pattern5 = @"(?<year>\d{4})-(?<month>0[0-9]|1[0-2])-(?<day>0[0-9]|[12][0-9]|3[01]).(?<hour>[0-2][0-9]):(?<minute>[0-5][0-9]):(?<second>[0-5][0-9]).(?<location>(?:Inne|Ute)\b),(?<temperature>[0-9]?[\d.][0-9]),(?<humidity>\d{1,2}|100)";
            Dictionary<string, Dictionary<string, List<string>>> monthlyTemp = new Dictionary<string, Dictionary<string, List<string>>>();
            using (StreamReader reader = new StreamReader(path + fileName))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    Match match = Regex.Match(line, pattern4);
                    if (match.Success)
                    {
                        string month = match.Groups["month"].Value;
                        string location = match.Groups["location"].Value;
                        string temperature = match.Groups["temperature"].Value;
                        string monthly = month.Substring(0, 2);
                        if (!monthlyTemp.ContainsKey(monthly))
                        {
                            monthlyTemp.Add(monthly, new Dictionary<string, List<string>>());

                        }
                        if (!monthlyTemp[monthly].ContainsKey(location))
                        {
                            monthlyTemp[monthly].Add(location, new List<string>());
                        }
                        monthlyTemp[monthly][location].Add(temperature);
                    }
                }
            }
            Dictionary<string, Dictionary<string, string>> medianTemp = new Dictionary<string, Dictionary<string, string>>();
            foreach (var month in monthlyTemp)
            {
                medianTemp.Add(month.Key, new Dictionary<string, string>());
                foreach (var location in month.Value)
                {
                    medianTemp[month.Key].Add(location.Key, location.Value.OrderBy(t => t).Skip(location.Value.Count / 2).First());
                }
            }
            foreach (var month in medianTemp)
            {
                Console.WriteLine($"Month: {month.Key}");
                foreach (var location in month.Value)
                {
                    Console.WriteLine($" Location: {location.Key}, Median temp: {location.Value}");
                }
            }
           
        }
        public static void DailyMidTemp(string fileName)
        {
            string pattern = @"(?<year>\d{4})-(?<month>0[0-9]|1[0-2])-(?<day>0[0-9]|[12][0-9]|3[01]).(?<hour>[0-2][0-9]):(?<minute>[0-5][0-9]):(?<second>[0-5][0-9]).(?<location>(?:Inne|Ute)\b),(?<temperature>[0-9]?[0-9][.][0-9]),(?<humidity>\d{1,2}|100)";
            Dictionary<string, Dictionary<string, List<string>>> dailyData = new Dictionary<string, Dictionary<string, List<string>>>();

            using (StreamReader reader = new StreamReader(path + fileName))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    Match match = Regex.Match(line, pattern);

                    if (match.Success)
                    {
                        string month = match.Groups["month"].Value;
                        string day = match.Groups["day"].Value;
                        string location = match.Groups["location"].Value;
                        string temperature = match.Groups["temperature"].Value;
                        string humidity = match.Groups["humidity"].Value;
                        string dateKey = month + day;

                        if (!dailyData.ContainsKey(dateKey))
                        {
                            dailyData.Add(dateKey, new Dictionary<string, List<string>>());
                        }

                        if (!dailyData[dateKey].ContainsKey(location))
                        {
                            dailyData[dateKey].Add(location, new List<string>());
                        }

                        dailyData[dateKey][location].Add($"{temperature},{humidity}");
                    }
                }
            }

            Dictionary<string, Dictionary<string, string>> medianData = new Dictionary<string, Dictionary<string, string>>();

            foreach (var date in dailyData)
            {
                medianData.Add(date.Key, new Dictionary<string, string>());

                foreach (var location in date.Value)
                {
                    List<string> tempHumidityList = location.Value;
                    List<double> temperatures = tempHumidityList.Select(entry => double.Parse(entry.Split(',')[0])).ToList();
                    List<int> humidities = tempHumidityList.Select(entry => int.Parse(entry.Split(',')[1])).ToList();

                    // Calculate median temperature
                    double medianTemperature = temperatures.OrderBy(t => t).Skip(temperatures.Count / 2).First();
                    // Calculate median humidity
                    int medianHumidity = humidities.OrderBy(h => h).Skip(humidities.Count / 2).First();

                    medianData[date.Key].Add(location.Key, $"{medianTemperature},{medianHumidity}");
                }
            }

            // Print results
            foreach (var date in medianData)
            {
                Console.WriteLine($"Date: {date.Key}");
                foreach (var location in date.Value)
                {
                    Console.WriteLine($" Location: {location.Key}, Median temp/humidity: {location.Value}");
                }
            }
        }

    }
    }

