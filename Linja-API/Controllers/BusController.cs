using Linja_API.Models;
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
                b => b.Removed != true).ToListAsync();
            return Ok(busses);
        }


        [HttpGet("GetBus")]
        public async Task<ActionResult> GetBus(int busId)
        {
            var bus = await database.Bus.Where(
                b => b.Id == busId && b.Removed != true).FirstOrDefaultAsync();
            return Ok(bus);
        }


        [HttpPost("AddBus")]
        public async Task<ActionResult> AddBus(Bus bus)
        {
            database.Add(bus);
            await database.SaveChangesAsync();

            return Ok();
        }
    };
}