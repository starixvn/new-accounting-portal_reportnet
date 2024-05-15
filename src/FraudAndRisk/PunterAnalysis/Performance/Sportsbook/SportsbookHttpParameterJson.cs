using MediatR;
using PunterAnalysis.Performance.Sportsbook.Model;

namespace PunterAnalysis.Performance.Sportsbook
{
    public class SportsbookHttpParameterJson : HttpParameterJson, IRequest<ViewModelJson>
    {
    }
}