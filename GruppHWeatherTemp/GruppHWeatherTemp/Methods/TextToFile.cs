using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruppHWeatherTemp.Methods
{
    internal class TextToFile
    {
        public delegate string MyDelegateStr(string text);
        public static string SaveToFile(string text)
        {
            string logFilePath = "../../../Files/log.txt";

            // Check if the log file exists
            if (!File.Exists(logFilePath))
            {
                // If the log file does not exist, create it and write the data
                File.WriteAllText(logFilePath, DateTime.Now + "\t" + text + Environment.NewLine);
                return text + " sparat";
            }
            else
            {
                // Read existing contents of the log file
                string[] existingLines = File.ReadAllLines(logFilePath);

                // Check if the text already exists in the log file
                if (Array.Exists(existingLines, line => line.Contains(text)))
                {
                    return "Data already exists in log file. Not writing duplicate entry.";
                }
                else
                {
                    // Append the text to the log file if it doesn't already exist
                    File.AppendAllText(logFilePath, DateTime.Now + "\t" + text + Environment.NewLine);
                    return text + " sparat";
                }
            }
        }
    }
}
