namespace HealthCareSys;

record Personnel(string SSN, string Password) : User(SSN, Password)
{
    public bool[] Permissions = new bool[Enum.GetNames(typeof(PersonnelPermission)).Length];

    //Change the specified permission to a given bool
    public void ChangePermission(PersonnelPermission Permission, bool IsPermitted) { Permissions[(int)Permission] = IsPermitted; }

    // Replaces permissions with a new bool array
    public void ChangePermissions(bool[] NewPermissions) { Permissions = NewPermissions; }
    
    //Prints Permissions to console
    public void ViewPermissions()
    {
        for (int i = 1; i < Permissions.Length; i++)
        {
            string Output = (PersonnelPermission) i + ": " + Permissions[i].ToString();
            Console.WriteLine(Output);
        }
    }
}