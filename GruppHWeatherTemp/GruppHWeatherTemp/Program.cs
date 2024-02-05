namespace GruppHWeatherTemp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Methods.ReadWriteFile.SortData("tempdata.txt");
            Console.ReadKey(true);
        }
    }
}
