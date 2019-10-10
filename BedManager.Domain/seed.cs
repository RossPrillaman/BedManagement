using Microsoft.EntityFrameworkCore;
using BedManager.Domain.Entity;
using System.Linq;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace BedManager.Domain
{
    public class Seed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BedManagerContext(
            serviceProvider.GetRequiredService<DbContextOptions<BedManagerContext>>()))
            {
                var Hosp = new Hospital("New Hosp 1");
                context.Hospitals.Add(Hosp);
                context.SaveChanges();

                var Room = Hosp.AddRoom(2);
                context.SaveChanges();
                Room.AddBed("Descrip_1", true);
                Room.AddBed("Descrip_2", false);
                context.SaveChanges();

                var Room2 = Hosp.AddRoom(1);
                context.SaveChanges();
                Room2.AddBed("Descrip_3", false);
                var Pat4 = new Patient("Finn", "Shark");
                var Bed4 = Room2.AddBed("Descrip_4", true);
                Pat4.AssignBed(Bed4);
                context.SaveChanges();
            }
        }
}
}
