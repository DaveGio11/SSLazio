namespace SSLazio.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Biglietti")]
    public partial class Biglietti
    {
        [Key]
        public int IdBiglietto { get; set; }

        public int IdUtente { get; set; }

        public int IdPartita { get; set; }

        public int IdSettore { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Cognome { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataNascita { get; set; }

        [Required]
        [StringLength(50)]
        public string LuogoNascita { get; set; }

        [Required]
        [StringLength(16)]
        public string CodiceFiscale { get; set; }

        [Required]
        [StringLength(50)]
        public string Residenza { get; set; }

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

        public virtual Partite Partite { get; set; }

        public virtual SettoriStadio SettoriStadio { get; set; }

        public virtual Utenti Utenti { get; set; }
    }
}
