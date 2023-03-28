using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace UI_TAFE_A.Models
{
    public class CartModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonDefaultValue("")]
        public string? Id { get; set; }

        [BsonElement("user_email")]
        public string? UserEmail { get; set; } = null;

        [BsonElement("item_id")]
        public string? ItemId { get; set; } = null;
    }
}
