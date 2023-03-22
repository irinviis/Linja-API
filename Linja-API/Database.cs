using Linja_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Linja_API
{
    public class Database : DbContext
    {

        public Database(DbContextOptions<Database> options) : base(options)
        {

        }

        public DbSet<Bus> Bus { get; set; }

    }
}