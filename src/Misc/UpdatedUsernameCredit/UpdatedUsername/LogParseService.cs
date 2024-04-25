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
            Regex fromNameRegex = new Regex(@"(?<=\bFromName="")[^""]*");
            Match fromNameMatch = fromNameRegex.Match(tracking);

            Regex toNameRegex = new Regex(@"(?<=\bToName="")[^""]*");
            Match toNameMatch = toNameRegex.Match(tracking);

            return new UsernameLogModel
            {
                FromName = fromNameMatch.Value,
                ToName = toNameMatch.Value
            };
        }
    }
}