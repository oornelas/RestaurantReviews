using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AwesomeFood.Contracts.Entities;
using AwesomeFood.Entities;

namespace AwesomeFood.WebAPI.Models
{
    public class Restaurant
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Phone]
        public string Phone { get; set; }
        [Url]
        public string Website { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public IEnumerable<Hours> Hours { get; set; }
        public string TimeZone { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public byte AwesomenessLevel { get; set; }
        [Required]
        public Guid CreatedByUserId { get; set; }
        [Required]
        public Guid ModifiedByUserId { get; set; }

        public static Restaurant MapFromEntity(IRestaurant restaurant)
        {
            if (restaurant == null) return null;

            return new Restaurant() {
                Id = restaurant.id,
                Name = restaurant.Name,
                Phone = restaurant.Phone,
                Website = restaurant.Website,
                Email = restaurant.Email,
                Hours = restaurant.Hours != null ? restaurant.Hours.Select(h => new Hours() { DayOfWeek = h.DayOfWeek, OpeningTime = h.OpeningTime, ClosingTime = h.ClosingTime }).ToList() : null,
                TimeZone = restaurant.TimeZone,
                AddressLine1 = restaurant.AddressLine1,
                AddressLine2 = restaurant.AddressLine2,
                City = restaurant.City,
                State = restaurant.State,
                ZipCode = restaurant.ZipCode,
                AwesomenessLevel = restaurant.AwesomenessLevel,
                CreatedByUserId = restaurant.CreatedByUserId,
                ModifiedByUserId = restaurant.ModifiedByUserId
            };
        }

        public static IRestaurant MapToEntity(Restaurant restaurant)
        {
            if (restaurant == null) return null;
            
            return new AwesomeFood.Entities.Restaurant() {
                id = restaurant.Id,
                Name = restaurant.Name,
                Phone = restaurant.Phone,
                Website = restaurant.Website,
                Email = restaurant.Email,
                Hours = restaurant.Hours != null ? restaurant.Hours.Select(h => new Common.Hours() { DayOfWeek = h.DayOfWeek, OpeningTime = h.OpeningTime, ClosingTime = h.ClosingTime }).ToList() : null,
                TimeZone = restaurant.TimeZone,
                AddressLine1 = restaurant.AddressLine1,
                AddressLine2 = restaurant.AddressLine2,
                City = restaurant.City,
                State = restaurant.State,
                ZipCode = restaurant.ZipCode,
                AwesomenessLevel = restaurant.AwesomenessLevel,
                CreatedByUserId = restaurant.CreatedByUserId,
                ModifiedByUserId = restaurant.ModifiedByUserId
            };
        }
    }
}