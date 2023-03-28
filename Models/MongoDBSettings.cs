namespace UX_UI_WEB_APP.Models
{
    public class MongoDBSettings
    {
        public string ConnectionURI
        { get; set; } = null!;

        public string Database
        { get; set; } = null!;

        public string UsersCollection
        { get; set; } = null!;

        public string ItemsCollection
        { get; set; } = null!;

        public string CartCollection
        { get; set; } = null!;
    }
}
