using System;

namespace AwesomeFood.Common
{
    public struct Time
    {
        public Time(byte hours, byte minutes)
        {
            if (hours < 0 || hours > 24)
            {
                throw new ArgumentException("Hours can't be negative or greater than 24.", "hours");
            }

            if (minutes < 0 || minutes > 60)
            {
                throw new ArgumentException("Minutes can't be negative or greater than 60.", "minutes");
            }

            Hours = hours;
            Minutes = minutes;
        }

        public byte Hours { get; private set; }

        public byte Minutes { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}:{1:0#}", Hours, Minutes);
        }

    }
}