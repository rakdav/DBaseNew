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
        public int T_number { get; set; }

        public int Code_items { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Pay_day { get; set; }

        [Column(TypeName = "money")]
        public decimal Sum_pay { get; set; }

        [Key]
        public int id_pay { get; set; }

        public virtual Items_pay Items_pay { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
