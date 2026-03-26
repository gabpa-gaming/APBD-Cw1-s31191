namespace APBD_Cw1_s31191;

public enum AvailabilityStatus
{
    Available,
    Leased,
    Unavailable
}
public abstract class Hardware
{
    public int Id { get; private set; }
    public AvailabilityStatus AvailabilityStatus { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    private static int _idCount;
    protected Hardware(string name, string description, AvailabilityStatus availabilityStatus)
    {
        Name = name;
        Description = description; 
        AvailabilityStatus = availabilityStatus;
        Id = _idCount++;
    }
}