using System;

namespace AwesomeFood.Common.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(Guid userId) : base()
        {
            UserId = userId;
        }
        public UserNotFoundException(Guid userId, string message) : base(message)
        {
            UserId = userId;
        }
        public UserNotFoundException(Guid userId, string message, Exception innerException) : base(message,innerException)
        {
            UserId = userId;
        }
        
        public Guid UserId { get; private set; }
    }
}