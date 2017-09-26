using System;
using AwesomeFood.Contracts.Entities;

namespace AwesomeFood.Entities
{
    public class DishReview : IDishReview
    {
        public Guid id { get; set; }
        public Guid DishId { get; set; }
        public string Review { get; set; }
        public Guid UserId { get; set; }
        public byte AwesomenessLevel { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid ModifiedByUserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }    
}