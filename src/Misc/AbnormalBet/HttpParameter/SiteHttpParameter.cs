namespace AbnormalBet.HttpParameter;

using MediatR;
using Model;

public class SiteHttpParameter : IRequest<IEnumerable<SiteModel>>;