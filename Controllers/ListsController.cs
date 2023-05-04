using Microsoft.AspNetCore.Mvc;
using UX_UI_WEB_APP.Services;
using UI_TAFE_A.Models;
using UX_UI_WEB_APP.Models;

namespace UX_UI_WEB_APP.Controllers
{
    [Controller]
    [Route("~/api/[controller]")]
    public class ListsController : Controller
    {
        #region region MongoDBServices Variable
        private readonly MongoDBServices _mongodb_services;
        #endregion

        #region Setting _mongodb_services To mongodb_services
        public ListsController(
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
        [Route("GetUserLists")]
        public async Task<List<ListModel>> GetUserLists([FromQuery]string email)
        {
            return await _mongodb_services.GetAllUserLists(email);
        }
        #endregion

        #region Delete cart item 
        /// <summary>
        /// Gets all sensor readings [Limited to 100 readings due to swagger not being able to load in more then 5000 records]
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteListReal")]
        public async Task<IActionResult> DeleteItem(string list_name)
        {
            await _mongodb_services.DeleteListRealAsync(list_name);

            return Ok("List gonzo");
        }
        #endregion

        #region Post an item
        [HttpPost]
        [Route("PostList")]
        public async Task<IActionResult> PostList(ListModel list_model)
        {
            await _mongodb_services.AddLists(list_model);

            return Ok("Item created");
        }
        #endregion
    }
}
