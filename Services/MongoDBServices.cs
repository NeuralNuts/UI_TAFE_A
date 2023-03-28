#region Imports
using UX_UI_WEB_APP.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
#endregion

namespace UX_UI_WEB_APP.Services
{
    public class MongoDBServices
    {
        #region Setting The IMongoCollection To Weather
        private readonly IMongoCollection<ItemModel>
        _item_collection;

        private readonly IMongoCollection<UserModel>
        _user_collection;
        #endregion

        #region ConnectionURI, Database & Collection - Variables
        public MongoDBServices(
        IOptions<MongoDBSettings> mongodbSettings)
        {
            MongoClient client =
            new MongoClient(
            mongodbSettings.Value.ConnectionURI);

            IMongoDatabase database =
            client.GetDatabase(
            mongodbSettings.Value.Database);

            _item_collection =
            database.GetCollection<ItemModel>(
            mongodbSettings.Value.ItemsCollection);

            _user_collection =
            database.GetCollection<UserModel>(
            mongodbSettings.Value.UsersCollection);
        }
        #endregion

        #region Gets all items
        public async Task<List<ItemModel>> GetAllItemsAsync()
        {
            return await _item_collection.Find(new BsonDocument()).
            ToListAsync();
        }
        #endregion

        #region Create single item 
        public async Task<Object?> PostSingleItemAsync(ItemModel item_model)
        {
            await _item_collection.InsertOneAsync(item_model);

            return true;
        }
        #endregion

        #region Gets all users
        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            return await _user_collection.Find(new BsonDocument()).
            ToListAsync();
        }
        #endregion

        #region Authentiactes user login
        public async Task<UserModel?> AuthenticateUserLoginAsync(
        string email,
        string password)
        {
            var email_filter = Builders<UserModel>
            .Filter
            .Eq(e => e.UserEmail, email);

            var password_filter = Builders<UserModel>
            .Filter
            .Eq(e => e.UserPassword, password);

            var filters = Builders<UserModel>
            .Filter
            .And(email_filter, password_filter);

            var user_login =
            await _user_collection
            .Find(filters)
            .FirstOrDefaultAsync();

            if (user_login == null)
            {
                return null;
            }
            else
            {
                return user_login;
            }
        }
        #endregion
    }
}
