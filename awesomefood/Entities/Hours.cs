using System;
using AwesomeFood.Common;
using AwesomeFood.Contracts.Entities;

namespace AwesomeFood.Entities
{
    public class Hours : IHours
    {
        public DayOfWeek DayOfWeek { get; set; }

        public Time OpeningTime { get; set; }

        public Time ClosingTime { get; set; }
    }
}