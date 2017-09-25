using System;
using AwesomeFood.Contracts.Entities;
using AwesomeFood.Entities;

namespace AwesomeFood.WebAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static User MapFromEntity(IUser user)
        {
            if (user == null) return null;

            return new User() {
                Id = user.id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public static IUser MapToEntity(User user)
        {
            if (user == null) return null;
            
            return new AwesomeFood.Entities.User() {
                id = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}