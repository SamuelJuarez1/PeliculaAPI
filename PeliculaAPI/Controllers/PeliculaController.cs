using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculaAPI.Data;
using PeliculaAPI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaController : ControllerBase
    {
        // Inyecion de dependencias
        private readonly ApplicationDbContext _db;
        public PeliculaController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetPeliculas()
        {
            var lista = await _db.Peliculas.OrderBy(p => p.NombrePelicula).Include(p => p.Categoria).ToListAsync();

            return Ok(lista); //Muestra codig 200 con la lista de las peliculas
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPeliculas(int id)
        {

            var obj = await _db.Peliculas.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);

        }

        [HttpPost]
        public async Task<IActionResult> CrearPelicula([FromBody] Pelicula pelicula)
        {

            if (pelicula == null)
            {

                return BadRequest(ModelState);

            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _db.AddAsync(pelicula);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("BuscarPorCategoria")]
        public async Task<IActionResult> BuscarPorCategoria(int idCategoria)
        {
            var pelicula = await _db.Peliculas
                .Where(p => p.CategoriaId == idCategoria)
                .ToListAsync();
            if (pelicula == null)
                    {
                return NotFound();

            }
            return Ok(pelicula);
        }
    }
}
