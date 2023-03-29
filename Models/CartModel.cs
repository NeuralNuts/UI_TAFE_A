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

        [BsonElement("item_name")]
        public string? ItemName { get; set; } = null!;

        [BsonElement("item_unit_size")]
        public string? ItemUnitSize { get; set; } = null!;

        [BsonElement("item_price")]
        public double? ItemPrice { get; set; } = null!;

        [BsonElement("item_qty")]
        public int? ItemQty { get; set; } = null!;
    }
}
