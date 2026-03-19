namespace APBD_Cw1_s31191;

public class Employee(string firstName, string lastName) : User(firstName, lastName)
{
    public override UserType UserType => UserType.Employee;
    
    public override int MaxLeaseCount => 5;
}