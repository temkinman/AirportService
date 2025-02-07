namespace Airport.Domain.Entities;

public class Location : BaseEntity
{
    public double Lon { get; set; }
    public double Lat { get; set; }
    public Airport Airport { get; set; }
}