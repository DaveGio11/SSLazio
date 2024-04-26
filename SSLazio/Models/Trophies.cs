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
        [StringLength(100)]
        public string Descrizione { get; set; }


    }
}
