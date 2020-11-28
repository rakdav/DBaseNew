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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pay()
        {
            Items_pay = new HashSet<Items_pay>();
        }

        public int T_number { get; set; }

        [Key]
        public int Code_pay { get; set; }

        [Column(TypeName = "date")]
        public DateTime Pay_day { get; set; }

        [Column(TypeName = "money")]
        public decimal Sum_pay { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Items_pay> Items_pay { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
