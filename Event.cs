namespace HealthCareSys;

record Event
{
    public string? SSN = null;
    public string? Location = null;
    public EventType eventType = EventType.None;

    public void UserAdmission(string ssn, string location)
    {
        SSN = ssn;
        Location = location;
        eventType = EventType.Admission;
    }
}