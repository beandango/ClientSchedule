namespace ClientSchedule.Models;

public sealed record CustomerItem(int CustomerId, string CustomerName)
{
    public override string ToString() => CustomerName;
}
