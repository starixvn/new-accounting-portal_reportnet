using System.Collections.Generic;
using MediatR;

namespace PunterAnalysis.Filter.UsernameSuggestion
{
    public class HttpParameterJson : IRequest<IEnumerable<string>>
    {
        public string Username { get; set; }

        public int RoleId { get; set; }
    }
}