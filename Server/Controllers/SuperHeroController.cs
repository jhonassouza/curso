using Microsoft.AspNetCore.Mvc;

namespace curso.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;
        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        // GET: api/SuperHero
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes()
        {
            var heroes = await _context.SuperHeroes
            .Include(h => h.Comic)
            .ToListAsync();
            if (heroes == null)
            {
                return NotFound("Nenhum herói encontrado.");
            }
            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetSingleHero(int id)
        {
            var hero = await _context.SuperHeroes
            .Include(h => h.Comic)
            .FirstOrDefaultAsync(h => h.Id == id);
            if (hero == null)
            {
                return NotFound("Herói não enconstrado.");
            }
            Console.WriteLine(hero.Comic.Name);
            return Ok(hero);
        }

        [HttpGet("comics")]
        public async Task<ActionResult<List<Comic>>> GetComics()
        {
            var comics = await _context.Comics.ToListAsync();
            if (comics == null)
            {
                return NotFound("Nenhuma história em quadrinhos encontrada.");
            }
            return Ok(comics);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> CreateSuperHero(SuperHero hero)
        {
            hero.Comic = null;
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await GetDbHeroes());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<SuperHero>>> UpdateSuperHero(SuperHero hero, int id)
        {
            var dbHero = await _context.SuperHeroes
            .Include(h => h.Comic)
            .FirstOrDefaultAsync(h => h.Id == id);

            if (dbHero == null)
            {
                return NotFound("Herói não encontrado.");
            }

            dbHero.FirstName = hero.FirstName;
            dbHero.LastName = hero.LastName;
            dbHero.HeroName = hero.HeroName;
            dbHero.ComicId = hero.ComicId;

            await _context.SaveChangesAsync();
            
            return Ok(await GetDbHeroes());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteSuperHero(int id)
        {
            var dbHero = await _context.SuperHeroes
            .Include(h => h.Comic)
            .FirstOrDefaultAsync(h => h.Id == id);
            if (dbHero == null)
            {
                return NotFound("Herói não encontrado.");
            }
            else
            {
                _context.SuperHeroes.Remove(dbHero);
                await _context.SaveChangesAsync();
            }
            return Ok(await GetDbHeroes());
        }

        private async Task<List<SuperHero>> GetDbHeroes()
        {
            return await _context.SuperHeroes
            .Include(h => h.Comic)
            .ToListAsync();
        }
    }
}