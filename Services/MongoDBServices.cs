#region Imports
using UX_UI_WEB_APP.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using UI_TAFE_A.Models;
using System.Linq;
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

        private readonly IMongoCollection<ListModel>
       _lists_collection;
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

            _lists_collection =
            database.GetCollection<ListModel>(
            mongodbSettings.Value.ListCollection);
        }
        #endregion

        #region Gets all items for cart
        public async Task<List<CartModel>> GetAllCartItemsAsync()
        {
            return await _cart_collection.Find(new BsonDocument()).
            ToListAsync();
        }
        #endregion

        #region Gets all lists for user
        public async Task<List<ListModel>> GetAllUserLists(string email)
        {
            var email_filter =
            Builders<ListModel>
            .Filter
            .Eq(u => u.UserEmail, email);

            return await _lists_collection.Find(email_filter).
            ToListAsync();
        }
        #endregion

        public async Task<Object?> AddLists(ListModel list_model)
        {
            await _lists_collection.InsertOneAsync(list_model);

            return true;
        }

        #region Gets users list items
        public async Task<List<CartModel>> GetUserItems(string email, string list_name)
        {
            var email_filter =
            Builders<CartModel>
            .Filter
            .Eq(u => u.UserEmail, email);

            var list_name_filter =
            Builders<CartModel>
            .Filter
            .Eq(u => u.ListName, list_name);

            var filters =
            Builders<CartModel>
            .Filter
            .And(list_name_filter, email_filter);

            return await _cart_collection.Find(filters).
            ToListAsync();
        }
        #endregion

        #region Add single cart item 
        public async Task<Object?> AddSingleCartItemAsync(CartModel cart_model)
        {
            await _cart_collection.InsertOneAsync(cart_model);

            return true;
        }
        #endregion

        #region Updates qty
        public async Task UpdateCartQtyAsync(string id, int qty)
        {
            FilterDefinition<CartModel> filter = Builders<CartModel>
            .Filter
            .Eq(u => u.Id, id);

            UpdateDefinition<CartModel> update = Builders<CartModel>
            .Update
            .Set(u => u.ItemQty, qty);

            await _cart_collection
            .UpdateOneAsync(filter,update);

            return;
        }
        #endregion

        #region Updates users theme
        public async Task UpadateUserTheme(string email, string theme)
        {
            FilterDefinition<UserModel> filter = Builders<UserModel>
            .Filter
            .Eq(u => u.UserEmail, email);

            UpdateDefinition<UserModel> update = Builders<UserModel>
            .Update
            .Set(u => u.UserTheme, theme);

             await _user_collection
            .UpdateOneAsync(filter, update);

            return;
        }
        #endregion

        #region Updates users theme
        public async Task UpadateUserImage(string email, string image)
        {
            FilterDefinition<UserModel> filter = Builders<UserModel>
            .Filter
            .Eq(u => u.UserEmail, email);

            UpdateDefinition<UserModel> update = Builders<UserModel>
            .Update
            .Set(u => u.UserImage, image);

            await _user_collection
           .UpdateOneAsync(filter, update);

            return;
        }
        #endregion

        public async Task DeleteCartItemAsync(string id)
        {
            FilterDefinition<CartModel> filter =
            Builders<CartModel>
            .Filter
            .Eq(u => u.Id, id);


            await _cart_collection
            .DeleteOneAsync(filter);

            return;
        }

        #region Gets all items
        public async Task<List<ItemModel>> GetAllItemsAsync()
        {
            return await _item_collection.Find(new BsonDocument()).
            ToListAsync();
        }
        #endregion

        #region Gets items based on id
        public async Task<List<ItemModel>> GetItemById(string id)
        {
            var id_filter =
            Builders<ItemModel>
            .Filter
            .Eq(u => u.Id, id);

            return await _item_collection.Find(id_filter).
            ToListAsync();
        }
        #endregion

        #region Gets user based on email
        public async Task<List<UserModel>> GetSingleUser(string email)
        {
            var id_filter =
            Builders<UserModel>
            .Filter
            .Eq(u => u.UserEmail, email);

            return await _user_collection.Find(id_filter).
            ToListAsync();
        }
        #endregion

        #region Gets user based on email
        public async Task<List<UserModel>> GetUserImage(string email)
        {
            var id_filter =
            Builders<UserModel>
            .Filter
            .Eq(u => u.UserEmail, email);

            return await _user_collection.Find(id_filter).
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

        #region Post item_id
        public async Task<Object?> PostItemAsync(UserModel user_model)
        {
            await _user_collection.InsertOneAsync(user_model);

            return true;
        }
        #endregion
    }
}
