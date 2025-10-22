namespace HealthCareSys;

public class Journal
{
  List<JournalEntry> entries = new List<JournalEntry>(); //list of journal entries for a patient

  public void AddEntry(string title, string description) // Adds the entry to the journal
  {
    entries.Add(new JournalEntry(DateTime.Now, title, description));
  }

  public List<JournalEntry> GetEntries() // Returns the journal entries
  {
    return entries;
  }
}