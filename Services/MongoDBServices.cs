#region Imports
using UX_UI_WEB_APP.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using UI_TAFE_A.Models;
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

        private readonly IMongoCollection<CartModel>
        _cart_collection;
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

            _cart_collection =
            database.GetCollection<CartModel>(
            mongodbSettings.Value.CartCollection);
        }
        #endregion

        #region Gets all items for cart
        public async Task<List<CartModel>> GetAllCartItemsAsync()
        {
            return await _cart_collection.Find(new BsonDocument()).
            ToListAsync();
        }
        #endregion

        #region Gets users items
        public async Task<CartModel> GetUserItems(string email)
        {
            var email_filter =
            Builders<CartModel>
            .Filter
            .Eq(u => u.UserEmail, email);

            return await _cart_collection.Find(email_filter).
            FirstAsync();
        }
        #endregion

        #region Add single cart item 
        public async Task<Object?> AddSingleCartItemAsync(CartModel cart_model)
        {
            await _cart_collection.InsertOneAsync(cart_model);

            return true;
        }
        #endregion

        #region Gets all items
        public async Task<List<ItemModel>> GetAllItemsAsync()
        {
            return await _item_collection.Find(new BsonDocument()).
            ToListAsync();
        }
        #endregion

        #region Gets items based on id
        public async Task<ItemModel> GetItemById(string id)
        {
            var id_filter =
            Builders<ItemModel>
            .Filter
            .Eq(u => u.Id, id);

            return await _item_collection.Find(id_filter).
            FirstAsync();
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

        #region Post item_id
        public async Task<Object?> PostItemAsync(UserModel user_model)
        {
            await _user_collection.InsertOneAsync(user_model);

            return true;
        }
        #endregion
    }
}
