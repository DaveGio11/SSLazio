namespace SSLazio.Models
{

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Abbonamenti")]
    public partial class Abbonamenti
    {
        [Key]
        public int IdAbbonamento { get; set; }

        public int IdUtente { get; set; }

        public int IdSettoreAbb { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Cognome { get; set; }

        [Required]
        public string ImgAbbonato { get; set; }

        [Required]
        [StringLength(50)]
        public string LuogoNascita { get; set; }

        [Required]
        [StringLength(50)]
        public string Residenza { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataNascita { get; set; }

        [Required]
        [StringLength(16)]
        public string CodiceFiscale { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoDocumento { get; set; }

        [Required]
        [StringLength(10)]
        public string NumeroDocumento { get; set; }

        [Required]
        [StringLength(50)]
        public string Riduzione { get; set; }

        public decimal Prezzo { get; set; }

        [Required(ErrorMessage = "Il campo NumeroCarta è obbligatorio.")]
        [StringLength(19, ErrorMessage = "Il campo NumeroCarta deve avere una lunghezza massima di 19 caratteri.")]
        [RegularExpression(@"^\d{4}-\d{4}-\d{4}-\d{4}$", ErrorMessage = "Il campo NumeroCarta deve essere nel formato '0000-0000-0000-0000'.")]
        public string NumeroCarta { get; set; }


        [Required]
        [StringLength(100)]
        public string Intestatario { get; set; }

        [Required]
        [StringLength(5)]
        [RegularExpression(@"^\d{2}-\d{2}$", ErrorMessage = "Il campo NumeroCarta deve essere nel formato '00-00'.")]
        public string Scadenza { get; set; }

        [Required]
        [StringLength(3)]
        public string Cvc { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataPagamento { get; set; }

        [Required]
        [StringLength(100)]
        public string EmailDestinatario { get; set; }

        public virtual SettoriAbb SettoriAbb { get; set; }

        public virtual Utenti Utenti { get; set; }
    }
}
