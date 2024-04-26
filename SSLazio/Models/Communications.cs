namespace SSLazio.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Communications
    {
        [Key]
        public int IdComunicazione { get; set; }

        [Required]
        [StringLength(40)]
        public string TitoloCom { get; set; }

        [Required]
        public string Contenuto { get; set; }

        [Required]
        public string ImgComunicazione { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataComunicazione { get; set; }

    }
}
