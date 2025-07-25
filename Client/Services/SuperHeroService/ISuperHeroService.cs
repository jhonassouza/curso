using curso.Shared;

namespace curso.Client.Services.SuperHeroService;

    public interface ISuperHeroService
    {
        List<SuperHero> Heroes { get; set; }
        List<Comic> Comics { get; set; }

        Task GetComics();
        Task GetSuperHeroes();
        Task<SuperHero> GetSingleSuperHero(int id);
        Task CreateSuperHero(SuperHero hero);
        Task UpdateSuperHero(SuperHero hero);
        Task DeleteSuperHero(int id);
    }
