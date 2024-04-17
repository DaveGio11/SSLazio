namespace SSLazio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SettoriStadio")]
    public partial class SettoriStadio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SettoriStadio()
        {
            Biglietti = new HashSet<Biglietti>();
        }

        [Key]
        public int IdSettore { get; set; }

        [Required]
        [StringLength(50)]
        public string NomeSettore { get; set; }

        [Column(TypeName = "money")]
        public decimal Intero { get; set; }

        [Column(TypeName = "money")]
        public decimal Ridotto { get; set; }

        public int PostiDisponibili { get; set; }

        public int IdPartita { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Biglietti> Biglietti { get; set; }

        public virtual Partite Partite { get; set; }
    }
}
