using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BedManager.Domain.Entity
{
    public class BedStatus
    {
        private BedStatus()
        {

        }

        public BedStatus(long BedId, bool IsClean )
        {
            this.BedId = BedId;
            this.IsClean = IsClean;
        }
        public BedStatus(Bed Bed, bool IsClean)
        {
            this.Bed = Bed;
            this.IsClean = IsClean;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        public long BedId { get; private set; }

        [ForeignKey("BedId")]
        public virtual Bed Bed { get; private set; }

        public bool IsClean { get; private set; }
    }
}