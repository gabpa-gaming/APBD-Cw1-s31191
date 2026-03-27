namespace APBD_Cw1_s31191;

public class Lease()
{
    public int LeasedItemId { get; private set; }
    public int LesseeId { get; private set; }
    public int JoinedLeaseId => ConvertIdsToJoinedLeaseId(LesseeId, LeasedItemId);
    public DateTime RentalDate { get; init; } = DateTime.Now;
    public DateTime DueDate { get; init; }
    
    public decimal BaseFee { get; init; }
    public decimal DailyPenaltyFee { get; private set; }

    public Lease(int leasedItemId, int lesseeId, DateTime rentalDate, DateTime dueDate,
        decimal baseFee, decimal dailyPenaltyFee) : this()
    {
        LeasedItemId = leasedItemId;
        LesseeId = lesseeId;
        RentalDate = rentalDate;
        DueDate = dueDate;
        BaseFee = baseFee;
        DailyPenaltyFee = dailyPenaltyFee;
    }
    
    public decimal CalculateReturnCost(DateTime today)
    {
        if (DueDate > today)
        {
            return BaseFee;
        }
        var timeLate = today - DueDate;
        var daysLate = timeLate.Days;
        return BaseFee + daysLate * DailyPenaltyFee;
    }
    public static int ConvertIdsToJoinedLeaseId(int userId, int leasedItemId)
    {
        return leasedItemId | userId << 16;
    }
}