namespace APBD_Cw1_s31191;

public class UserRepository : Repository<User>
{
    public void AddUser(User user)
    {
        AddEntry(user, user.Id);
    }

    public User? GetUser(int id)
    {
        return GetEntry(id);
    }
    
    public List<User> GetAllUsers()
    {
        return GetEntries();
    }
}