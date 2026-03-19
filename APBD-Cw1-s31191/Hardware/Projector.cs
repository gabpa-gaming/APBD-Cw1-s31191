namespace APBD_Cw1_s31191;
public class Projector(
    string name,
    string description,
    AvailabilityStatus availabilityStatus,
    double aspectRatio,
    int brightnessInLumens)
    : Hardware(name, description, availabilityStatus)
{
    public double AspectRatio { get; private set; } = aspectRatio;
    public int BrightnessInLumens { get; private set; } = brightnessInLumens;
}
