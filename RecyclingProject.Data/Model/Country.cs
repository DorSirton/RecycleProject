using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RecyclingProject.Data.Model
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(15)]
        public string Name { get; set; } = null!;

        [InverseProperty("Country")]
        public virtual ICollection<City> Cities { get; set; }
    }
}
