namespace HealthCareSys;
using System.Diagnostics;

class AvailabilityLocation
{
    public DayOfWeek WeekDayName;
    public TimeOnly WeekdayOpen;
    public TimeOnly WeekdayClose;

    public AvailabilityLocation(DayOfWeek dayname, TimeOnly open, TimeOnly close)
    {
        WeekDayName = dayname;
        WeekdayOpen = open;
        WeekdayClose = close;
    }
}

class Location
{
    public string Name;
    public string Address;
    public List<AvailabilityLocation> OpenHours = new List<AvailabilityLocation>();
    private TimeOnly defaultWeekdayOpen = new TimeOnly(8, 0, 0);
    private TimeOnly defaultWeekdayClose = new TimeOnly(18, 0, 0);

    public Location(string name, string address)
    {
        Name = name;
        Address = address;
        SetAvailabilityLocation();
    }



    void SetAvailabilityLocation()
    {


        for (int i = 0; i < 7; i++)
        {
            if (i == 0)
            {
                OpenHours.Add(new AvailabilityLocation(DayOfWeek.Sunday, new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0)));
            }
            else if (i >= 1 && i <= 5)
            {
                DayOfWeek day = (DayOfWeek)i;
                OpenHours.Add(new AvailabilityLocation(day, defaultWeekdayOpen, defaultWeekdayClose));
            }
            else if (i == 6)
            {
                OpenHours.Add(new AvailabilityLocation(DayOfWeek.Sunday, new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0)));
            }

        }
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
        FileHandler.SaveData("Locations.csv", line);
        Console.WriteLine("New location added sucessfully! Press enter to return to main menu...");
        Console.ReadLine();
    }

    public static void ViewLocations(List<Location> locations)
    {
        foreach (Location location in locations)
        {
            Console.WriteLine($"{location.Name}\n{location.Address}");

            foreach (AvailabilityLocation availabilitylocation in location.OpenHours)
            {
                Console.Write($"{availabilitylocation.WeekDayName} ");
                if (availabilitylocation.WeekdayOpen == new TimeOnly(0, 0, 0))
                {
                    Console.Write($"Closed\n\n");
                }
                else
                {
                    Console.Write($"{availabilitylocation.WeekdayOpen} {availabilitylocation.WeekdayClose}\n\n");
                }
            }

        }
    }
}