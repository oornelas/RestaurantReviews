using System;
using AwesomeFood.Contracts.Entities;
using AwesomeFood.Entities;
using AwesomeFood.Common;

namespace AwesomeFood.WebAPI.Models
{
    public class Hours
    {
        public DayOfWeek DayOfWeek { get; set; }

        public Time OpeningTime { get; set; }

        public Time ClosingTime { get; set; }
    }
}