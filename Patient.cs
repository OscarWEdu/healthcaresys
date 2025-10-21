namespace HealthCareSys;

record Patient(string SSN, string Password) : User(SSN, Password)
{
  public Journal Journal = new Journal(); // Every patient has a thier own journal
}