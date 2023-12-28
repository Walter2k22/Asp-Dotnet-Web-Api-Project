using System.ComponentModel.DataAnnotations;

namespace SPO_WebApi.DTOs
{
    public class PredmetAddPutModel
    {
        [Required]
        public string Naziv { get; set; } = string.Empty;
        [Required]
        public string Sifra { get; set; } = string.Empty;
        [Required]
        public bool Aktivan { get; set; }
    }
}
