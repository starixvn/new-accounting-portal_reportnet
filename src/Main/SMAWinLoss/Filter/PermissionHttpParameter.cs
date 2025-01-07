using MediatR;

namespace SMAWinLoss.Filter;

public class PermissionHttpParameter : IRequest<int>
{
    public string FunctionKey { get; set; }
}