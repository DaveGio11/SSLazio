namespace SSLazio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Calciatrici")]
    public partial class Calciatrici
    {
        [Key]
        public int IDCalciatrice { get; set; }

        [Required]
        public string ImgCalciatrice { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        public string Cognome { get; set; }

        [Required]
        [StringLength(50)]
        public string Ruolo { get; set; }

        public int NumeroMaglia { get; set; }

        [Required]
        [StringLength(100)]
        public string Nazionalita { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataNascita { get; set; }

        [Required]
        public string Descrizione { get; set; }

        public int Presenze { get; set; }

        public int MinutiGiocati { get; set; }

        public int Gol { get; set; }

        public int Assist { get; set; }

        public int CartelliniGialli { get; set; }

        public int CartelliniRossi { get; set; }

        [Required]
        public string ImgRuolo { get; set; }

        [Required]
        public string ImgCalciatrice2 { get; set; }
    }
}
