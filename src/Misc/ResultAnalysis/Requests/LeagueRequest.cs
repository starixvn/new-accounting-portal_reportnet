using MediatR;
using ResultAnalysis.Models;

namespace ResultAnalysis.Requests
{
    public class LeagueRequest: IRequest<IEnumerable<LeagueModel>>
	{
		public int SportId { get; set; }

		public string SearchText { get; set; }
	}
}
