namespace HealthCareSys;

record Patient(string SSN, string Password) : User(SSN, Password)
{
    public new string Serialize()
    {
        return SSN + ';' + Password;
    }
}