using System;

namespace AwesomeFood.Common
{
    public struct Hours
    {
        public DayOfWeek DayOfWeek { get; set; }

        public Time OpeningTime { get; set; }

        public Time ClosingTime { get; set; }
    }
}