using System;
using System.Collections.Generic;
using System.Linq;
using AwesomeFood.Contracts.Entities;
using AwesomeFood.Entities;

namespace AwesomeFood.WebAPI.Models
{
    public class DishReview
    {
        public Guid Id { get; set; }
        public Guid DishId { get; set; }
        public string Review { get; set; }
        public byte AwesomenessLevel { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid ModifiedByUserId { get; set; }
        public Dish Dish { get; set; }
        public Restaurant Restaurant { get; set; }

        public static DishReview MapFromEntity(IDishReview dishReview)
        {
            if (dishReview == null) return null;

            return new DishReview() {
                Id = dishReview.id,
                DishId = dishReview.DishId,
                Review = dishReview.Review,
                AwesomenessLevel = dishReview.AwesomenessLevel,
                CreatedByUserId = dishReview.CreatedByUserId,
                ModifiedByUserId = dishReview.ModifiedByUserId
            };
        }

        public static IDishReview MapToEntity(DishReview dishReview)
        {
            if (dishReview == null) return null;
            
            return new AwesomeFood.Entities.DishReview() {
                id = dishReview.Id,
                DishId = dishReview.DishId,
                Review = dishReview.Review,
                AwesomenessLevel = dishReview.AwesomenessLevel,
                CreatedByUserId = dishReview.CreatedByUserId,
                ModifiedByUserId = dishReview.ModifiedByUserId
            };
        }
    }
}