using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projetoConcessionaria.Models;

namespace projetoConcessionaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FabricantesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FabricantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Implantação dos métodos CRUD:

        // GET: api/Fabricantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fabricante>>> GetFabricantes()
        {
            // Retorna todos os fabricantes do banco de dados
            return await _context.Fabricantes.ToListAsync();
        }

        // GET: api/Fabricantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fabricante>> GetFabricante(int id)
        {
            // Procura o fabricante com o ID fornecido no banco de dados
            var fabricante = await _context.Fabricantes.FindAsync(id);

            if (fabricante == null)
            {
                // Retorna 404 Not Found se o fabricante não for encontrado
                return NotFound();
            }

            // Retorna o fabricante encontrado
            return fabricante;
        }

        // POST: api/Fabricantes
        [HttpPost]
        public async Task<ActionResult<Fabricante>> PostFabricante(Fabricante fabricante)
        {
            // Adiciona o novo fabricante ao contexto do Entity Framework
            _context.Fabricantes.Add(fabricante);
            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();

            // Retorna o fabricante criado com o código de status 201 Created
            return CreatedAtAction(nameof(GetFabricante), new { id = fabricante.ID_FABRICANTE }, fabricante);
        }

        // PUT: api/Fabricantes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFabricante(int id, Fabricante fabricante)
        {
            if (id != fabricante.ID_FABRICANTE)
            {
                // Retorna 400 Bad Request se o ID fornecido não corresponder ao ID do fabricante
                return BadRequest();
            }

            // Marca o fabricante como modificado para atualizá-lo no banco de dados
            _context.Entry(fabricante).State = EntityState.Modified;

            try
            {
                // Salva as alterações no banco de dados
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FabricanteExists(id))
                {
                    // Retorna 404 Not Found se o fabricante não for encontrado
                    return NotFound();
                }
                else
                {
                    // Retorna 500 Internal Server Error se ocorrer um erro inesperado
                    throw;
                }
            }

            // Retorna 204 No Content para indicar que a atualização foi bem-sucedida
            return NoContent();
        }

        // DELETE: api/Fabricantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFabricante(int id)
        {
            // Procura o fabricante com o ID fornecido no banco de dados
            var fabricante = await _context.Fabricantes.FindAsync(id);
            if (fabricante == null)
            {
                // Retorna 404 Not Found se o fabricante não for encontrado
                return NotFound();
            }

            // Remove o fabricante do contexto do Entity Framework
            _context.Fabricantes.Remove(fabricante);
            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();

            // Retorna 204 No Content para indicar que a exclusão foi bem-sucedida
            return NoContent();
        }

        private bool FabricanteExists(int id)
        {
            // Verifica se existe um fabricante com o ID fornecido no banco de dados
            return _context.Fabricantes.Any(e => e.ID_FABRICANTE == id);
        }
    }
}
