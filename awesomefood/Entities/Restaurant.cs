using System;
using System.Collections.Generic;
using AwesomeFood.Contracts.Entities;
using AwesomeFood.Common;

namespace AwesomeFood.Entities
{
    public class Restaurant : IRestaurant
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public IEnumerable<Common.Hours> Hours { get; set; }
        public string TimeZone { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public byte AwesomenessLevel { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid ModifiedByUserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}