using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPO_Data.Data;
using SPO_Data.Models;
using SPO_WebApi.DTOs;

namespace SPO_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentiController : ControllerBase
    {
        public readonly AppDbContext _context;
        public StudentiController(AppDbContext _context) {
            this._context = _context;
        }
        [HttpGet]
        public async Task<IActionResult> Studenti()
        {
            return Ok(await _context.Studenti.ToListAsync());

        }
        [HttpGet("Pretraga")]
        public async Task<IActionResult> Pretraga([FromQuery] string? BrojIndeksa, [FromQuery] string? Ime, [FromQuery] bool? Aktivan)
        {
            var Filter = _context.Studenti.AsQueryable();

            if (!string.IsNullOrEmpty(BrojIndeksa))
                Filter = Filter.Where(x => x.BrojIndeksa == BrojIndeksa);
            if (!string.IsNullOrEmpty(Ime))
                Filter = Filter.Where(x => x.Ime.ToLower().Contains(Ime.ToLower()));
            if (Aktivan.HasValue)
                Filter = Filter.Where(x => x.Aktivan == Aktivan);
            var RezultatPretrage = await Filter.ToListAsync();
            return Ok(RezultatPretrage);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetStudent([FromRoute] int id)
        {
            var Student = await _context.Studenti.FirstOrDefaultAsync(x => x.Id == id);
            if (Student != null)
                return Ok(Student);
            else
                return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentAddPutModel data)
        {
            foreach (var student in _context.Studenti.ToList())
                if (data.BrojIndeksa == student.BrojIndeksa)
                    return BadRequest ("Student sa ovim indeksom vec postoji.");
            var newStudent = new Student()
            {
                Ime = data.Ime,
                Prezime = data.Prezime,
                DatumRodjenja = data.DatumRodjenja,
                BrojIndeksa = data.BrojIndeksa,
                Fakultet = data.Fakultet,
                Aktivan = data.Aktivan
            };
            await _context.Studenti.AddAsync(newStudent);
            await _context.SaveChangesAsync();
            return Ok(newStudent);
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] int id, [FromBody] StudentAddPutModel data)
        {
            var Student = await _context.Studenti.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (Student == null)
                return NotFound();
            Student.Ime = data.Ime;
            Student.Prezime = data.Prezime;
            Student.DatumRodjenja = data.DatumRodjenja;
            Student.Fakultet = data.Fakultet;
            Student.Aktivan = data.Aktivan;
            Student.BrojIndeksa = data.BrojIndeksa;

            _context.Studenti.Update(Student);
            await _context.SaveChangesAsync();
            return Ok(Student);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            var student = await _context.Studenti.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if(student != null)
            {
                 _context.Studenti.Remove(student);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}
