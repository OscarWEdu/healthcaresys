namespace HealthCareSys;

using System.Diagnostics;

class DataHandler()
{
    static string FileDirectory = "Data";
    public static void CheckFilesExist()
    {
        //Checks if Data dir exists, otherwise create it
        if (!Directory.Exists(FileDirectory))
        {
            Directory.CreateDirectory(FileDirectory);
        }
        //Checks if Locations.csv file exists, otherwise create it
        if (!File.Exists(Path.Combine(FileDirectory, "Locations.csv")))
        {
            File.Create(Path.Combine(FileDirectory, "Locations.csv")).Close();
        }
        //Add additional CSVs here
    }

    /// <summary>
    /// Reads the data of a specified file
    /// </summary>
    /// <param name="FileName">The name of the file</param>
    /// <returns>The data of the file in a string array</returns>
    public static string[] ReadData(string FileName)
    {
        return File.ReadAllLines(@Path.Combine("Data", FileName));
    }

    /// <summary>
    /// Adds a string of data to the specified file.
    /// </summary>
    /// <param name="FileName">The name of the file</param>
    /// <param name="FileData">The string to save</param>
    public static void SaveData(string FileName, string FileData)
    {
        File.AppendAllText(@Path.Combine(FileDirectory, FileName), FileData);
    }

    /// <summary>
    /// Overrides the data of the specified file with a given List of strings
    /// </summary>
    /// <param name="FileName">The name of the file</param>
    /// <param name="FileData">A list of strings to save</param>
    public static void OverrideData(string FileName, List<string> FileData)
    {
        File.WriteAllLines(@Path.Combine(FileDirectory, FileName), FileData);
    }
}