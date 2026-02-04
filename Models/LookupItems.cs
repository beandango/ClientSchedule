namespace ClientSchedule.Models;

public sealed record CountryItem(int CountryId, string Country)
{
    public override string ToString() => Country;
}

public sealed record CityItem(int CityId, string City)
{
    public override string ToString() => City;
}
