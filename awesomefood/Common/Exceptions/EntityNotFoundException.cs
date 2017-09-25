using System;

namespace AwesomeFood.Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(Guid userId) : base()
        {
            UserId = userId;
        }
        public EntityNotFoundException(Guid userId, string message) : base(message)
        {
            UserId = userId;
        }
        public EntityNotFoundException(Guid userId, string message, Exception innerException) : base(message,innerException)
        {
            UserId = userId;
        }
        
        public Guid UserId { get; private set; }
    }
}