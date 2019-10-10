using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BedManager.Domain.Entity
{
    public class Hospital
    {
        private Hospital()
        {

        }

        public Hospital(string Name)
        {
            this.Name = Name;
            _rooms = new HashSet<Room>();

        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        public string Name { get; private set; }

        private HashSet<Room> _rooms;

        public IEnumerable<Room> Rooms => _rooms?.ToList();



        public Room AddRoom(long MaxBeds)
        {
            var room = new Room(Id, MaxBeds);
            this._rooms.Add(room);
            return room;
        }

        public List<Bed> GetBeds()
        {
            var AllBeds = new List<Bed>();
            AllBeds = this.Rooms.SelectMany(r => r.Beds).ToList();
            return AllBeds;
        }

        public List<Bed> GetBedClean(BedManagerContext context = null)
        {

                var AllBeds = new List<Bed>();
                AllBeds = this.Rooms.SelectMany(r => r.Beds).Where(b=> b.BedStatuses.OrderByDescending(bs => bs.Id).FirstOrDefault().IsClean).ToList();
                return AllBeds;


        }

    }
}