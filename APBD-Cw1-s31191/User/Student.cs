namespace APBD_Cw1_s31191;

public class Student(string firstName, string lastName) : User(firstName, lastName)
{
    public override UserType UserType => UserType.Student;
    public override int MaxLeaseCount => 2;
}
