using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// EntityBase.
    /// </summary>
    public class EntityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityBase"/> class.
        /// </summary>
        public EntityBase()
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
