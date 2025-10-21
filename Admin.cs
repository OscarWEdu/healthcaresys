namespace HealthCareSys;

record Admin(string SSN, string Password) : User(SSN, Password)
{
    public bool[] Permissions = new bool[Enum.GetNames(typeof(AdminPermission)).Length];

    //Change the specified permission to a given bool
    public void ChangePermission(AdminPermission Permission, bool IsPermitted) { Permissions[(int)Permission] = IsPermitted; }

    // Replaces permissions with a new bool array
    public void ChangePermissions(bool[] NewPermissions) { Permissions = NewPermissions; }

    //Prints Permissions to console
    public void ViewPermissions()
    {
        for (int i = 1; i < Permissions.Length; i++)
        {
            string Output = (AdminPermission)i + ": " + Permissions[i].ToString();
            Console.WriteLine(Output);
        }
    }

    //Returns whether or not the Admin has the permission in question
    public bool HasPermission(AdminPermission permission)
    {
        return Permissions[(int)permission];
    }

    //Returns whether the admin in question can assign a given permission
    public bool CanAssign(AdminPermission permission)
    {
        AdminPermission AssignPermission = GetAssignNeeded(permission);
        if (AssignPermission != AdminPermission.None && HasPermission(AssignPermission)) { return true; }
        else { return false; }
    }

    //Returns the needed permission to assign a given permission
    public AdminPermission GetAssignNeeded(AdminPermission permission)
    {
        switch (permission)
        {
            case AdminPermission.AddLoc: return AdminPermission.AssignAddLoc;
            case AdminPermission.AcceptPatient: return AdminPermission.AssignHandleReg;
            case AdminPermission.DenyPatient: return AdminPermission.AssignHandleReg;
            case AdminPermission.CreatePersAcc: return AdminPermission.AssignCreatePersAcc;
            case AdminPermission.ViewPermissions: return AdminPermission.AssignViewPermissions;
            case AdminPermission.ManagePermissions: return AdminPermission.ManagePermissions;
            default: return AdminPermission.None;
        }
    }
    
    public new string Serialize()
    {
        return SSN + ';' + Password + ';' + string.Join(",", Permissions);
    }
}