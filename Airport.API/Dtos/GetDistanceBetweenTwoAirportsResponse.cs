namespace Airport.API.Dtos;

public record GetDistanceBetweenTwoAirportsResponse(
    double distance,
    string distanceType = "km");