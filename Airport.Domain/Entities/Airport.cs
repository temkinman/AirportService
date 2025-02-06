using Airport.Domain.Enums;

namespace Airport.Domain.Entities;

public class Airport : BaseEntity
{
    public string Iata { get; set; }
    public string Icao { get; set; }
    public string Name { get; set; }
    public int Rating { get; set; }
    public int Hubs { get; set; }
    public string TimezoneRegionName { get; set; }
    public BuildingType Type { get; set; }

    public Guid CityId { get; set; }
    public City City { get; set; }
    public Country Country { get; set; }
    public Location Location { get; set; }
}