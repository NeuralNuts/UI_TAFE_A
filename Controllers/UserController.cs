using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Security.Claims;
using UX_UI_WEB_APP.Models;
using UX_UI_WEB_APP.Services;
using System.Security.Cryptography;
using System.Text;

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

        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        string HashPasword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }

        bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
        }

        #region Authenticate user login
        [HttpGet]
        [Route("AuthenticateUserLogin")]
        public async Task<IActionResult> GetAuthenticateUserLogin(
        [FromQuery] string email,
        [FromQuery] string password)
        {
            try
            {
                var hash = HashPasword(password, out var salt);
                var result = await _mongodb_services
                .AuthenticateUserLoginAsync(email, password);

                if (VerifyPassword(password, hash, salt) == false)
                {
                    return Unauthorized("Unauthorized".ToJson());
                }

                if (result == null ||
                    email == null ||
                    password == null)
                {
                    return Unauthorized("Unauthorized".ToJson());
                }
                else
                {
                    //var claims = new List<Claim>
                    //{
                    //  new Claim(ClaimTypes.Name, Guid.NewGuid().ToString())
                    //};

                    //var claimsIdentity = new ClaimsIdentity(
                    //  claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //var authProperties = new AuthenticationProperties();

                    //await HttpContext.SignInAsync(
                    //  CookieAuthenticationDefaults.AuthenticationScheme,
                    //  new ClaimsPrincipal(claimsIdentity),
                    //  authProperties);
                    string id = HttpContext.Session.Id;
                    HttpContext.Session.GetString(id);
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
