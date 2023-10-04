using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Validation.Models;

namespace Validation.Data
{
    public class VehiclesContext : DbContext
    {
        public VehiclesContext(DbContextOptions<VehiclesContext> options)
            : base(options)
        {
        }

        public DbSet<Validation.Models.Vehicle> Vehicle { get; set; } = default!;
    }
}
