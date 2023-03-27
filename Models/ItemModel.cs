using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace UX_UI_WEB_APP.Models
{
    public class ItemModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonDefaultValue("")]
        public string? Id { get; set; }

        [BsonElement("item_name")]
        public string? ItemName { get; set; } = null!;

        [BsonElement("item_unit_size")]
        public string? ItemUnitSize { get; set;} = null!;

        [BsonElement("item_description")]
        public string? ItemDescription { get; set; } = null!;
    }
}
