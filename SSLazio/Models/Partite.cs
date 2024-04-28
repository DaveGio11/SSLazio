namespace SSLazio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Partite")]
    public partial class Partite
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Partite()
        {
            Biglietti = new HashSet<Biglietti>();
            SettoriStadio = new HashSet<SettoriStadio>();
        }

        [Key]
        public int IdPartita { get; set; }

        [Required]
        [StringLength(100)]
        public string NomePartita { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataOra { get; set; }

        [Required]
        public string LogoLazio { get; set; }

        [Required]
        public string LogoAvversario { get; set; }

        [Required]
        public string LogoCompetizione { get; set; }

        [Required]
        [StringLength(50)]
        public string Luogo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Biglietti> Biglietti { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SettoriStadio> SettoriStadio { get; set; }
    }
}
