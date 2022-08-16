using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RecyclingProject.Data.Model
{
    public partial class City
    {
        public City()
        {
            Addresses = new HashSet<Address>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(15)]
        public string Name { get; set; } = null!;
        [Column("Country_Id")]
        public int? CountryId { get; set; }

        [ForeignKey("CountryId")]
        [InverseProperty("Cities")]
        public virtual Country? Country { get; set; }
        [InverseProperty("City")]
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
