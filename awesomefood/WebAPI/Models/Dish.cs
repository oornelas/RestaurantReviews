using System;
using System.Collections.Generic;
using System.Linq;
using AwesomeFood.Contracts.Entities;
using AwesomeFood.Entities;

namespace AwesomeFood.WebAPI.Models
{
    public class Dish
    {
        public Guid Id { get; set; }
        public Guid RestaurantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte AwesomenessLevel { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid ModifiedByUserId { get; set; }

        public static Dish MapFromEntity(IDish dish)
        {
            if (dish == null) return null;

            return new Dish() {
                Id = dish.id,
                RestaurantId = dish.RestaurantId,
                Name = dish.Name,
                Description = dish.Description,
                AwesomenessLevel = dish.AwesomenessLevel,
                CreatedByUserId = dish.CreatedByUserId,
                ModifiedByUserId = dish.ModifiedByUserId
            };
        }

        public static IDish MapToEntity(Dish dish)
        {
            if (dish == null) return null;
            
            return new AwesomeFood.Entities.Dish() {
                id = dish.Id,
                RestaurantId = dish.RestaurantId,
                Name = dish.Name,
                Description = dish.Description,
                AwesomenessLevel = dish.AwesomenessLevel,
                CreatedByUserId = dish.CreatedByUserId,
                ModifiedByUserId = dish.ModifiedByUserId
            };
        }
    }
}