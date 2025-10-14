namespace HealthCareSys;

class Availability
{
    public DateTime Start;
    public DateTime End;

    public Availability(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }
}