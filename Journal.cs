namespace HealthCareSys;

public class Journal
{
  List<JournalEntry> entries = new List<JournalEntry>();

  public void AddEntry(string title, string description)
  {
    entries.Add(new JournalEntry(DateTime.Now, title, description));
  }

  public List<JournalEntry> GetEntries()
  {
    return entries;
  }
}