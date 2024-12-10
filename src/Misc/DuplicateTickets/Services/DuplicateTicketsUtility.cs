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
            List<TicketModel> groups = new List<TicketModel>();
            DateTime prevBetTimeGroup = new DateTime(2014, 7, 11);
            DateTime prevBetTimeTicket = new DateTime(2014, 7, 11);
            int ticketIndex = 0;
            double timeDifferenceGroup = 0;
            double timeDifferenceTicket = 0;
            int groupNo = 0;

            foreach (var duplicateTicket in duplicateTicketByUserName)
            {
                var betTime = duplicateTicket.BetTime;
                TimeSpan timeSpanGroup = betTime.Subtract(prevBetTimeGroup);
                TimeSpan timeSpanTicket = betTime.Subtract(prevBetTimeTicket);
                timeDifferenceGroup = timeSpanGroup.TotalMilliseconds;
                timeDifferenceTicket = timeSpanTicket.TotalMilliseconds;

                if (ticketIndex == 0)
                {
                    timeDifferenceGroup = 0;
                    timeDifferenceTicket = 0;
                }

                if (timeDifferenceGroup < 0)
                {
                    continue;
                }

                if (timeDifferenceGroup >= TotalMilliseconds)
                {
                    groupNo++;
                    ticketIndex = 0;
                    timeDifferenceTicket = 0;
                }

                var ticketInfo = new TicketModel()
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
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new String builder to collect the bytes 
            // and create a string.
            StringBuilder builder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return builder.ToString();
        }
    }
}
