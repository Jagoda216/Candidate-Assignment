using BCrypt.Net;

namespace Server.Models
{
    /// <summary>
    /// HashModel for Verifying hash passwords
    /// </summary>
    public class HashModel
    {
        /// <summary>
        /// Verify user's submited password with a hashed password stored in database
        /// </summary>
        /// <param name="password">User's password</param>
        /// <param name="storedPassword">User's hashed password</param>
        /// <returns></returns>
        public bool VerifyHash(string password, string storedPassword)
        {
            //Return true if password match otherwise false
            return BCrypt.Net.BCrypt.Verify(password, storedPassword); ;
        }
    }
}
