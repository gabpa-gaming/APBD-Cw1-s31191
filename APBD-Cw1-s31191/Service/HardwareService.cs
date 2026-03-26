namespace APBD_Cw1_s31191;

public class HardwareService
{
    private HardwareRepository _repository;
    
    public void AddHardware(Hardware hardware)
    {
        _repository.AddHardware(hardware);
    }

    public Hardware? GetHardware(int id)
    {
        return _repository.GetHardware(id);
    }
}