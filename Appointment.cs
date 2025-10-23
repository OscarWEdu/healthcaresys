namespace HealthCareSys;

record Appointment(DateTime Date, string Location, string PatientSSN, string Description);

class AppointmentFunctions
{
  public static Appointment BookAppointment(List<Location> locations, Patient patient)
  {
    Console.WriteLine("These are the available times at your location");

    Location patientlocation = null;

    foreach (Location loc in locations)
    {
      if (Convert.ToString(loc.Name) == patient.Location)
      {
        patientlocation = loc;
        break;
      }
    }
    if (patientlocation == null)
    {
      Console.WriteLine("You're registration hasn't been completed yet, please await approval!");
      Console.ReadLine();
    }

    Console.WriteLine($"{patientlocation.Name}\n");

    DateOnly today = DateOnly.FromDateTime(DateTime.Now);
    TimeOnly closed = new TimeOnly(0, 0, 0);
    List<DateTime> availabletimes = new List<DateTime>();
    DateTime availabletime = new DateTime();

    int indexcounter = 0;
    int userselect = 1;

    for (int i = 0; i < 4; i++)
    {
      
      DateOnly displaydate = today.AddDays(i);
      DayOfWeek displayday = displaydate.DayOfWeek;

      AvailabilityLocation avabilitycurrentday = patientlocation.OpenHours.Find(x => x.WeekDayName == displayday);

      if (avabilitycurrentday.WeekdayOpen == closed)
      {
        Console.WriteLine($"\n{displaydate} {displayday} \nLocation is closed");
        break;
      }

      TimeOnly opentime = avabilitycurrentday.WeekdayOpen;

      Console.WriteLine($"\n{displayday} -- {displaydate}");
      Console.WriteLine("---------------------------------");



      while (avabilitycurrentday.WeekdayOpen <= opentime && avabilitycurrentday.WeekdayClose > opentime)
      {
        availabletime = displaydate.ToDateTime(opentime);
        availabletimes.Add(availabletime);
        Console.WriteLine($"{userselect}. {TimeOnly.FromDateTime(availabletimes[indexcounter])}");
        opentime = opentime.AddHours(1);
        indexcounter++;
        userselect++;
      }
    }
    Console.WriteLine("Please select slot by using the corresponding number;");
    int.TryParse(Console.ReadLine(), out userselect);
    int userselectedindex = userselect--;

    if ((indexcounter < userselectedindex) && !(userselectedindex <= 0))
    {
      Console.WriteLine("Invalid selection.");
      return null;
    }

    Console.WriteLine("Please enter the reason for the appointment");
    string description = Console.ReadLine();

    Appointment appointment = new Appointment(availabletimes[userselectedindex], patient.Location, patient.SSN, description);

    Console.WriteLine($"Appointment booked for /Should be patients name/ on {availabletimes[userselectedindex]} at {patientlocation.Name}.");

    return appointment;
    

  }
}

