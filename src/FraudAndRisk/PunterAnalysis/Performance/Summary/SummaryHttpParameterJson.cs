namespace PunterAnalysis.Performance.Summary;

using MediatR;
using Model;

public class SummaryHttpParameterJson : HttpParameterJson, IRequest<ViewModelJson>;