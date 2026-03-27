namespace APBD_Cw1_s31191;

public enum LeaseResult
{
    UserNotFound,
    HardwareNotFound,
    HardwareUnavailable,
    UserLeaseLimitReached,
    LeaseSuccess
}

public enum ReturnResult
{
    ItemNotLeased,
    ReturnSuccess,
}

public class LeaseService(
    HardwareService hardwareService,
    UserService userService,
    LeaseRepository leaseRepository)
{
    private HardwareService _hardwareService = hardwareService;
    private UserService _userService = userService;
    private LeaseRepository _leaseRepository = leaseRepository;

    public List<Lease> GetUserLeases(int userId)
    {
        return _leaseRepository.GetLeasesByUserId(userId);
    }

    public List<Lease> GetOverdueLeases(DateTime today)
    {
        return _leaseRepository.GetAllLeases().FindAll(lease => lease.DueDate < today);
    }
    
    public LeaseResult LeaseHardware(DateTime today, int itemId, int userId, DateTime dueDate,
        decimal baseFee, decimal dailyPenaltyFee)
    {
        var user = _userService.GetUser(userId);
        if (user == null)
        {
            return LeaseResult.UserNotFound;
        }
        if (_leaseRepository.GetUserLeaseCount(userId) >= user.MaxLeaseCount)
        {
            return LeaseResult.UserLeaseLimitReached;
        }
        var hardware = _hardwareService.GetHardware(itemId);
        if (hardware == null)
        {
            return LeaseResult.HardwareNotFound;
        }
        if (hardware.AvailabilityStatus != AvailabilityStatus.Available)
        {
            return LeaseResult.HardwareUnavailable;
        }
        _leaseRepository.AddLease(new Lease(itemId, userId, today, dueDate, baseFee, dailyPenaltyFee));
        hardware.AvailabilityStatus = AvailabilityStatus.Leased;
        return LeaseResult.LeaseSuccess;
    }

    public (decimal?, ReturnResult) ReturnLeasedHardware(DateTime today, int itemId, int userId)
    {
        var lease = _leaseRepository.GetLease(userId, itemId);
        if (lease == null)
        {
            return (null, ReturnResult.ItemNotLeased);
        }
        var fee = lease.CalculateReturnCost(today);
        var item = _hardwareService.GetHardware(lease.LeasedItemId); // if it's null other logic is invalid
        item.AvailabilityStatus = AvailabilityStatus.Available;
        _leaseRepository.RemoveLease(lease.LeasedItemId);
        return (fee, ReturnResult.ReturnSuccess);
    }


}