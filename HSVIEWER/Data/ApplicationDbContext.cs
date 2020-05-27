using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace HSVIEWER.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Stage> Stages { get; set; }

        public DbSet<Deal> Deals { get; set; }
        public DbSet<HsOwner> HsOwners { get; set; }
        public DbSet<Pipeline> Pipelines { get; set; }
        public DbSet<Entities.Models.WorkOrder> WorkOrder { get; set; }
    }
}
