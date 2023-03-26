using Linja_API.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Linja_API.Models
{
    public class Maintenance
    {
        public int Id { get; set; }

        public string? Info { get; set; }

        public DateTime Timestamp { get; set; }

        public DateTime NextMaintenanceTimestamp { get; set; }

        public bool Removed { get; set; }

        public int BusId { get; set; }

        public MaintenanceTypeEnum MaintenanceType { get; set; }
      
        [NotMapped]
        public bool NextServiceIsSoon => DateTime.Now.AddDays(5) >= NextMaintenanceTimestamp;
    }
}
