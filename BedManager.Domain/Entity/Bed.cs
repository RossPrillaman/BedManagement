using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BedManager.Domain.Entity
{
    public class Bed
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        public string Description { get; private set; }

        private HashSet<BedStatus> _bedStatuses;

        public IEnumerable<BedStatus> BedStatuses => _bedStatuses?.ToList();

        public long RoomId { get; private set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; private set; }

        public long PatientId { get; private set; }

        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; private set; }


        private Bed()
        {
            this.Description = "";
            this._bedStatuses = new HashSet<BedStatus>();
        }


        public Bed(string Description)
        {
            this.Description = Description;
            this._bedStatuses = new HashSet<BedStatus>();
        }

        public Bed(string Description, bool IsClean)
        {
            this.Description = Description;
            var NewStatus = new BedStatus(Id, IsClean);
        }

        public BedStatus GetCurrentStatus()
        {
            var CurrentBedStatus = this.BedStatuses.FirstOrDefault();
            return CurrentBedStatus;
        }

        public void AddStatus(bool IsClean)
        {
            var NewBedStatus = new BedStatus(this, IsClean);
            this._bedStatuses.Add(NewBedStatus);
        }



}
}