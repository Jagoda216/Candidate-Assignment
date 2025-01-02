using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Hubs;
using Server.Models;

namespace Server.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateDataBaseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<QuantityUpdateHub> _quantityUpdateHub;

        public UpdateDataBaseController(ApplicationDbContext context, IHubContext<QuantityUpdateHub> quantityUpdateHub)
        {
            _context = context;
            _quantityUpdateHub = quantityUpdateHub;
        }

        [HttpPost("updateQuantity")]
        [Authorize]
        public async Task<IActionResult> UpdatedQuantity([FromBody] UserQuantityModel model)
        {
            if (string.IsNullOrEmpty(model.Name) || model.Quantity == 0)
            {
                return BadRequest("Name and Quantity are required");
            }

            var user = await _context.Customer.SingleOrDefaultAsync(c => c.Name == model.Name);
            if (user == null)
            {
                return NotFound("User was not found");
            }

            if(model.Quantity < 1)
            {
                return BadRequest("Quantity is not a positive number");
            }

            user.Quantity += model.Quantity;

            await _context.SaveChangesAsync();
            await _quantityUpdateHub.Clients.All.SendAsync("RecivedUpdatedQuantity", user.Name, user.Quantity);
            

            return Ok(user);
        }
        
    }
}
