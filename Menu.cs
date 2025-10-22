namespace HealthCareSys;

enum Menu { None, Login, Logout, CreateAccount, Main, ManagePermissions, AddLocation, ViewLocationSchedule, ManageRequest, ManageAppointments, ManageJournal, ViewAdminPermissions, CreatePersonnel, ManageRegistration, AssignRegion, RequestPatientStatus, ViewLocations }

class MenuClass
{
    public Menu? GetMenu(string MenuString)
    {
        Menu output;
        if (Enum.TryParse(MenuString, out output))
        {
            return (Menu)output;
        }

        return null;
    }
}