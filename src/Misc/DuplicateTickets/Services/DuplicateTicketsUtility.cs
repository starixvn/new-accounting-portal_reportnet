using DuplicateTickets.Entities;
using DuplicateTickets.Model;
using System.Security.Cryptography;
using System.Text;
using Utilities.DateTimes;

namespace DuplicateTickets.Services
{
    public interface DuplicateTicketsUtility
    {
        DuplicateTicketsByUserNameModel AssignMainTicketInfo(IGrouping<dynamic, DuplicateTicketEntity> duplicateTicketByUserName); 

        List<TicketModel> AssignSubTicketInfo(IGrouping<dynamic, DuplicateTicketEntity> duplicateTicketByUserName);

        string GetMD5Hash(string input);
    }

    public class DuplicateTicketsUtilityImp : DuplicateTicketsUtility
    {
        public const int TotalMilliseconds = 1000;

        public List<TicketModel> AssignSubTicketInfo(IGrouping<dynamic, DuplicateTicketEntity> duplicateTicketByUserName)
        {
            List<TicketModel> groups = [];
            var prevBetTimeGroup = new DateTime(2014, 7, 11);
            var prevBetTimeTicket = new DateTime(2014, 7, 11);
            var ticketIndex = 0;
            var groupNo = 0;

            foreach (var duplicateTicket in duplicateTicketByUserName)
            {
                var betTime = duplicateTicket.BetTime;
                var timeSpanGroup = betTime.Subtract(prevBetTimeGroup);
                var timeSpanTicket = betTime.Subtract(prevBetTimeTicket);
                var timeDifferenceGroup = timeSpanGroup.TotalMilliseconds;
                var timeDifferenceTicket = timeSpanTicket.TotalMilliseconds;

                if (ticketIndex == 0)
                {
                    timeDifferenceGroup = 0;
                    timeDifferenceTicket = 0;
                }

                switch (timeDifferenceGroup)
                {
                    case < 0:
                        continue;
                    case >= TotalMilliseconds:
                        groupNo++;
                        ticketIndex = 0;
                        timeDifferenceTicket = 0;
                        break;
                }

                var ticketInfo = new TicketModel
                {
                    Status = duplicateTicket.Status,
                    ReferenceNumber = duplicateTicket.RefNo,
                    BetTime = betTime.ToLongMMMddFormatedDate(),
                    TimeDifference = timeDifferenceTicket,
                    GroupNo = groupNo,
                    TransDesc = duplicateTicket.TransDesc,
                    Site = duplicateTicket.SiteName,
                    BetFrom = duplicateTicket.BetFrom
                };

                if (ticketIndex == 0)
                {
                    prevBetTimeGroup = betTime;
                }

                prevBetTimeTicket = betTime;

                ticketIndex++;
                groups.Add(ticketInfo);
            }

            return groups;
        }

        public DuplicateTicketsByUserNameModel AssignMainTicketInfo(IGrouping<dynamic, DuplicateTicketEntity> duplicateTicketByUserName)
        {
            DateTime winlostDate = duplicateTicketByUserName.Key.WinLostDate;
            var ticket = new DuplicateTicketsByUserNameModel
            {
                UserName = duplicateTicketByUserName.Key.UserName,
                CustId = duplicateTicketByUserName.Key.CustId,
                BetType = duplicateTicketByUserName.Key.TypeName,
                WinLostDate = winlostDate.ToShortFormatedDate(),
                BalanceBeforeBet = duplicateTicketByUserName.Key.Balance,
                Stake = duplicateTicketByUserName.Key.Stake,
                Odds = duplicateTicketByUserName.Key.Odds,
                BetTypeId = duplicateTicketByUserName.Key.Bettype
            };

            return ticket;
        }

        public string GetMD5Hash(string input)
        {
            var md5Hash = MD5.Create();

            // Convert the input string to a byte array and compute the hash. 
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new String builder to collect the bytes 
            // and create a string.
            var builder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            foreach (var t in data)
            {
                builder.Append(t.ToString("x2"));
            }

            // Return the hexadecimal string. 
            return builder.ToString();
        }
    }
}
