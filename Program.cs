using HealthCareSys;

List<User> Users = new List<User>();
List<Appointment> Appointments = new List<Appointment>();
List<Location> Locations = new List<Location>();
List<Event> UnhandledEvents = new List<Event>();
List<Event> HandledEvents = new List<Event>();
List<PatientRequest> PatientRequests = new List<PatientRequest>();

Menu CurrentMenu = Menu.None;
User? CurrentUser = null;

//Check if CSV files and Data folder exists, otherwise create
FileHandler.CheckFilesExist();

AddTestData();
LoadJournals();

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
    Users.Add(new Patient("pat", "pat", "Hospital"));
    Locations.Add(new Location("Hospital", "Street 1", ERegion.South));
    Locations.Add(new Location("Screening", "Street 2", ERegion.South));
    Locations.Add(new Location("Utanization", "Street 3", ERegion.North));
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
        case (Menu.ViewLocations): ViewLocationsMenu(); break;
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

    SaveJournals(); //Saves the journals when someone logs out

    // This effectively logs the user out of the system.
    CurrentUser = null;

    // Set the current menu state to Login.
    CurrentMenu = Menu.Login;

    // Pause the application so the user can read the message.
    Console.WriteLine("Press Any Key to return to the login screen...");
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
    Console.WriteLine("5.Add new location");
    Console.WriteLine("9.Log out");
    String input = Console.ReadLine();

    switch (input)
    {
        case "1": CurrentMenu = Menu.ManagePermissions; break;
        case "2": CurrentMenu = Menu.ViewAdminPermissions; break;
        case "3": CurrentMenu = Menu.CreatePersonnel; break;
        case "4": CurrentMenu = Menu.ManageRegistration; break;
        case "5": CurrentMenu = Menu.AddLocation; break;
        case "9": CurrentMenu = Menu.Logout; break;
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

//Allows an admin with sufficient permissions edit a given admins permissions
void ManagePermissionsMenu()
{
    Admin CurrentAdmin = (Admin)CurrentUser;
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
            Console.WriteLine("Write the permission you would like the change, complete with capitalization:");
            string PermissionString = Console.ReadLine();
            AdminPermission permission;
            if (AdminPermission.TryParse(PermissionString, out permission))
            {
                if (CurrentAdmin.CanAssign(permission)) { admin.ChangePermission(permission, !admin.Permissions[(int)permission]); }
                else { Console.WriteLine("You do not have the needed permission to change this admins permission."); }
            }
            else { Console.WriteLine("The written permission does not exist"); }

            Console.WriteLine($"Would you like to keep editing {admin.SSN}s permissions? (Y/N)");
            if (!YesNoQuestion()) { keep_changing = false; CurrentMenu = Menu.Main; }
        }
    }
}

void AddLocationMenu()
{
    if (CurrentUser is Admin)
    {
        //Requests location details from user, then adds to CSV
        Location.AddLocationToCSV();

        //Clears Locations list and repopulates with data from CSV
        ReadLocations();
    }
    else
    {
        Console.WriteLine("You don't have sufficent permissions to add a location. Press enter to return to main menu");
        Console.ReadLine();
        CurrentMenu = Menu.Main;
    }
    Console.WriteLine("Press enter to return to main menu...");
    Console.ReadLine();
    CurrentMenu = Menu.Main;
}

void ViewLocationsMenu()
{
    //Location.ViewLocations(Locations);
    Console.ReadLine();
    CurrentMenu = Menu.ViewLocations;

}

void ViewLocationScheduleMenu()
{
    //Clears Locations list and repopulates with data from CSV. Prob better way to do it but works for testing
    //ReadLocations();

    //Displays available locations
    Console.WriteLine("== Appointments at location ==\n");
    Console.WriteLine("Select location to view appointments at:");
    Location SelectedLocation = Location.SelectLocation(Locations);

    // Compares user input againts location.Name, if match display the locations appointments.

    Console.WriteLine($"{SelectedLocation.Name}'s scheduled appointments");
    

}

void ManageRequestMenu()
{
    Console.Clear();
    Console.WriteLine("Appointment Requests");

    // Check if the list of patient requests is empty
    if (PatientRequests.Count == 0)
    {

        Console.WriteLine("No requests.");
        Console.ReadLine();
        CurrentMenu = Menu.Main;
        return;
    }

    // If there are requests, loop through each one in the PatientRequests list
    foreach (PatientRequest request in PatientRequests)
    {
        // Display the details for each request
        Console.WriteLine("SSN: " + request.SSN + ", Location: " + request.LocationString);
    }

    Console.WriteLine("Enter SSN:");
    string SSN = Console.ReadLine();

    PatientRequest selected = null;
    foreach (PatientRequest patient in PatientRequests)
    {
        if (patient.SSN == SSN)
        {
            selected = patient;
            break;
        }
    }
    if (selected is not null)
    {
        Console.WriteLine("Approve? (Y/N)");

        if (YesNoQuestion())
        {
            // If the user answers Yes
            Console.WriteLine("Enter date (yyyy-MM-dd):");
            string date = Console.ReadLine();
            DateTime appointmentDate;

            // Loop until the user gives a valid date
            while (!DateTime.TryParse(date, out appointmentDate))
            {
                Console.WriteLine("Invalid date format. Please use yyyy-MM-dd:");
                date = Console.ReadLine();
            }

            Console.WriteLine("Description:");
            string description = Console.ReadLine();

            Appointments.Add(new Appointment(appointmentDate, selected.LocationString, selected.SSN, description));
            Console.WriteLine("Appointment created!");
        }
        else
        {
            Console.WriteLine("Request denied and removed.");
        }

        PatientRequests.Remove(selected);
    }
    else
    {
        Console.WriteLine("No request found with that SSN.");
    }

    Console.ReadLine();
}

void ManageAppointmentsMenu()
{
    Console.Clear();
    Console.WriteLine("Your Appointments");

    if (CurrentUser is Patient)
    {
        // If they are a Patient, loop through the global 'Appointments' list
        foreach (Appointment appointment in Appointments)
        {
            // Check if the appointment's SSN matches the current user's SSN
            if (appointment.PatientSSN == CurrentUser.SSN)
            {
                Console.WriteLine("Date: " + appointment.Date);
                Console.WriteLine("Location: " + appointment.Location);
                Console.WriteLine("Description: " + appointment.Description);
            }
        }
    }
    else if (CurrentUser is Personnel)
    {
        // If they are Personnel, loop through the global 'Appointments' list
        foreach (Appointment appointment in Appointments)
        {
            Console.WriteLine("Patient: " + appointment.PatientSSN + ", Date: " + appointment.Date);
            Console.WriteLine("Location: " + appointment.Location);
        }
    }

    Console.ReadLine();
    CurrentMenu = Menu.Main;
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
        SaveJournals();
        Console.WriteLine("Journal updated. Press Enter to continue");
        Console.ReadLine();
    }

    else if (CurrentUser is Patient patient) // If a patient is logged in, shows their journal
    {
        List<JournalEntry> entries = patient.Journal.GetEntries(); //get the Journal entriess

        if (entries.Count == 0)
        {
            Console.WriteLine("Your journal is empty.");
        }
        else
        {
            Console.WriteLine("--- Your journal---");
            foreach (JournalEntry entry in entries) // Runs through journal entries
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
        Console.WriteLine("Press Any Key to Return to Main Menu");
        Console.ReadLine();
        CurrentMenu = Menu.Main;
    }
}

//Lets an admin with sufficient permissions register a User as a Personnel
void CreatePersonnelMenu()
{
    Admin CurrentAdmin = (Admin)CurrentUser;
    if (CurrentAdmin.HasPermission(AdminPermission.CreatePersAcc))
    {
        Console.WriteLine("Users:");
        foreach (User user in Users)
        {
            if (user is not Admin && user is not Patient && user is not Personnel) { Console.WriteLine(user.SSN); }
        }
        Console.WriteLine("Type the name of the user you would like to register as a personnel");
        string InputString = Console.ReadLine();
        ChangeUserToPersonnel(Users.Find(x => InputString == x.SSN));
    }
    else
    {
        Console.WriteLine("You do not have the permission to register personnel.\nPress Any Key to Return to Main Menu");
        Console.ReadLine();
        CurrentMenu = Menu.Main;
    }
}

//Removes a user and re-adds it typed as a personnel
void ChangeUserToPersonnel(User user)
{
    Personnel personnel = (Personnel)user;
    Users.Remove(user);
    Users.Add(personnel);
}

//Removes a user and re-adds it typed as a Patient
void ChangeUserToPatient(User user, string LocationString)
{
    Patient patient = (Patient)user;
    Users.Remove(user);
    Users.Add(patient);
}

//Let Admins handle admission requests for patient at a location
void ManageRegistrationMenu()
{
    Admin CurrentAdmin = (Admin)CurrentUser;
    if (CurrentAdmin.HasPermission(AdminPermission.AcceptPatient) && CurrentAdmin.HasPermission(AdminPermission.DenyPatient))
    {
        ViewPatientRequests();
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
}

//Displays patient requests
void ViewPatientRequests()
{
    Console.WriteLine("Patient Requests");
    foreach (PatientRequest patientRequest in PatientRequests)
    {
        Console.WriteLine(patientRequest.SSN);
    }
}

//Creates a registration event for the given patient
void AcceptRegistration(Patient patient)
{
    foreach (PatientRequest request in PatientRequests)
    {
        // Display the details for each request
        Console.WriteLine("SSN: " + request.SSN + ", Location: " + request.LocationString);
    }
    Console.WriteLine("Enter SSN of the user you would like to accept:");
    string SSN = Console.ReadLine();
    PatientRequest SelectedRequest = PatientRequests.Find(x => SSN == x.SSN);
    if (SelectedRequest != null)
    {
        Event NewEvent = new Event();
        NewEvent.UserAdmission(SelectedRequest.SSN, SelectedRequest.LocationString);
        UnhandledEvents.Add(NewEvent);
    }
}

void AssignRegionMenu()
{
    Console.WriteLine("North Region");
    foreach (Location location in Locations)
    {
        if (location.Region == ERegion.North)
        {
            Console.WriteLine($"{location.Name} - Address: {location.Address}");
        }
    }
    Console.WriteLine("\nSouth Region");
    foreach (Location location in Locations)
    {
        if (location.Region == ERegion.South)
        {
            Console.WriteLine($"{location.Name} - Address: {location.Address}");
        }
    }
    Console.ReadLine();
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
    foreach (Event UnhandledEvent in UnhandledEvents)
    {
        switch (UnhandledEvent.eventType)
        {
            case (EventType.Admission):
                {
                    ChangeUserToPatient(GetUserByName(UnhandledEvent.SSN), UnhandledEvent.Location);
                    break;
                }
        }
    }
    HandledEvents.AddRange(UnhandledEvents);
    UnhandledEvents.Clear();
}

//Returns user with matching ssn, if no match, returns null
User? GetUserByName(string SSN)
{
    foreach (User user in Users)
    {
        if (SSN == user.SSN) { return user; }
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
            int.TryParse(split_lines_locations[2], out int RegionInt);
            Locations.Add(new Location(split_lines_locations[0], split_lines_locations[1], (ERegion)RegionInt));
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

void SaveJournals()
{
    List<string> journalData = new List<string>(); //creates an empty list with journals

    foreach (User user in Users)
    {
        if (user is Patient patient)
        {
            foreach (JournalEntry entry in patient.Journal.GetEntries()) // Checks journal entries in patient journal
            {
                journalData.Add($"{patient.SSN};{entry.Timestamp};{entry.Title};{entry.Description}");
            }
        }
    }
    FileHandler.OverrideData("Journals.csv", journalData); // Replaces old data in csv
}

void LoadJournals() //Loads Journals
{
    string[] journalLines = FileHandler.ReadData("Journals.csv"); //reads the all the lines in journals.csv
    foreach (string line in journalLines) // loops all the lines
    {
        string[] journalfield = line.Split(';');

        string ssn = journalfield[0]; // patients ssn
        DateTime timestamp = DateTime.Parse(journalfield[1]); // Journal entry time
        string title = journalfield[2]; // title of journal entry
        string description = journalfield[3]; // description of journal entry

        Patient? patient = Users.FirstOrDefault(MatchSSNAndIsPatient) as Patient;

        if (patient != null)
        {
            patient.Journal.GetEntries().Add(new JournalEntry(timestamp, title, description)); // Adds the entry in the right patient journal
        }
        bool MatchSSNAndIsPatient(User user)
        {
            return user.SSN == ssn && user is Patient;
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
