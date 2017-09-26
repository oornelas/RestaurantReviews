using System;
using System.Collections.Generic;
using AwesomeFood.Common;

namespace AwesomeFood.Contracts.Entities
{
    public interface IRestaurant : IUserCreatedEntity
    {
        string Name { get; set; }
        string Phone { get; set; }
        string Website { get; set; }
        string Email { get; set; }
        IEnumerable<Hours> Hours { get; set; }
        string TimeZone { get; set; }
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string City { get; set; }
        string State { get; set; }
        string ZipCode { get; set; }
        byte AwesomenessLevel { get; set; }
    }
}