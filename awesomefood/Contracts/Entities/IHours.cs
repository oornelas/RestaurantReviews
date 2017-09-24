using System;
using AwesomeFood.Common;

namespace AwesomeFood.Contracts.Entities
{
    public interface IHours
    {
        DayOfWeek DayOfWeek { get; set; }

        Time OpeningTime { get; set; }

        Time ClosingTime { get; set; }
    }
}