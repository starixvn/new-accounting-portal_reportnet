using MediatR;
using OfficeOpenXml;
using UpdatedUsernameCredit.Common;

namespace UpdatedUsernameCredit.UpdatedUsername
{
    public class ExcelMessage : BaseMessage, IRequest<ExcelPackage>
    {
    }
}