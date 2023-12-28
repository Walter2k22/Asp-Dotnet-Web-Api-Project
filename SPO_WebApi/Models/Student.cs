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
    public class Student
    {
            [Key]
            public int Id { get; set; }
            [Required]
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
           [JsonIgnore]
            public List<Ocjena> Ocjene { get; set; }
        }
    }

