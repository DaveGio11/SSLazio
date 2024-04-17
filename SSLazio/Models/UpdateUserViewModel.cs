using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSLazio.Models
{
    public class UpdateUserViewModel
    {
        [Required]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Nome { get; set; }

        [StringLength(50)]
        public string Cognome { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataNascita { get; set; }

        [StringLength(50)]
        public string LuogoNascita { get; set; }

        [StringLength(16)]
        public string CodiceFiscale { get; set; }

        [StringLength(50)]
        public string Residenza { get; set; }

    }
}
