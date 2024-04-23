namespace SSLazio.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DettaglioOrdini")]
    public partial class DettaglioOrdini
    {
        [Key]
        public int IdDettaglioOrdine { get; set; }

        public int IdProdotto { get; set; }

        public int IdOrdine { get; set; }

        public int Quantita { get; set; }

        [Column(TypeName = "money")]
        public decimal Totale { get; set; }

        public virtual Ordini Ordini { get; set; }

        public virtual Prodotti Prodotti { get; set; }
    }
}
