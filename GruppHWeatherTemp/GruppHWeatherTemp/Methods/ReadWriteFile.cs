using System;
using System.Collections.Generic;
using System.Linq;
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
            using (StreamReader reader = new StreamReader(path + fileName))
            {
                //string pattern = @"(?<year>\d{4})-(?<month>\d{2})-(?<day>\d{2})";
                //string pattern2 = @"(\d{4}-\d{2}-\d{2}).(\d{2}:\d{2}:\d{2}).(\b(?:[iI]nne|[uU]te)\b).(\d{2}.\d{1}),(\d{1,2}|100)";
                //string pattern3 = @"(\d{4}-[0-1][0-9]-[0-3][0-9]).(?<hour>[0-2][0-9]):([0-5][0-9]):([0-5][0-9]).(\b(?:[iI]nne|[uU]te)\b),([0-9]?[0-9][.][0-9]),(\d{1,2}|100)";
                //string pattern4 = @"(?<year>\d{4})-(?<month>[0-1][0-9])-(?<day>[0-3][0-9]).(?<hour>[0-2][0-9]):(?<minute>[0-5][0-9]):(?<second>[0-5][0-9]).(\b(?:Inne|Ute)\b),(?<temperature>[0-9]?[0-9][.][0-9]),(?<humidity>\d{1,2}|100)";
                string pattern5 = @"(?<year>\d{4})-(?<month>0[0-9]|1[0-2])-(?<day>0[0-9]|[12][0-9]|3[01]).(?<hour>[0-2][0-9]):(?<minute>[0-5][0-9]):(?<second>[0-5][0-9]).(\b(?:Inne|Ute)\b),(?<temperature>[0-9]?[0-9][.][0-9]),(?<humidity>\d{1,2}|100)";
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
                            decimal temperature = decimal.Parse(match.Groups["temperature"].Value);
                            int humidity = int.Parse(match.Groups["humidity"].Value);

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
            }
        }
    }
}
