using FactureAbonnement.API.Data;
using FactureAbonnement.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FactureAbonnement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbonnementController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AbonnementController(AppDbContext context)
        {
            _context = context;
        }
    

        //GET: api/Abonnement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Abonnement>>> GetAbonnement()
        {
            return await _context.Abonnements.ToListAsync();
        }

        // GET: api/Abonnement/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Abonnement>> GetAbonnement(Guid id)
        {
            var abo = await _context.Abonnements.FindAsync(id);
            if (abo == null) return NotFound();
            return abo;
        }

        [HttpPost]
        public async Task<ActionResult<Abonnement>> CreerAbonnement([FromBody] Abonnement abo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            abo.Id = Guid.NewGuid();
            abo.Statut = SubscriptionStatut.Actif;

            _context.Abonnements.Add(abo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAbonnement), new { id = abo.Id }, abo);
        }


        // PUT: api/Abonnement/{id}/modifier
        [HttpPut("{id}/modifier")]
        public async Task<IActionResult> Modifier(Guid id, decimal tarif, DateTime nouvelleDateFin)
        {
            var abo = await _context.Abonnements.FindAsync(id);
            if (abo == null) return NotFound();

            try
            {
                abo.Modifier(tarif, nouvelleDateFin);
                await _context.SaveChangesAsync();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        // PUT: api/Abonnement/{id}/suspendre
        [HttpPut("{id}/suspendre")]
        public async Task<IActionResult> Suspendre(Guid id)
        {
            var abo = await _context.Abonnements.FindAsync(id);
            if (abo == null) return NotFound();

            abo.Suspendre();
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Abonnement/{id}/resilier
        [HttpPut("{id}/resilier")]
        public async Task<IActionResult> Resilier(Guid id)
        {
            var abo = await _context.Abonnements.FindAsync(id);
            if (abo == null) return NotFound();

            abo.Resilier();
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}