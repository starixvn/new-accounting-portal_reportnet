namespace PunterAnalysis.Performance.Sportsbook;

using MediatR;
using Model;

public class SportsbookHttpParameterJson : HttpParameterJson, IRequest<ViewModelJson>;