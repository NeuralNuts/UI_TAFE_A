using Microsoft.AspNetCore.Mvc;
using UX_UI_WEB_APP.Services;
using UI_TAFE_A.Models;

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
        public async Task<List<ListModel>> GetUserLists()
        {
            return await _mongodb_services.GetAllUserLists();
        }
        #endregion
    }
}
