namespace APBD_Cw1_s31191.Lease;

public class Lease()
{
    public int LeasedItemId { get; private set; }
    public int LesseeId { get; private set; }
    
    public DateTime RentalDate { get; init; } = DateTime.Now;
    public DateTime DueDate { get; init; }
    public DateTime? ActualReturnDate { get; set; }
    
    public decimal BaseFee { get; init; }
    public decimal? PenaltyFee { get; private set; }

    public Lease(int leasedItemId, int lesseeId, DateTime rentalDate, DateTime dueDate,
        decimal baseFee, decimal? penaltyFee, DateTime? actualReturnDate) : this()
    {
        LeasedItemId = leasedItemId;
        LesseeId = lesseeId;
        RentalDate = rentalDate;
        DueDate = dueDate;
        BaseFee = baseFee;
        PenaltyFee = penaltyFee;
        ActualReturnDate = actualReturnDate;
    }
}