using System.ComponentModel.DataAnnotations;

namespace SPO_WebApi.DTOs
{
    public class StudentAddPutModel
    {
        public string BrojIndeksa { get; set; }
        [Required]
        public string Ime { get; set; }
        [Required]
        public string Prezime { get; set; }
        [Required]
        public DateTime DatumRodjenja { get; set; }
        [Required]
        public string Fakultet { get; set; }
        [Required]
        public bool Aktivan { get; set; }
    }
}
