using System.Net.Http.Json;
using curso.Shared;

namespace curso.Client.Services.SuperHeroService;

public class SuperHeroService : ISuperHeroService
{
    private readonly HttpClient _http;

    public SuperHeroService(HttpClient http)
    {
        _http = http;
    }

    public List<SuperHero> Heroes { get; set; } = new List<SuperHero>();
    public List<Comic> Comics { get; set; } = new List<Comic>();

    public async Task GetComics()
    {
        var result  = await _http.GetFromJsonAsync<List<Comic>>("api/superhero/comics");
        if (result != null)
            Comics = result;
    }

    public async Task<SuperHero> GetSingleSuperHero(int id)
    {
        var result  = await _http.GetFromJsonAsync<SuperHero>($"api/superhero/{id}");
        if (result != null)
            return result;
        throw new Exception("Heroi não encontrado");
    }

    public async Task GetSuperHeroes()
    {
        var result  = await _http.GetFromJsonAsync<List<SuperHero>>("api/superhero");
        if (result != null)
            Heroes = result;
        
    }
}