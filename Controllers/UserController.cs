using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using UX_UI_WEB_APP.Models;
using UX_UI_WEB_APP.Services;

namespace UX_UI_WEB_APP.Controllers
{
    [Controller]
    [Route("~/api/[controller]")]

    public class UserController : Controller
    {
        #region region MongoDBServices Variable
        private readonly MongoDBServices _mongodb_services;
        #endregion

        #region Setting _mongodb_services To mongodb_services
        public UserController(
        MongoDBServices
        mongodbServices)
        {
            _mongodb_services = mongodbServices;
        }
        #endregion

        #region Http get all users
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _mongodb_services.GetAllUsersAsync();
        }
        #endregion

        #region Http get all users
        [HttpGet]
        [Route("GetSingleUser")]
        public async Task<List<UserModel>> GetSingleUsers(string email)
        {
            return await _mongodb_services.GetSingleUser(email);
        }
        #endregion

        #region Authenticate user login
        [HttpGet]
        [Route("AuthenticateUserLogin")]
        public async Task<IActionResult> GetAuthenticateUserLogin(
        [FromQuery] string email,
        [FromQuery] string password)
        {
            try
            {
                var result = await _mongodb_services
                .AuthenticateUserLoginAsync(email,password);

                if (result == null ||
                    email == null ||
                    password == null)
                {
                    return Unauthorized("Unauthorized".ToJson());
                }
                else
                {

                    return Ok("Authorized".ToJson());
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion

        #region Post an item id
        [HttpPost]
        [Route("PostItemId")]
        public async Task<IActionResult> PostItemId(UserModel user_model)
        {
            await _mongodb_services.PostItemAsync(user_model);

            return Ok("Item id added to cart");
        }
        #endregion

        #region Update user theme
        /// <summary>
        /// Gets all sensor readings [Limited to 100 readings due to swagger not being able to load in more then 5000 records]
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("PutUserTheme")]
        public async Task<IActionResult> PutUserTheme([FromQuery] string email, string theme)
        {
            await _mongodb_services.UpadateUserTheme(email, theme);

            return Ok("Theme updated");
        }
        #endregion
    }
}
