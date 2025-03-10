namespace UpdatedUsernameCredit.UpdatedCredit;

using Common;
using MediatR;
using OfficeOpenXml;

public class ExcelMessage : BaseMessage, IRequest<ExcelPackage>;