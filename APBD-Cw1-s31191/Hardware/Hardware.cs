namespace APBD_Cw1_s31191;

public abstract class Hardware
{
    public int Id { get; private set; }
    private static int _idCount;

    protected Hardware()
    {
       Id = _idCount++;
    }
}