namespace DBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Items_pay
    {
        public int Code_pay { get; set; }

        [Required]
        [StringLength(50)]
        public string Item_pay { get; set; }

        [Column(TypeName = "money")]
        public decimal Item_sum { get; set; }

        [Key]
        public int Code_Items { get; set; }

        public virtual Pay Pay { get; set; }
    }
}
