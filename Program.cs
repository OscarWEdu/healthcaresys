using System.ComponentModel.Design;
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
    //CurrentMenu = Menu.Main; //Uncomment and change Menu.Main to the menu you want to test
    MenuManager();
    is_running = false;
}

void AddTestData()
{
    Users.Add(new Admin("admin", "admin"));
    Users.Add(new Personnel("pers", "pers"));
    Users.Add(new Patient("pat", "pat"));
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
    Console.WriteLine("Enter your username to login to your account, or type \"new\" to create a new account:");
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
        if (user.Username == Name)
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
    // Clear the console window for a clean interface.
    Console.Clear();

    // Display a goodbye message. 
    Console.WriteLine("Goodbye! You have been logged out.");


    // This effectively logs the user out of the system.
    CurrentUser = null;

    // Set the current menu state to Login.
    CurrentMenu = Menu.Login;

    // Pause the application so the user can read the message.
    Console.WriteLine("Press Enter to return to the login screen...");
    Console.ReadLine();
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
    Console.Clear();
    Console.WriteLine(Welcome!);

    if (CurrentUser is Admin)
    {
        AdminMainMenu()
    }

    else if (CurrentUser is Personnel)
    {
        PersonnelMainMenu()
    }

    else if (CurrentUser is Patient)
    {
        PatientMainMenu()
    }
    else

    {
        UserMainMenu()
    }

}

void AdminMainMenu()
{
    Console.WriteLine("Admin Menu");
    Console.WriteLine("1.Manage Permissions");
    Console.WriteLine("2.View all Permissions");
    Console.WriteLine("3.Create Personnel account");
    Console.WriteLine("4.Manage Patient Registrations");
    Console.WriteLine("5.Log out");
    String input = Console.ReadLine();

    switch (input)
    {
        case "1": CurrentMenu = Menu.ManagePermissions; break;
        case "2": CurrentMenu = Menu.ViewAdminPermissions; break;
        case "3": CurrentMenu = Menu.CreatePersonnel; break;
        case "4": CurrentMenu = Menu.ManageRegistration; break;
        case "5": CurrentMenu = Menu.Logout; break;
        default: Console.WriteLine("Please pick a vaild option"); break;
    }
}
void PersonnelMainMenu()
{
    Console.WriteLine("Personnel Menu");
    Console.WriteLine("1.Veiw location schedule");
    Console.WriteLine("2.Manage appointsments request");
    Console.WriteLine("3.Manage appointsments");
    Console.WriteLine("4.Write in journal");
    Console.WriteLine("5.Log out");

    String input = Console.ReadLine();

    switch (input)
    {
        case "1": CurrentMenu = Menu.ViewLocationSchedule; break;
        case "2": CurrentMenu = Menu.ManageRequest; break;
        case "3": CurrentMenu = Menu.ManageAppointments; break;
        case "4": CurrentMenu = Menu.ManageJournal; break;
        case "5": CurrentMenu = Menu.Logout; break;
        default: Console.WriteLine("Please pick a vaild option"); break;
    }
}

void PatientMainMenu()
{
    Console.WriteLine("Patient Menu");
    Console.WriteLine("1.View Journal");
    Console.WriteLine("2.Request appiontments");
    Console.WriteLine("3.View appiontments");
    Console.WriteLine("4.Request Patient status");
    Console.WriteLine("5.Log out");

    String input = Console.ReadLine();

    switch (input)
    {
        case "1": CurrentMenu = Menu.ManageJournal; break;
        case "2": CurrentMenu = Menu.ManageRequest; break;
        case "3": CurrentMenu = Menu.ViewLocationSchedule; break;
        case "4": CurrentMenu = Menu.RequestPatientStatus; break;
        case "5": CurrentMenu = Menu.Logout; break;
        default: Console.WriteLine("Please pick a vaild option"); break;

    }
}
void UserMainMenu()
{
    Console.WriteLine("Unknown user!");
    Console.WriteLine("1.Request Patient status");
    Console.WriteLine("2.Log out");
    String input = Console.ReadLine();

    switch (input)
    {
        case "1": CurrentMenu = Menu.RequestPatientStatus; break;
        case "2": CurrentMenu = Menu.Logout; break;
        default: Console.WriteLine("Please pick a vaild option"); break;
    }
}

    void ManagePermissionsMenu()
    {

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
        Console.WriteLine(admin.Username + "s Admin Permissions:");
        admin.ChangePermission(AdminPermission.AddLoc, true);
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