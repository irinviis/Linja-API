using Linja_API.Models;
using Linja_API.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Linja_API.Controllers
{
    public class BusController : BaseApiController
    {
        private readonly Database database;
        
        public BusController(Database dBase)
        {
            database = dBase;
        }


        [HttpGet("GetBusses")]
        public async Task<ActionResult> GetBusses()
        {
            var busses = await database.Bus.Where(
                b => b.Removed != true).OrderBy(b => b.Maker).ThenBy(
                b => b.RegNumber).ToListAsync();

            var maintenances = await database.Maintenance.ToListAsync();

            foreach (var bus in busses)
            {
                foreach (var maintType in Enum.GetValues(typeof(MaintenanceTypeEnum)).OfType<MaintenanceTypeEnum>())
                {
                    var maintenancesOfType = maintenances.Where(m => m.BusId == bus.Id && m.MaintenanceType == maintType).ToList();

                    var lastOneMaintenanceOfType = maintenancesOfType.LastOrDefault();
                    
                    if (lastOneMaintenanceOfType != null)
                    {
                        bus.Maintenances.Add(lastOneMaintenanceOfType);
                    }
                }
            }
            return Ok(busses);
        }


        [HttpPost("AddBus")]
        public async Task<ActionResult> AddBus(Bus bus)
        {
            database.Add(bus);
            await database.SaveChangesAsync();

            return Ok();
        }


        [HttpPost("UpdateBus")]
        public async Task UpdateBus(Bus updatedBus)
        {
            var busToUpdate = await database.Bus.Where(b => b.Id == updatedBus.Id).FirstOrDefaultAsync();

            if(busToUpdate != null)
            {
                busToUpdate.Maker = updatedBus.Maker;
                busToUpdate.RegNumber = updatedBus.RegNumber;

                database.SaveChanges();
            }
        }


        [HttpPost("RemoveBus")]
        public async Task<ActionResult> RemoveBus(Bus bus)
        {
            var busToRemove = await database.Bus.FirstOrDefaultAsync(b => b.Id == bus.Id);

            if(busToRemove != null)
            {
                busToRemove.Removed = true;
                database.SaveChanges();
            }
            return Ok();
        }
            
    }
}