namespace HealthCareSys;

class Location
{
    public Availability?[] OpeningHours = new Availability?[7];
    public List<Availability> Exceptions = new List<Availability>();
    public string Name;
    public string Address;


    //Opening hours of a standard workday
    private int _weekdayStart = 7;
    private int _weekdayEnd = 17;

    public Location(string name, string address)
    {
        Name = name;
        Address = address;
        DefaultOpeningHours();
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
}