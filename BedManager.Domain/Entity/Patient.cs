using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BedManager.Domain.Entity
{
    public class Patient
    {
        private Patient()
        {

        }

        public Patient(string FirstName, string LastName)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public virtual Bed Bed { get; private set; }

        public void AssignBed(Bed Bed)
        {
            this.Bed = Bed;
        }
    }
}