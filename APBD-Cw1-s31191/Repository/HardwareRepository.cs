namespace APBD_Cw1_s31191;

public class HardwareRepository : Repository<Hardware>
{
    public void AddHardware(Hardware hardware)
    {
        AddEntry(hardware, hardware.Id);
    }

    public Hardware? GetHardware(int id)
    {
        return GetEntry(id);
    }
    
    public List<Hardware> GetAllHardware()
    {
        return GetEntries();
    }

    public List<Hardware> GetAllAvailableHardware()
    {
        return GetAllHardware().FindAll(hardware => hardware.AvailabilityStatus == AvailabilityStatus.Available);
    }
}