using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GtMotive.Estimate.Microservice.Domain.Base
{
    /// <summary>
    /// EntityBase.
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityBase"/> class.
        /// </summary>
        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets Id.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; private set; }
    }
}
