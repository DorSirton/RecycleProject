using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RecyclingProject.Data.Model
{
    public partial class Bottle
    {
        [Key]
        public int Id { get; set; }
        [Column("Category_Id")]
        public int CategoryId { get; set; }
        [Column("Collector_Id")]
        public int? CollectorId { get; set; }
        [Column("Recycler_Id")]
        public int? RecyclerId { get; set; }

        [ForeignKey("CategoryId")]
        [InverseProperty("Bottles")]
        public virtual BottleCategory Category { get; set; } = null!;
        [ForeignKey("CollectorId")]
        [InverseProperty("Bottles")]
        public virtual Collector? Collector { get; set; }
        [ForeignKey("RecyclerId")]
        [InverseProperty("Bottles")]
        public virtual Recycler? Recycler { get; set; }
    }
}
