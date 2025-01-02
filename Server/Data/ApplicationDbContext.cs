using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
    /// <summary>
    /// Represents application's database context for interacting with the database
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance ApplicationDbContext with specified options
        /// </summary>
        /// <param name="options">For configuring database context</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets Customer table in database
        /// </summary>
        public DbSet<CustomerTable> Customer { get; set; }
    }
}
