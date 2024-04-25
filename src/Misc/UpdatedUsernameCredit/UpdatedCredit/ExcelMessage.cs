using MediatR;
using OfficeOpenXml;
using UpdatedUsernameCredit.Common;

namespace UpdatedUsernameCredit.UpdatedCredit
{
    public class ExcelMessage : BaseMessage, IRequest<ExcelPackage>
    {
    }
}