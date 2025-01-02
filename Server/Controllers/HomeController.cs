using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Hubs;
using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;

namespace Server.Controllers
{
	/// <summary>
	/// HomeController handles requests to the main page of application
	/// </summary>
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext _context;
        private readonly IHubContext<QuantityUpdateHub> _quantityUpdateHub;
        /// <summary>
        /// Initializes a new instance HomeController
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="quantityUpdateHub">SignalR hub for broadcasting updates</param>
        /// <exception cref="ArgumentNullException"></exception>
        public HomeController(ApplicationDbContext context, IHubContext<QuantityUpdateHub> quantityUpdateHub)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
            _quantityUpdateHub = quantityUpdateHub ?? throw new ArgumentNullException(nameof(quantityUpdateHub));
        }

        /// <summary>
        /// Displays a list of all customers in database
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //Retrive all customers from database
            var customers = _context.Customer.ToList();

            //Return to View(Index.cshtml) all the customer 
            return View(customers);
        }

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
