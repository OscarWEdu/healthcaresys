namespace HealthCareSys;

record Event
{
    private string SSN = null;
    private EventType eventType = EventType.None;

    public void UserAdmission(string ssn)
    {
        SSN = ssn;
        eventType = EventType.Admission;
    }
}