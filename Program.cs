using HealthCareSys;

List<User> Users = new List<User>();

Menu CurrentMenu = Menu.None;
User? CurrentUser = null;

bool is_running = true;
while (is_running)
{
    AddTestData();

    if (CurrentMenu == Menu.None || (CurrentUser == null && CurrentMenu != Menu.CreateAccount))
    {
        CurrentMenu = Menu.Login;
    }
    CurrentMenu = Menu.ManagePermissions; //Uncomment and change Menu.Main to the menu you want to test
    MenuManager();
    is_running = false;
}

void AddTestData()
{
    Users.Add(new Admin("admin", "admin"));
    Users.Add(new Personnel("pers", "pers"));
    Users.Add(new Patient("pat", "pat"));
}

//Reads user input until the user has answered either yes or no, and returns a bool
bool YesNoQuestion()
{
    bool NotAnswered = true;
    while (NotAnswered)
    {
        string UserInput = Console.ReadLine().ToLower();
        if (UserInput == "y" || UserInput == "yes") { return true; }
        if (UserInput == "n" || UserInput == "no") { return false; }
        Console.WriteLine("Please only type \"yes\" or \"no\"");
    }
    return false;
}

//Executes the *Menu Method corresponding with the CurrentMenu variable
void MenuManager()
{
    switch (CurrentMenu)
    {
        case (Menu.Login): LoginMenu(); break;
        case (Menu.Logout): LogoutMenu(); break;
        case (Menu.CreateAccount): CreateAccountMenu(); break;
        case (Menu.Main): MainMenu(); break;
        case (Menu.ManagePermissions): ManagePermissionsMenu(); break;
        case (Menu.AddLocation): AddLocationMenu(); break;
        case (Menu.ViewLocationSchedule): ViewLocationScheduleMenu(); break;
        case (Menu.ManageRequest): ManageRequestMenu(); break;
        case (Menu.ManageAppointments): ManageAppointmentsMenu(); break;
        case (Menu.ManageJournal): ManageJournalMenu(); break;
        case (Menu.ViewAdminPermissions): ViewAdminPermissionsMenu(); break;
        case (Menu.CreatePersonnel): CreatePersonnelMenu(); break;
        case (Menu.ManageRegistration): ManageRegistrationMenu(); break;
        case (Menu.AssignRegion): AssignRegionMenu(); break;
        case (Menu.RequestPatientStatus): RequestPatientStatusMenu(); break;
    }
}

//Handles User login
void LoginMenu()
{
    Console.WriteLine("Enter your SSN to login to your account, or type \"new\" to create a new account:");
    string Name = Console.ReadLine();
    if (string.Equals(Name, "new")) { CurrentMenu = Menu.CreateAccount; }
    else
    {
        Console.WriteLine("Password:");
        string Pass = Console.ReadLine();
        CurrentUser = FindUser(Name, Pass);
        if (CurrentUser == null) { Console.WriteLine("Name or Password is incorrect"); }
        else { CurrentMenu = Menu.Main; }
    }
}

//Fetches a User with a given username and password
User? FindUser(string Name, string Pass)
{
    foreach (User user in Users)
    {
        if (user.SSN == Name)
        {
            if (user.Password == Pass)
            {
                return user;
            }
        }
    }
    return null;
}

void LogoutMenu()
{
     //Forget Current User
    CurrentUser = null;

    //Return to Login Menu
    CurrentMenu = Menu.Login;

    Console.WriteLine ("You have been logged out!");
}

//Handles Account Creation
void CreateAccountMenu()
{
    Console.Clear();
    Console.WriteLine("Username:");
    string Name = Console.ReadLine();
    Console.WriteLine("Password:");
    string Pass = Console.ReadLine();
    CurrentUser = new Patient(Name, Pass);
    Users.Add(CurrentUser);
    CurrentMenu = Menu.Main;
}

void MainMenu()
{
    Console.WriteLine("Main Menu");
}

void ManagePermissionsMenu()
{
    Console.WriteLine("Admins:");
    foreach (Admin user in GetAdmins()) { Console.WriteLine(user.SSN); }
    Console.WriteLine("Type out the username of the Admin you would like to manage:");
    string Username = Console.ReadLine();
    Admin admin = (Admin)GetUserByName(Username);
    if (admin == null) { Console.WriteLine("Invalid Input"); }
    else
    {
        Console.WriteLine(admin.SSN + "s Admin Permissions:");
        bool keep_changing = true;
        while (keep_changing)
        {
            admin.ViewPermissions();
            Console.WriteLine("Write the permssion you would like the change, complete with capitalization:");
            string PermissionString = Console.ReadLine();
            AdminPermission permission;
            if (AdminPermission.TryParse(PermissionString, out permission)) { admin.ChangePermission(permission, !admin.Permissions[(int)permission]); }
            else { Console.WriteLine("The written permission does not exist"); }

            Console.WriteLine($"Would you like to keep editing {admin.SSN}s permissions? (Y/N)");
            if (!YesNoQuestion()) { keep_changing = false; CurrentMenu = Menu.Main; }
        }
    }
}

void AddLocationMenu()
{

}

void ViewLocationScheduleMenu()
{

}

void ManageRequestMenu()
{

}

void ManageAppointmentsMenu()
{

}

void ManageJournalMenu()
{

}

void ViewAdminPermissionsMenu()
{
    foreach (Admin admin in GetAdmins())
    {
        Console.WriteLine(admin.SSN + "s Admin Permissions:");
        admin.ViewPermissions();
    }
}

void CreatePersonnelMenu()
{

}

void ManageRegistrationMenu()
{

}

void AssignRegionMenu()
{

}

void RequestPatientStatusMenu()
{

}

//Returns user with matching username, if no match, returns null
User? GetUserByName(string username)
{
    foreach (User user in Users)
    {
        if (username == user.SSN) { return user; }
    }
    return null;
}

//Gets all Patients
List<Patient> GetPatients()
{
    List<Patient> Patients = new List<Patient>();
    foreach (User user in Users)
    {
        if (user is Patient) { Patients.Add((Patient)user); }
    }
    return Patients;
}

//Gets all Personnel
List<Personnel> GetPersonnel()
{
    List<Personnel> Personnel = new List<Personnel>();
    foreach (User user in Users)
    {
        if (user is Personnel) { Personnel.Add((Personnel)user); }
    }
    return Personnel;
}

//Gets all Admins
List<Admin> GetAdmins()
{
    List<Admin> Admins = new List<Admin>();
    foreach (User user in Users)
    {
        if (user is Admin) { Admins.Add((Admin)user); }
    }
    return Admins;
}