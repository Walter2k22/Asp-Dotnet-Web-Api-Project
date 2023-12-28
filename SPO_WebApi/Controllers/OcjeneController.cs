using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPO_Data.Data;
using SPO_Data.Models;
using SPO_WebApi.DTOs;
using System.Linq;

namespace SPO_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OcjeneController : ControllerBase
    {
        private readonly AppDbContext _context;
        public OcjeneController(AppDbContext _context)
        {
            this._context = _context;
        }
        [HttpGet]
        [Route("Student/{id:int}")]
        public async Task<IActionResult> GetOcjene([FromRoute] int id)
        {
            var ocjene = _context.Ocjene.ToList().Where(x => x.StudentId == id);
            return Ok(ocjene);
        }
        [HttpPost]
        public async Task<IActionResult> AddOcjena([FromBody] AddOcjenaModel data)
        {
            var novaOcjena = new Ocjena();
            if (ValidirajOcjenu(data))
            {
                novaOcjena.StudentId = data.StudentId;
                novaOcjena.PredmetId = data.PredmetId;
                novaOcjena.Uspjeh = data.Uspjeh;
                novaOcjena.Napomena = data.Napomena;
                await _context.Ocjene.AddAsync(novaOcjena);
                await _context.SaveChangesAsync();
                return Ok(novaOcjena);
            }
            return BadRequest();
        }
    
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateOcjena([FromRoute] int id, AddOcjenaModel data)
        {
            var ocjena = await _context.Ocjene.FindAsync(id);
            if (ocjena == null) return BadRequest();
            if (!ValidirajOcjenu(data)) return BadRequest();
            ocjena.StudentId = data.StudentId;
            ocjena.PredmetId = data.PredmetId;
            ocjena.Uspjeh = data.Uspjeh;
            ocjena.Napomena = data.Napomena;
             _context.Ocjene.Update(ocjena);
            await _context.SaveChangesAsync();
            return Ok(ocjena);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Deleteocjena([FromRoute] int id)
        {
            var ocjena = _context.Ocjene.ToList().FirstOrDefault(x => x.Id == id);
            if (ocjena == null) return NotFound();
            _context.Ocjene.Remove(ocjena);
            await _context.SaveChangesAsync();
            return Ok();
        }
        bool ValidirajOcjenu(AddOcjenaModel data)
        {
            var student = _context.Studenti.ToList().FirstOrDefault(x => x.Id == data.StudentId);
            var predmet = _context.Predmeti.ToList().FirstOrDefault(x => x.Id == data.PredmetId);
            if (student == null || predmet == null || (data.Uspjeh < 5 || data.Uspjeh > 10))
                return false;
            return true;

        }
    }
}
