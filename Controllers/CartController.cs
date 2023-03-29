using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;
using UX_UI_WEB_APP.Models;
using UX_UI_WEB_APP.Services;
using UI_TAFE_A.Models;
using System.ComponentModel.DataAnnotations;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization.IdGenerators;

namespace UX_UI_WEB_APP.Controllers
{
    [Controller]
    [Route("~/api/[controller]")]
    public class CartController : Controller
    {
        #region region MongoDBServices Variable
        private readonly MongoDBServices _mongodb_services;
        #endregion

        #region Setting _mongodb_services To mongodb_services
        public CartController(
        MongoDBServices
        mongodbServices)
        {
            _mongodb_services = mongodbServices;
        }
        #endregion

        #region Http get all cart items
        /// <summary>
        /// Gets all sensor readings [Limited to 100 readings due to swagger not being able to load in more then 5000 records]
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllCartItems")]
        public async Task<List<CartModel>> GetAllCartItems()
        {
            return await _mongodb_services.GetAllCartItemsAsync();
        }
        #endregion

        #region Sum item price
        /// <summary>
        /// Gets all sensor readings [Limited to 100 readings due to swagger not being able to load in more then 5000 records]
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("SumCartPrice")]
        public async Task<IActionResult> SumCartPrice(double[] price)
        {
            var sum = price.Sum();

            return Ok(sum);
        }
        #endregion

        #region Http get user cart items
        /// <summary>
        /// Gets all sensor readings [Limited to 100 readings due to swagger not being able to load in more then 5000 records]
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserCartItems")]
        public async Task<IActionResult> GetUserCartItems(string email)
        {
            var result = await _mongodb_services.GetUserItems(email);

            return Ok(result);
        }
        #endregion

        #region Post single cart item
        /// <summary>
        /// Gets all sensor readings [Limited to 100 readings due to swagger not being able to load in more then 5000 records]
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("PostSingleCartItem")]
        public async Task<IActionResult> PostCartItem(CartModel cart_model)
        {
            await _mongodb_services.AddSingleCartItemAsync(cart_model);

            return Ok("Item added to cart");
        }
        #endregion

        #region Update cart item qty
        /// <summary>
        /// Gets all sensor readings [Limited to 100 readings due to swagger not being able to load in more then 5000 records]
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("PutItemQty")]
        public async Task<IActionResult> PutItemQty(string id, int qty)
        {
            await _mongodb_services.UpdateCartQtyAsync(id, qty);

            return Ok("Cart qty updated");
        }
        #endregion
    }
}
