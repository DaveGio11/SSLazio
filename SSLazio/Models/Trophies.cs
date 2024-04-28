namespace SSLazio.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class Trophies
    {
        [Key]
        public int IDTrofeo { get; set; }

        [Required]
        public string ImgTrofeo { get; set; }

        [Required]
        public string ImgSquadra { get; set; }

        [Required]
        [StringLength(50)]
        public string NomeTrofeo { get; set; }

        [Required]
        [StringLength(9)]
        public string AnnoTrofeo { get; set; }

        [Required]
        public string Descrizione { get; set; }

        [StringLength(500)]
        public string Formazione { get; set; }

        [StringLength(50)]
        public string TextDetail1 { get; set; }

        public string ImgDettaglio1 { get; set; }

        [StringLength(50)]
        public string TextDetail2 { get; set; }

        public string ImgDettaglio2 { get; set; }

        [StringLength(50)]
        public string TextDetail3 { get; set; }

        public string ImgDettaglio3 { get; set; }

        [StringLength(50)]
        public string TextDetail4 { get; set; }

        public string ImgDettaglio4 { get; set; }

    }
}
