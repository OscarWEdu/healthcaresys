namespace HealthCareSys;

record User(string SSN, string Password)
{
    public string Serialize()
    {
        return SSN + ';' + Password;
    }
}