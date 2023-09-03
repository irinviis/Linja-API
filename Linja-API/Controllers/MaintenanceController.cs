using Linja_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Linja_API.Controllers
{
    public class MaintenanceController : BaseApiController
    {
        private readonly Database database;

        public MaintenanceController(Database dBase)
        {
            database = dBase;
        }


        [HttpGet("GetMaintenances")]
        public async Task<ActionResult> GetMaintenances(MaintenanceFilter filter)
        {
            var maintenances = new List<Maintenance>();

            if (!string.IsNullOrWhiteSpace(filter.RegNumber))
            {
                var bus = await database.Bus.FirstOrDefaultAsync(b => b.RegNumber == filter.RegNumber);
                if (bus != null)
                {
                    maintenances.AddRange(database.Maintenance.Where(m => m.BusId == bus.Id).ToList());
                }
            }
            return Ok(maintenances); 
        }


        [HttpPost("AddMaintenance")]
        public async Task<ActionResult> AddMaintenance(Maintenance maintenance)
        {
            database.Add(maintenance);
            await database.SaveChangesAsync();

            return Ok();
        }
    }
}