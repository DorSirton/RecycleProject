using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RecyclingProject.Data.Model
{
    [Table("Bottle_Categories")]
    public partial class BottleCategory
    {
        public BottleCategory()
        {
            Bottles = new HashSet<Bottle>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        public string Name { get; set; } = null!;
        [Column("Recycle_Price")]
        public double RecyclePrice { get; set; }
        [Column("Photo_Url")]
        public string? PhotoUrl { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<Bottle> Bottles { get; set; }
    }
}
