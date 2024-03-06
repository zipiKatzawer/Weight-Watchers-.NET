using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeightWatchers.data.Entities;

namespace WeightWatchers.data
{
    public class WeightWatchersContext:DbContext
    {
        public DbSet<Subscriber> Subscriber { get; set; }
        public DbSet<Card> Card { get; set; }
        public WeightWatchersContext(DbContextOptions<WeightWatchersContext>options):base(options)
        {

        }
    }
}
