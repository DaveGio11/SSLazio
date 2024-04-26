namespace SSLazio.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class Memories


    {
        [Key]
        public int IdStoria { get; set; }

        [Required]
        public string ImgStoria { get; set; }

        [Required]
        [StringLength(50)]
        public string TitoloStoria { get; set; }

        [Required]
        [StringLength(4)]
        public string DataStoria { get; set; }

        [Required]
        public string Descrizione { get; set; }

    }
}
