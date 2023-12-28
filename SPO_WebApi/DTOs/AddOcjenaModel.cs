using System.ComponentModel.DataAnnotations;

namespace SPO_WebApi.DTOs
{
    public class AddOcjenaModel
    {
        [Required]
        public int PredmetId { get; set; }
        [Required]
        public int Uspjeh { get; set;}
        [Required]
        public int StudentId { get; set; }
        public string Napomena { get; set; } = string.Empty;
    }
}
