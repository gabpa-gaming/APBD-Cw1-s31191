namespace APBD_Cw1_s31191;

public class Laptop(
    string name,
    string description,
    AvailabilityStatus availabilityStatus,
    string processorName,
    double inchScreenSize)
    : Hardware(name, description, availabilityStatus)
{
    public string ProcessorName { get; private set; } = processorName;
    public double InchScreenSize { get; private set; } = inchScreenSize;
}