using Microsoft.AspNetCore.Mvc;

namespace curso.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuperHeroController : ControllerBase
    {
        public static List<Comic> Comics = new List<Comic>{
            new Comic{Id = 1, Name = "Marvel"},
            new Comic{Id = 2, Name = "DC"}
        };

        public static List<SuperHero> SuperHeroes = new List<SuperHero>{
            new SuperHero {
                Id = 1, 
                FirstName = "Peter",
                LastName = "Parker",
                HeroName = "Spider-Man",
                Comic = Comics[0],
                ComicId = 1
            },
            new SuperHero {
                Id = 2,
                FirstName = "Bruce",
                LastName = "Wayne",
                HeroName = "Batman",
                Comic = Comics[1],
                ComicId = 2
            }
        };

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes()
        {
            return Ok(SuperHeroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetSingleHero(int id)
        {
            var hero = SuperHeroes.FirstOrDefault(h => h.Id == id);
            if (hero == null)
            {
                return NotFound("Herói não enconstrado.");
            }
            return Ok(hero);
        }

        [HttpGet("comics")]
        public async Task<ActionResult<List<Comic>>> GetComics()
        {
            return Ok(Comics);
        }
    }
}