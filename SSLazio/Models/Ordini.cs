namespace SSLazio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ordini")]
    public partial class Ordini
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ordini()
        {
            DettaglioOrdini = new HashSet<DettaglioOrdini>();
        }

        [Key]
        public int IdOrdine { get; set; }

        public int IdUtente { get; set; }

        [Required]
        [StringLength(100)]
        public string Indirizzo { get; set; }

        [Required]
        [StringLength(100)]
        public string Citta { get; set; }

        [Required]
        [StringLength(5)]
        public string Cap { get; set; }

        public int Qta { get; set; }

        [Column(TypeName = "money")]
        public decimal PrezzoTot { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataOrdine { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DettaglioOrdini> DettaglioOrdini { get; set; }

        public virtual Utenti Utenti { get; set; }
    }
}
