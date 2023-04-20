using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace UX_UI_WEB_APP.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonDefaultValue("")]
        public string? Id { get; set; }

        [BsonElement("user_email")]
        public string? UserEmail { get; set; } = null;

        [BsonElement("user_password")]
        public string? UserPassword { get; set; } = null;

        [BsonElement("user_role")]
        public string? UserRole { get; set; } = null;

        [BsonElement("user_theme")]
        public string? UserTheme { get; set; } = null;

        [BsonElement("user_image")]
        public string? UserImage { get; set; } = null;
    }
}
