namespace HealthCareSys;

record Event
{
    private string SSN = null;
    private string Password = null; //Do not Serialize, clear on event handled
    private EventType eventType = EventType.None;

    public void UserAdmission(string ssn, string password)
    {
        SSN = ssn;
        Password = password;
        eventType = EventType.Admission;
    }
}