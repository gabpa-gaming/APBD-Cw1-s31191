namespace APBD_Cw1_s31191;
public abstract class User 
{
    public int Id { get; }
    
    public string FirstName { get; protected set; }
    public string LastName { get; protected set; }
    
    public abstract UserType UserType { get; }
    public abstract int MaxLeaseCount { get; }
    
    private static int _idCount;
    
    protected User(string firstName, string lastName)
    {
       Id = _idCount++;
       FirstName = firstName;
       LastName = lastName;
    }
}
