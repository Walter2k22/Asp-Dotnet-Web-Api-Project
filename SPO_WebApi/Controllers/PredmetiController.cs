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
    public class PredmetiController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PredmetiController(AppDbContext _context)
        {
            this._context = _context;
        }
        [HttpGet]
        [Route("Pretraga")]
    public async Task<IActionResult> Pretraga([FromQuery] string? Sifra, [FromQuery] string? Naziv, [FromQuery] bool? Aktivan)
        {
            var Querry = _context.Predmeti.AsQueryable();

            if(!string.IsNullOrEmpty(Sifra) ) 
            Querry = Querry.Where(x => x.Sifra.ToLower() == Sifra.ToLower());
            if(!string.IsNullOrEmpty(Naziv))
                Querry = Querry.Where(x => x.Naziv.ToLower().Contains(Naziv.ToLower()));
            if(Aktivan.HasValue)
                Querry = Querry.Where(x => x.Aktivan == Aktivan);
                var Rezultat = await Querry.ToListAsync();
                return Ok(Rezultat);
        }
        [HttpGet]
        public async Task<IActionResult> GetPredmeti()
        {
            return Ok(await _context.Predmeti.ToListAsync());
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetPredmet(int id)
        {
           var predmet = await _context.Predmeti.FindAsync(id);
            if (predmet != null)
                return Ok(predmet);
            return NotFound("Predmet ne postoji.");
        }
        [HttpPost]
        public async Task<IActionResult> AddPredmet([FromBody] PredmetAddPutModel data)
        {
            foreach (var predmet in _context.Predmeti.ToList())
                if (predmet.Naziv.ToLower() == data.Naziv.ToLower())
                    return BadRequest("Predmet vec postoji");
            var newPredmet = new Predmet()
            {
                Naziv = data.Naziv,
                Sifra = data.Sifra,
                Aktivan = data.Aktivan
            };
            await _context.Predmeti.AddAsync(newPredmet);
            await _context.SaveChangesAsync();
            return Ok(newPredmet); 
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdatePredmet([FromRoute] int id, PredmetAddPutModel data)
        {
            var predmet = await _context.Predmeti.FindAsync(id);
            if (predmet == null)
                return NotFound("Predmet ne postoji.");
            foreach (var obj in _context.Predmeti.ToList())
                if (data.Naziv == obj.Naziv)
                    return BadRequest("Predmet vec postiji.");

            predmet.Naziv = data.Naziv;
            predmet.Sifra = data.Sifra;
            predmet.Aktivan = data.Aktivan;
            _context.Predmeti.Update(predmet);
            await _context.SaveChangesAsync();
            return Ok(predmet);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePredmet([FromRoute]int id)
        {
            var zaObrisati = await _context.Predmeti.FirstOrDefaultAsync(x => x.Id == id);
            if (zaObrisati != null)
            {
                _context.Predmeti.Remove(zaObrisati);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}
