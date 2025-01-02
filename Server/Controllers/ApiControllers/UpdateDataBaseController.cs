using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Hubs;
using Server.Models;

namespace Server.Controllers.ApiControllers
{
    /// <summary>
    /// API controller responsible for updating
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateDataBaseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<QuantityUpdateHub> _quantityUpdateHub;

        /// <summary>
        /// Initializes a new instance UpdateDataBaseController
        /// </summary>
        /// <param name="context">Database context for accessing and modifying data</param>
        /// <param name="quantityUpdateHub">SignalR hub for notifying clients of quantity updates</param>
        public UpdateDataBaseController(ApplicationDbContext context, IHubContext<QuantityUpdateHub> quantityUpdateHub)
        {
            _context = context;
            _quantityUpdateHub = quantityUpdateHub;
        }
        /// <summary>
        /// Updates quantity for a logged in user
        /// </summary>
        /// <param name="model">Model contains user's name and quantity update from Client</param>
        /// <returns>Returns result of operation</returns>
        [HttpPost("updateQuantity")]
        [Authorize]
        public async Task<IActionResult> UpdatedQuantity([FromBody] UserQuantityModel model)
        {
            if (string.IsNullOrEmpty(model.Name) || model.Quantity == 0)
            {
                return BadRequest("Name and Quantity are required");
            }
            
            //Retrive user from database
            var user = await _context.Customer.SingleOrDefaultAsync(c => c.Name == model.Name);
            if (user == null)
            {
                return NotFound("User was not found");
            }

            if(model.Quantity < 1)
            {
                return BadRequest("Quantity is not a positive number");
            }

            //Increase user's quantity
            user.Quantity += model.Quantity;

            //Save changes to database
            await _context.SaveChangesAsync();

            //Notify web client about updated quantity using SignalR
            await _quantityUpdateHub.Clients.All.SendAsync("RecivedUpdatedQuantity", user.Name, user.Quantity);
            

            return Ok(user);
        }
        
    }
}
