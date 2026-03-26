namespace APBD_Cw1_s31191;

public abstract class Repository<T> where T : class
{
    private Dictionary<int, T> Entries = new();

    protected void AddEntry(T entry, int id)
    {
       Entries[id] = entry; 
    }

    protected void RemoveEntry(int id)
    {
        Entries.Remove(id);
    }

    protected T? GetEntry(int id)
    {
        return EntryExists(id) ? Entries[id] : null;
    }
    
    protected List<T> GetEntries()
    {
        return Entries.Values.ToList();
    }
    
    public bool EntryExists(int id)
    {
        return Entries.ContainsKey(id);
    }
}