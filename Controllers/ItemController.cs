using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;
using UX_UI_WEB_APP.Models;
using UX_UI_WEB_APP.Services;

namespace UX_UI_WEB_APP.Controllers
{
    [Controller]
    [Route("~/api/[controller]")]
    public class ItemController : Controller
    {
        #region region MongoDBServices Variable
        private readonly MongoDBServices _mongodb_services;
        #endregion

        #region Setting _mongodb_services To mongodb_services
        public ItemController(
        MongoDBServices
        mongodbServices)
        {
            _mongodb_services = mongodbServices;
        }
        #endregion

        #region Http get all items
        /// <summary>
        /// Gets all sensor readings [Limited to 100 readings due to swagger not being able to load in more then 5000 records]
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllItems")]
        public async Task<List<ItemModel>> GetAllItems()
        {
            return await _mongodb_services.GetAllItemsAsync();
        }
        #endregion


        #region Post an item
        [HttpPost]
        [Route("PostSingleItem")]
        public async Task<IActionResult> PostSingleItem(ItemModel item_model)
        {
            await _mongodb_services.PostSingleItemAsync(item_model);

            return Ok("Item created");
        }
        #endregion
    }
}
