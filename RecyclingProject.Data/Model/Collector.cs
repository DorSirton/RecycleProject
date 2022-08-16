using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RecyclingProject.Data.Model
{
    public partial class Collector
    {
        public Collector()
        {
            Bottles = new HashSet<Bottle>();
        }

        [Key]
        public int Id { get; set; }
        [Column("First_Name")]
        [StringLength(40)]
        [Display(Name = "First name")]
        public string FirstName { get; set; } = null!;
        [Column("Last_Name")]
        [StringLength(40)]
        [Display(Name = "Last name")]
        public string LastName { get; set; } = null!;
        [Column("Address_Id")]
        public int AddressId { get; set; }
        [StringLength(40)]
        [Display(Name = "Telephone")]
        public string? Tel { get; set; }
        [Column("Current_Num_Of_Bottles")]
        [Display(Name = "Current bottles")]
        public int? CurrentNumOfBottles { get; set; } = 0;
        [Column("Total_History_Num_Of_Bottles")]
        [Display(Name = "Total bottles collected")]
        public int? TotalHistoryNumOfBottles { get; set; } = 0;
        [Column("Current_Worth")]
        [Display(Name = "Current bottles worth")]
        public double? CurrentWorth { get; set; } = 0;
        [Column("Total_Worth_History")]
        [Display(Name = "Total bottles worth")]
        public double? TotalWorthHistory { get; set; } = 0;

        [ForeignKey("AddressId")]
        [InverseProperty("Collectors")]
        public virtual Address Address { get; set; } = null!;
        [InverseProperty("Collector")]
        public virtual ICollection<Bottle> Bottles { get; set; }

        [StringLength(100)]
        public string? Connection_Id { get; set; }
    }
}
