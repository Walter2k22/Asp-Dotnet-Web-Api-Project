using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SPO_Data.Models
{
    public class Ocjena
    {
        [Key]
        public int Id { get; set; }
        public int PredmetId { get; set; }
        [JsonIgnore]
        public Predmet Predmet { get; set; }
        public int StudentId { get; set; }
        [JsonIgnore]
        public Student Student { get; set; }
        public string Napomena { get; set; } = string.Empty;
        public int Uspjeh {  get; set; }
    }
}
