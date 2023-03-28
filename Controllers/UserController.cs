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
    }
}
