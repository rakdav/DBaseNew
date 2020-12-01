namespace DBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pay")]
    public partial class Pay
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int T_number { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Code_items { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime Pay_day { get; set; }

        [Column(TypeName = "money")]
        public decimal? Sum_pay { get; set; }

        public virtual Items_pay Items_pay { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
