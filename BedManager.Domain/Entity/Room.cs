using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BedManager.Domain.Entity
{
    public class Room
    {
        private Room()
        {

        }

        public Room(long HospitalId, long MaxBeds)
        {
            this.HospitalId = HospitalId;
            this.MaxBeds = MaxBeds;
            this._beds = new HashSet<Bed>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        public long HospitalId { get; private set; }

        [ForeignKey("HospitalId")]
        public virtual Hospital Hospital { get; private set; }

        public long MaxBeds { get; private set; }

        private HashSet<Bed> _beds;

        public IEnumerable<Bed> Beds => _beds?.ToList();

        public Bed AddBed(string Description, bool IsClean)
        {
            var Bed = new Bed(Description);
            Bed.AddStatus(IsClean);
            this._beds.Add(Bed);
            return Bed;
        }


    }
}