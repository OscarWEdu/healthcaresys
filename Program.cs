using System.ComponentModel.Design;
using HealthCareSys;

List<User> Users = new List<User>();
List<Location> Locations = new List<Location>();
List<Event> UnhandledEvents = new List<Event>();
List<Event> HandledEvents = new List<Event>();
List<PatientRequest> PatientRequests = new List<PatientRequest>();

Menu CurrentMenu = Menu.None;
User? CurrentUser = null;

//Check if CSV files and Data folder exists, otherwise create
FileHandler.CheckFilesExist();

AddTestData();

bool is_running = true;
while (is_running)
{
    if (CurrentMenu == Menu.None || (CurrentUser == null && CurrentMenu != Menu.CreateAccount))
    {
        CurrentMenu = Menu.Login;
    }
    // CurrentMenu = Menu.Login; //Uncomment and change Menu.* to the menu you want to test
    MenuManager();
    EventHandler();
}

void AddTestData()
{
    Users.Add(new User("user", "user"));
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
    Console.WriteLine("SSN:");
    string Name = Console.ReadLine();
    Console.WriteLine("Password:");
    string Pass = Console.ReadLine();
    CurrentUser = new User(Name, Pass);
    Users.Add(CurrentUser);
    CurrentMenu = Menu.Main;
}

void MainMenu()
{
    Console.Clear();
    Console.WriteLine("Welcome!");

    if (CurrentUser is Admin)
    {
        AdminMainMenu();
    }

    else if (CurrentUser is Personnel)
    {
        PersonnelMainMenu();
    }

    else if (CurrentUser is Patient)
    {
        PatientMainMenu();
    }
    else

    {
        UserMainMenu();
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
    Console.WriteLine("2.Manage appointments request");
    Console.WriteLine("3.Manage appointments");
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
    Console.WriteLine("2.Request appointments");
    Console.WriteLine("3.View appointments");
    Console.WriteLine("4.Request Patient status");
    Console.WriteLine("5.Log out");

    String input = Console.ReadLine();

    switch (input)
    {
        case "1": CurrentMenu = Menu.ManageJournal; break;
        case "2": CurrentMenu = Menu.ManageRequest; break;
        case "3": CurrentMenu = Menu.ManageAppointments; break;
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
    Console.Clear();
    if (CurrentUser is Personnel) //checks if User is Personnel to give access to write in journal
    {
        Console.WriteLine("Enter the patients SSN");
        string patientSSN = Console.ReadLine();
        User user = GetUserByName(patientSSN); // Gets the right persons journal by thier ssn

        if (user is not Patient patient) // If the patient is not found, returns to main menu
        {
            Console.WriteLine("Patient could not be found, press ENTER to return");
            Console.ReadLine();
            CurrentMenu = Menu.Main;
            return; // Exits the method and doesnt continue running.
        }
        Console.WriteLine("Write a title: ");
        string title = Console.ReadLine();

        Console.WriteLine("Write a description: ");
        string description = Console.ReadLine();

        patient.Journal.AddEntry(title, description); // adds the journal entry to the patients journal
        Console.WriteLine("Journal updated. Press Enter to continue");
        Console.ReadLine();
    }

    else if (CurrentUser is Patient patient) // If a patient is logged in, shows thier journal
    {
        List<JournalEntry> entries = patient.Journal.GetEntries(); //get the Journal entriess

        if (entries.Count == 0)
        {
            Console.WriteLine("Your journal is empty.");
        }
        else
        {
            Console.WriteLine("--- Your journal---");
            foreach (JournalEntry entry in entries) // Runs throw journal entries
            {
                Console.WriteLine($"\n[{entry.Timestamp}]{entry.Title}\n{entry.Description}");
            }
        }
        Console.WriteLine("Press ENTER to return");
        Console.ReadLine();
    }

    CurrentMenu = Menu.Main;
}

void ViewAdminPermissionsMenu()
{
    foreach (Admin admin in GetAdmins())
    {
        Console.WriteLine(admin.SSN + "s Admin Permissions:");
        admin.ViewPermissions();
        Console.WriteLine("Press any key to Return to Main Menu");
        Console.ReadLine();
        CurrentMenu = Menu.Main;
    }
}

void CreatePersonnelMenu()
{

}

void ManageRegistrationMenu()
{
    //TODO: Ask User what to do; manage registration, view requests
    ViewPatientRequests();
}

void ViewPatientRequests()
{
    Console.WriteLine("Patient Requests");
    foreach (PatientRequest patientRequest in PatientRequests)
    {
        Console.WriteLine(patientRequest.SSN);
    }
    Console.WriteLine("Type in the SSN of the request you would like to handle");
    string SSN = Console.ReadLine();
    User? patient = Users.Find(x => SSN == x.SSN);
    if (patient is Patient)
    {
        Console.WriteLine("Would you like to accept the registration?");
        if (YesNoQuestion()) { AcceptRegistration((Patient)patient); }
        PatientRequests.Remove(PatientRequests.Find(x => SSN == x.SSN));
    }
}

//Creates a registration event for the given patient
void AcceptRegistration(Patient patient)
{
    Event NewEvent = new Event();
    NewEvent.UserAdmission(patient.SSN);
    UnhandledEvents.Add(NewEvent);
}

void AssignRegionMenu()
{

}

void RequestPatientStatusMenu()
{
    //TODO: Display Locations
    Console.WriteLine("Type the name of the location you would like to register to:");
    string LocationString = Console.ReadLine();
    PatientRequests.Add(new PatientRequest(CurrentUser.SSN, LocationString));
}

void EventHandler()
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

//Reads locations CSV and populates Locations list
List<Location> ReadLocations()
{
    Locations.Clear();
    string[] lines_locations = FileHandler.ReadData("Locations.csv");
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

//Saves all users to file
void SaveUsers()
{
    List<string> UserData = new List<string>();
    foreach (User user in Users)
    {
        UserData.Add(user.Serialize());
    }
    FileHandler.OverrideData("Users.csv", UserData);
}

//Gets all users from file
void LoadUsers()
{
    string[] UserStringArray = FileHandler.ReadData("Users.csv");
    if (UserStringArray != null | UserStringArray.Length != 0)
    {
        Users.Clear();
        foreach (string UserString in UserStringArray)
        {
            string[] UserField = UserString.Split(';');
            User user = new User(UserField[1], UserField[2]);
            if (UserField[0] == typeof(User).Name) { Users.Add(user); }
            if (UserField[0] == typeof(Patient).Name) { Users.Add((Patient)user); }
            if (UserField[0] == typeof(Personnel).Name)
            {
                Personnel personnel = (Personnel)user;
                personnel.ChangePermissions(ParsePermissions(UserField[3]));
            }
            if (UserField[0] == typeof(Admin).Name)
            {
                Admin admin = (Admin)user;
                admin.ChangePermissions(ParsePermissions(UserField[3]));
            }
        }
    }
}

bool[] ParsePermissions(string RawData)
{
    string[] PermsissionStringArray = RawData.Split(",");
    bool[] Permissions = new bool[PermsissionStringArray.Length];
    for (int i = 0; i < PermsissionStringArray.Length; i++)
    {
        string PermissionString = PermsissionStringArray[i];
        bool Permission;
        if (bool.TryParse(PermissionString, out Permission)) { Permissions[i] = Permission; }
    }
    return Permissions;
}
