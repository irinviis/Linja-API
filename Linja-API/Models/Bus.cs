using System.ComponentModel.DataAnnotations.Schema;

namespace Linja_API.Models
{
    public class Bus
    {
        public int Id { get; set; }
        public string? Maker { get; set; }
        public string? RegNumber { get; set; }
        public bool Removed { get; set; }

        [NotMapped]
        public ICollection<Maintenance> Maintenances { get; set; } = new List<Maintenance>();
    }
}
