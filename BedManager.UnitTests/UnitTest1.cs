using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BedManager.Domain.Entity;
using System.Linq;

namespace BedManager.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        static DbContextOptionsBuilder<BedManagerContext> optbldr = new DbContextOptionsBuilder<BedManagerContext>().UseInMemoryDatabase("Test");
        static DbContextOptions<BedManagerContext> options = optbldr.Options;
        static BedManagerContext context = new BedManagerContext(options);

        [TestMethod]
        public void HospitalsLoad()
        {
            TestData.LoadBeds(context);
            var Result = context.Hospitals.Count();
            Assert.AreEqual(1, Result);

        }


        [TestMethod]
        public void GetAllRooms()
        {
            DbContextOptions<BedManagerContext> options = optbldr.Options;
            BedManagerContext context = new BedManagerContext(options);

            TestData.LoadBeds(context);
            var Hosp = context.Hospitals.Include(p => p.Rooms).ThenInclude(r => r.Beds).Where(H => H.Id == 1).ToList().First();
            var Result = Hosp.GetBeds().Count();
            Assert.AreEqual(4, Result);

        }


        [TestMethod]
        public void GetAllCleanRooms()
        {
            DbContextOptions<BedManagerContext> options = optbldr.Options;
            BedManagerContext context = new BedManagerContext(options);

            TestData.LoadBeds(context);
            var Hosp = context.Hospitals.Include(p => p.Rooms).ThenInclude(r => r.Beds).Where(H => H.Id == 1).ToList().First();
            var Result = Hosp.GetBedClean().Count();
            Assert.AreEqual(2, Result);
        }


        [TestMethod]
        public void AssignPatient()
        {


            TestData.LoadBeds(context);
            var Hosp = context.Hospitals.Include(p => p.Rooms).ThenInclude(r => r.Beds).Where(H => H.Id == 1).ToList().First();
            var Bed =  Hosp.GetBedClean().First();

            var Pat = new Patient("Me", "Ma");
            context.Add(Pat);
            Pat.AssignBed(Bed);
            context.SaveChanges();
            var Result = context.Beds.Where(b => b.Id == Bed.Id).FirstOrDefault();
            Assert.AreNotEqual(Pat.Id, 0);
            Assert.AreEqual(Pat.Id, Result.PatientId);
        }

        [TestMethod]
        public void MaxRooms()
        {
            DbContextOptions<BedManagerContext> options = optbldr.Options;
            BedManagerContext context = new BedManagerContext(options);

            TestData.LoadBeds(context);
            var Hosp = context.Hospitals.Include(p => p.Rooms).ThenInclude(r => r.Beds).Where(H => H.Id == 1).ToList().First();

            var room = Hosp.Rooms.First();
            for (int i = 0; i <= room.MaxBeds+1; i++)
            {
                var Bed = room.AddBed("new Bed x", true);
            }

            context.SaveChanges();

            Assert.AreEqual(room.Beds.Count(), room.MaxBeds);
        }

    }
}
