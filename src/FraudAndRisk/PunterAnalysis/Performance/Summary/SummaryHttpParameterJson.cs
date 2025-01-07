using MediatR;
using PunterAnalysis.Performance.Summary.Model;

namespace PunterAnalysis.Performance.Summary;

public class SummaryHttpParameterJson : HttpParameterJson, IRequest<ViewModelJson>;