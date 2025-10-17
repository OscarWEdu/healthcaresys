namespace HealthCareSys;

record Patient(string SSN, string Password) : User(SSN, Password)
{
    
}