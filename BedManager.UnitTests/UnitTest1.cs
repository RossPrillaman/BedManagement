using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BedManager.Domain.Entity;
using System.Linq;

namespace BedManager.UnitTests
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void HospitalsLoad()
        {
            DbContextOptionsBuilder<BedManagerContext> optbldr = new DbContextOptionsBuilder<BedManagerContext>().UseInMemoryDatabase("UnitTest1_HospitalsLoad");
            DbContextOptions<BedManagerContext> options = optbldr.Options;
            BedManagerContext context = new BedManagerContext(options);

            TestData.LoadBeds(context);
            var Result = context.Hospitals.Count();
            Assert.AreEqual(1, Result);

        }


        [TestMethod]
        public void GetAllRooms()
        {
            DbContextOptionsBuilder<BedManagerContext> optbldr = new DbContextOptionsBuilder<BedManagerContext>().UseInMemoryDatabase("UnitTest1_GetAllRooms");
            DbContextOptions<BedManagerContext> options = optbldr.Options;
            BedManagerContext context = new BedManagerContext(options);

            TestData.LoadBeds(context);
            var Hosp = context.Hospitals.Include(p => p.Rooms).ThenInclude(r => r.Beds).ToList().First();
            var Result = Hosp.GetBeds().Count();
            Assert.AreEqual(4, Result);

        }


        [TestMethod]
        public void GetAllCleanRooms()
        {
            DbContextOptionsBuilder<BedManagerContext> optbldr = new DbContextOptionsBuilder<BedManagerContext>().UseInMemoryDatabase("UnitTest1_GetAllCleanRooms");
            DbContextOptions<BedManagerContext> options = optbldr.Options;
            BedManagerContext context = new BedManagerContext(options);

            TestData.LoadBeds(context);
            var Hosp = context.Hospitals.Include(p => p.Rooms).ThenInclude(r => r.Beds).ToList().First();
            var Result = Hosp.GetCleanBeds().Count();
            Assert.AreEqual(2, Result);
        }


        [TestMethod]
        public void AssignPatient()
        {
            DbContextOptionsBuilder<BedManagerContext> optbldr = new DbContextOptionsBuilder<BedManagerContext>().UseInMemoryDatabase("UnitTest1_AssignPatient");
            DbContextOptions<BedManagerContext> options = optbldr.Options;
            BedManagerContext context = new BedManagerContext(options);

            TestData.LoadBeds(context);
            var Hosp = context.Hospitals.Include(p => p.Rooms).ThenInclude(r => r.Beds).ToList().First();
            var Bed =  Hosp.GetBeds().First();

       
            var Pat = new Patient("Me", "Ma");
            context.Add(Pat);
            Pat.AssignBed(Bed);
            context.SaveChanges();
            var Result = context.Beds.Where(b => b.Id == Bed.Id).FirstOrDefault();
            Assert.AreNotEqual(Pat.Id, 0);
            Assert.AreEqual(Pat.Id, Result.PatientId);
            Assert.AreEqual(true, Bed.GetCurrentStatus().IsClean);
        }

        [TestMethod]
        public void MaxRooms()
        {
            DbContextOptionsBuilder<BedManagerContext> optbldr = new DbContextOptionsBuilder<BedManagerContext>().UseInMemoryDatabase("UnitTest1_MaxRooms");
            DbContextOptions<BedManagerContext> options = optbldr.Options;
            BedManagerContext context = new BedManagerContext(options);

            TestData.LoadBeds(context);
            var Hosp = context.Hospitals.Include(p => p.Rooms).ThenInclude(r => r.Beds).ToList().First();

            var room = Hosp.Rooms.First();
            for (int i = 0; i <= room.MaxBeds+1; i++)
            {
                var bedname = string.Format("new Bed {0}", i);
                var Bed = room.AddBed(bedname, true);
            }

            context.SaveChanges();

            Assert.AreEqual(room.MaxBeds, room.Beds.Count());
        }

    }
}
