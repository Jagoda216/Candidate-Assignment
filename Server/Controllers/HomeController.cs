using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Hubs;
using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;

namespace Server.Controllers
{
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext _context;
        private readonly IHubContext<QuantityUpdateHub> _quantityUpdateHub;

        public HomeController(ApplicationDbContext context, IHubContext<QuantityUpdateHub> quantityUpdateHub)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
            _quantityUpdateHub = quantityUpdateHub ?? throw new ArgumentNullException(nameof(quantityUpdateHub));
        }
        public IActionResult Index()
        {
            var customers = _context.Customer.ToList();
            return View(customers);
        }

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
