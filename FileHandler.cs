namespace HealthCareSys;

using System.Diagnostics;

class DataHandler()
{
    public static void CheckFilesExist()
    {
        //Checks if Data dir exists, otherwise create it
        if (!Directory.Exists("Data"))
        {
            Directory.CreateDirectory("Data");
        }
        //Checks if Locations.csv file exists, otherwise create it
        if (!File.Exists(Path.Combine("Data", "Locations.csv")))
        {
            File.Create(Path.Combine("Data", "Locations.csv")).Close();
        }
        //Add additional CSVs here
    }
}