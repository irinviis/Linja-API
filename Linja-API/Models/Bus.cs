namespace Linja_API.Models
{
    public class Bus
    {
        public int Id { get; set; }

        public string? Maker { get; set; }

        public string? RegNumber { get; set; }

        public DateTime LastServiceDate { get; set; }

        public DateTime UpdatedServiceDate { get; set; }

        public bool Removed { get; set; }
    }
}
