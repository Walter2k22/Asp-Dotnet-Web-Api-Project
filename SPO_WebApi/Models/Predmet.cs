using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SPO_Data.Models
{
    public class Predmet
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Required]
        public string Sifra { get; set; }
        [Required]
        public bool Aktivan { get; set; }
        [JsonIgnore]
        public List<Ocjena> Ocjene { get; set; }
    }
}
