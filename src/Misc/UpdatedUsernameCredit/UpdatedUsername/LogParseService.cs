using System.Text.RegularExpressions;

namespace UpdatedUsernameCredit.UpdatedUsername
{
    public interface LogParseService
    {
        UsernameLogModel GetUsernameLog(string tracking);
    }

    public class LogParseServiceImp : LogParseService
    {
        public UsernameLogModel GetUsernameLog(string tracking)
        {
            var fromNameRegex = new Regex("""(?<=\bFromName=")[^"]*""");
            var fromNameMatch = fromNameRegex.Match(tracking);

            var toNameRegex = new Regex("""(?<=\bToName=")[^"]*""");
            var toNameMatch = toNameRegex.Match(tracking);

            return new UsernameLogModel
            {
                FromName = fromNameMatch.Value,
                ToName = toNameMatch.Value
            };
        }
    }
}