namespace APBD_Cw1_s31191;

public class HardwareService(HardwareRepository repository)
{
    private HardwareRepository _repository = repository;
    
    public void AddHardware(Hardware hardware)
    {
        _repository.AddHardware(hardware);
    }

    public Hardware? GetHardware(int id)
    {
        return _repository.GetHardware(id);
    }
    
    public List<Hardware> GetAllHardware()
    {
        return _repository.GetAllHardware();
    }

    public List<Hardware> GetAllAvailbleHardware()
    {
        return _repository.GetAllAvailbleHardware();
    }
}