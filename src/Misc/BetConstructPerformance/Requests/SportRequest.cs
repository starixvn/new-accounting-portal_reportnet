using MediatR;
using System;
using BetConstructPerformance.Models;

namespace BetConstructPerformance.Requests
{
    public class SportRequest : IRequest<SportModel>
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool NoDraw { get; set; }

        public int? SportId { get; set; }
    }
}
