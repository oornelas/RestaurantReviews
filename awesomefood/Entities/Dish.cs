using System;
using AwesomeFood.Contracts.Entities;

namespace AwesomeFood.Entities
{
    public class Dish : IDish
    {
        public Guid id { get; set; }
        public Guid RestaurantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte AwesomenessLevel { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid ModifiedByUserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}