namespace APBD_Cw1_s31191;

public class Camera(
    string name,
    string description,
    AvailabilityStatus availabilityStatus,
    string lensKind,
    double megapixelCount)
    : Hardware(name, description, availabilityStatus)
{
    public string LensKind { get; private set; } = lensKind;
    public double MegapixelCount { get; private set; } = megapixelCount;
}

