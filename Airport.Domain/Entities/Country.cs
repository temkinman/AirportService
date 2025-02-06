namespace Airport.Domain.Entities;

public class Country : BaseEntity
{
    public string Name { get; set; }    
    public string Iata { get; set; }
    
    public ICollection<City> Cities { get; set; } = new List<City>();
}