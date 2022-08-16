using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RecyclingProject.Data.Model
{
    public partial class Address
    {
        public Address()
        {
            Collectors = new HashSet<Collector>();
            Recyclers = new HashSet<Recycler>();
        }

        [Key]
        public int Id { get; set; }
        [Column("City_Id")]
        public int CityId { get; set; }
        [Column("Address")]
        [StringLength(40)]
        public string Address1 { get; set; } = null!;
        public string? Cordin_lat { get; set; }
        public string? Cordin_lng { get; set; }

        [ForeignKey("CityId")]
        [InverseProperty("Addresses")]
        public virtual City City { get; set; } = null!;
        [InverseProperty("Address")]
        public virtual ICollection<Collector> Collectors { get; set; }
        [InverseProperty("Address")]
        public virtual ICollection<Recycler> Recyclers { get; set; }
    }
}
