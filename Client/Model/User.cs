using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{   
    /// <summary>
    /// Represents a user's structure in application
    /// </summary>
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Quantity { get; set; }
    }
}
