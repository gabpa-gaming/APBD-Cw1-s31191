namespace APBD_Cw1_s31191;

public class LeaseRepository : Repository<Lease>
{
    public void AddLease(Lease lease)
    {
        AddEntry(lease, lease.JoinedLeaseId);
    }

    public void RemoveLease(int id)
    {
        RemoveEntry(id);
    }

    public List<Lease> GetAllLeases()
    {
        return GetEntries();
    }

    public Lease? GetLease(int userId, int leasedItemId)
    {
        return GetEntry(Lease.ConvertIdsToJoinedLeaseId(userId, leasedItemId));
    }
    
    public List<Lease> GetLeasesByUserId(int userId)
    {
        return GetEntries().FindAll((lease)  => lease.LesseeId == userId);
    }

    public int GetUserLeaseCount(int userId)
    {
        return GetLeasesByUserId(userId).Count;
    }
}