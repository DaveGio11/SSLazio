namespace SSLazio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SettoriAbb")]
    public partial class SettoriAbb
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SettoriAbb()
        {
            Abbonamenti = new HashSet<Abbonamenti>();
        }

        [Key]
        public int IdSettoreAbb { get; set; }

        [Required]
        [StringLength(50)]
        public string NomeSettore { get; set; }

        [Column(TypeName = "money")]
        public decimal Intero { get; set; }

        [Column(TypeName = "money")]
        public decimal Under16 { get; set; }

        [Column(TypeName = "money")]
        public decimal Aquilotto { get; set; }

        public int PostiDisponibili { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Abbonamenti> Abbonamenti { get; set; }
    }
}
