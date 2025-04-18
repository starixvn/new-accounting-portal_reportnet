namespace SMAWinLoss.Filter;

using MediatR;

public class PermissionHttpParameter : IRequest<int>
{
    public string FunctionKey { get; set; }
}