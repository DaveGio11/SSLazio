namespace SSLazio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        [Key]
        public int IDNews { get; set; }

        [Required]
        [StringLength(50)]
        public string TitoloNews { get; set; }

        [Required]
        [StringLength(50)]
        public string Sottotitolo { get; set; }

        [Required]
        public string Descrizione { get; set; }

        [Required]
        public string ImgNews { get; set; }
    }
}
