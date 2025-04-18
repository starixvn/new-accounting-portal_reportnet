namespace PunterAnalysis.Filter.UsernameSuggestion;

using MediatR;

public class HttpParameterJson : IRequest<IEnumerable<string>>
{
    public string Username { get; set; }

    public int RoleId { get; set; }
}