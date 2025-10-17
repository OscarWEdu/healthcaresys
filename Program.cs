using HealthCareSys;

List<User> Users = new List<User>();
List<Location> Locations = new List<Location>();

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
    CurrentMenu = Menu.ViewLocationSchedule; //Uncomment and change Menu.Main to the menu you want to test
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

}

void AddLocationMenu()
{
    //Check if CSV and Data folder exists, otherwise create
    Location.CheckLocationCSVExists();

    //Requests location details from user, then adds to CSV
    Location.AddLocationToCSV();

    //Clears Locations list and repopulates with data from CSV
    ReadLocations();

    CurrentMenu = Menu.Main;
}

void ViewLocationScheduleMenu()
{
    //Clears Locations list and repopulates with data from CSV. Prob better way to do it but works for testing
    ReadLocations();

    //Displays available locations
    Console.WriteLine("== Appointments at location ==\n");
    Console.WriteLine("At location would you like to view booked appointments for?");
    foreach (Location location in Locations)
    {
        Console.WriteLine($"{location.Name}");
    }
    // String input from user, has to match Location.Name except for casing
    Console.WriteLine("Enter the name of the location you wish to view: ");
    string location_choice = Console.ReadLine();

    // Compares user input againts location.Name, if match display the locations appointments.
    foreach (Location locations in Locations)
    {
        if (location_choice.ToLower() == locations.Name.ToLower())
        {
            location_choice = location_choice.ToLower();
            Console.WriteLine($"{location_choice}'s scheduled appointments");
            break;
        }
        else
        {
            //If user input doesn't match any existing Location.Name, user will be returned to choose location
            Console.WriteLine("Invalid choice, press enter to choose location again...");
            Console.ReadLine();
            CurrentMenu = Menu.ViewLocationSchedule;
            break;
        }

    }
    
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

//Reads locations CSV and populates Locations list
List<Location> ReadLocations()
{
    Locations.Clear();
    string[] lines_locations = File.ReadAllLines(@Path.Combine("Data", "Locations.csv"));
    if (lines_locations != null | lines_locations.Length != 0)
    {
        foreach (string location in lines_locations)
        {
            string[] split_lines_locations = location.Split(';');
            Locations.Add(new Location(split_lines_locations[0], split_lines_locations[1]));
        }
    }
    return Locations;
}