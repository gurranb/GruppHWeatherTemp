namespace GruppHWeatherTemp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Methods.ReadWriteFile.MonthlyTemp("tempdata.txt");
            Methods.ReadWriteFile.DailyMidTemp("tempdata.txt");

            //Methods.ReadWriteFile.SortData("tempdata.txt");
            Console.ReadKey(true);
              

        }
    }
}
