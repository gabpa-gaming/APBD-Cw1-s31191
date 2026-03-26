namespace APBD_Cw1_s31191;

public class UserService(UserRepository repository)
{
    private UserRepository _repository = repository;
    
    public void AddUser(User user)
    {
        _repository.AddUser(user);
    }

    public User? GetUser(int userId)
    {
        return _repository.GetUser(userId);
    }
}