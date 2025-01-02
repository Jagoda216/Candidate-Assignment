using BCrypt.Net;

namespace Server.Models
{
    public class HashModel
    {
        public bool VerifyHash(string password, string storedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedPassword); ;
        }
    }
}
