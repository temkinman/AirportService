namespace Airport.Domain.Entities;

public class City : BaseEntity
{
    public string Name { get; set; }
    public string Iata { get; set; }
    
    public Guid CountryId { get; set; }
    public Country Country { get; set; }
    public ICollection<Airport> Airports { get; set; } = new List<Airport>();
}