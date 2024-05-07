using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetoConcessionaria.Controllers
{
    public class ModeloController
    {
         [Route("api/[controller]")]
    [ApiController]
    public class ModeloController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ModeloController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Modelos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Modelo>>> GetModelos()
        {
            return await _context.Modelos.ToListAsync();
        }

        // GET: api/Modelos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Modelo>> GetModelo(int id)
        {
            var modelo = await _context.Modelos.FindAsync(id);

            if (modelo == null)
            {
                return NotFound();
            }

            return modelo;
        }

        // POST: api/Modelos
        [HttpPost]
        public async Task<ActionResult<Modelo>> PostModelo(Modelo modelo)
        {
            _context.Modelos.Add(modelo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetModelo), new { id = modelo.ID_MODELO }, modelo);
        }

        // PUT: api/Modelos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModelo(int id, Modelo modelo)
        {
            if (id != modelo.ID_MODELO)
            {
                return BadRequest();
            }

            _context.Entry(modelo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModeloExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Modelos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModelo(int id)
        {
            var modelo = await _context.Modelos.FindAsync(id);
            if (modelo == null)
            {
                return NotFound();
            }

            _context.Modelos.Remove(modelo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModeloExists(int id)
        {
            return _context.Modelos.Any(e => e.ID_MODELO == id);
        }
    }
    }
}