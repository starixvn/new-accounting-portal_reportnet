namespace AbnormalBet.HttpParameter;

using MediatR;
using Model;

public class BetTypeHttpParameter : IRequest<IEnumerable<FilterItemModel>>
{
    public string SportId { get; set; }

}