using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BedManager.Domain.Entity
{
    public class BedManagerContext : DbContext
    {
        public BedManagerContext(DbContextOptions<BedManagerContext> options) : base(options)
        {

        }

        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Bed> Beds { get; set; }
        public virtual DbSet<Hospital> Hospitals { get; set; }
        public virtual DbSet<BedStatus> BedStatuses { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }

    }
}
