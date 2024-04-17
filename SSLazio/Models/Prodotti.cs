namespace SSLazio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prodotti")]
    public partial class Prodotti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prodotti()
        {
            DettaglioOrdini = new HashSet<DettaglioOrdini>();
        }

        [Key]
        public int IdProdotto { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        public string Img { get; set; }

        [Required]
        [StringLength(150)]
        public string Descrizione { get; set; }

        public int? NumeroMaglia { get; set; }

        [StringLength(50)]
        public string NomeMaglia { get; set; }

        public bool? Patch { get; set; }

        [Column(TypeName = "money")]
        public decimal Prezzo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DettaglioOrdini> DettaglioOrdini { get; set; }
    }
}
