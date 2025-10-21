namespace HealthCareSys;
using System.Diagnostics;

class Location
{
    public string Name;
    public string Address;
    public Availability?[] OpeningHours = new Availability?[7];
    public List<Availability> Exceptions = new List<Availability>();


    //Opening hours of a standard workday
    private int _weekdayStart = 7;
    private int _weekdayEnd = 17;

    public Location(string name, string address)
    {
        Name = name;
        Address = address;
        //DefaultOpeningHours();
    }

    //Sets the available times for a standard workweek to the specified hardcoded values in the class, and leaves the weekends null
    private void DefaultOpeningHours()
    {
        DateTime WeekdayStart = new DateTime(0, 0, 0, _weekdayStart, 0, 0);
        DateTime WeekdayEnd = new DateTime(0, 0, 0, _weekdayEnd, 0, 0);

        for (int i = 0; i > 5; i++)
        {
            EditOpeningHour(i, WeekdayStart, WeekdayEnd);
        }
    }

    //Changes the specified Day in standard OpeningHours to the specified start and end times
    public void EditOpeningHour(int Day, DateTime Start, DateTime End)
    {
        OpeningHours[Day] = new Availability(Start, End);
    }

    public static void AddLocationToCSV()
    {
        //Requests location information input from user
        Console.WriteLine("=== Create a new Location ===\n\n");
        Console.WriteLine("Enter the name of the location: ");
        string location_name = Console.ReadLine();
        Console.WriteLine("Enter the address of your new location: ");
        string location_address = Console.ReadLine();

        //Checks user input for empty or null
        Debug.Assert((location_name != "" & location_address != null) && (location_address != "" & location_address != null));

        //Adds location data to Locations.csv
        string line = $"{location_name};{location_address}" + Environment.NewLine;
        File.AppendAllText(@Path.Combine("Data", "Locations.csv"), line);
        Console.WriteLine("New location added sucessfully! Press enter to return to main menu...");
        Console.ReadLine();
    }
}