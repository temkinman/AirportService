using Airport.Application.Helpers;
using FluentValidation;
using MediatR;

namespace Airport.Application.Airports.Queries.GetDistanceBetweenTwoAirports;

public record GetDistanceBetweenTwoAirportsQuery(
    string firstAirportCode,
    string secondAirportCode) : IRequest<GetDistanceBetweenTwoAirportsQueryResult>;
    
public record GetDistanceBetweenTwoAirportsQueryResult(double distance);

public class GetDistanceBetweenTwoAirportsQueryValidator : AbstractValidator<GetDistanceBetweenTwoAirportsQuery>
{
    public GetDistanceBetweenTwoAirportsQueryValidator()
    {
        RuleFor(x => x.firstAirportCode).NotNull().NotEmpty().WithMessage("First airportCode is required");
        RuleFor(x => x.firstAirportCode).MaximumLength(3).WithMessage("First airportCode must be maximum 3 symbols");
        RuleFor(x => x.secondAirportCode).NotNull().NotEmpty().WithMessage("Second airportCode is required");
        RuleFor(x => x.secondAirportCode).MaximumLength(3).WithMessage("Second airportCode must be maximum 3 symbols");
        RuleFor(x => x.firstAirportCode).Must(AirportHelper.BeUpperCase).WithMessage("The string must be in uppercase.");
        RuleFor(x => x.secondAirportCode).Must(AirportHelper.BeUpperCase).WithMessage("The string must be in uppercase.");
    }
}