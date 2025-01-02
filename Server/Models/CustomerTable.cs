namespace Server.Models
{
    /// <summary>
    /// Customer table structure in database
    /// </summary>
    public class CustomerTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Quantity { get; set; }
    }
}
