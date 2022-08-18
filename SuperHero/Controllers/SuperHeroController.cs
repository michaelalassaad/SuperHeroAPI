using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHero.Data;

namespace SuperHero.Controllers;

[Route("api/[controller]")]
[ApiController]

public class SuperHeroController : Controller
{
    private readonly DataContext _dataContext;

    public SuperHeroController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    // GET
    [HttpGet]
    public async Task<ActionResult<List<Models.SuperHero>>> GetHero()
    {
        return Ok(await _dataContext.SuperHeroes.ToListAsync());
    }
    
    // Get Single
    [HttpGet("{Id}")]
    public async Task<ActionResult<List<Models.SuperHero>>> GetSingleHero(int Id)
    {
        var hero = await _dataContext.SuperHeroes.FindAsync(Id);
        if (hero == null)
            return BadRequest("Hero Not Found");
        return Ok(hero);
    }
    
    //POST
    [HttpPost]
    public async Task<ActionResult<List<Models.SuperHero>>> AddHero(Models.SuperHero hero)
    { 
        await _dataContext.AddAsync(hero);
        await _dataContext.SaveChangesAsync();
        
        return Ok(await _dataContext.SuperHeroes.ToListAsync());
    }
    
    //UPDATE
    [HttpPut]
    public async Task<ActionResult<List<Models.SuperHero>>> UpdateHero(Models.SuperHero request)
    {
        var dbHero = await _dataContext.SuperHeroes.FindAsync(request.Id);
        if (dbHero == null) 
            return BadRequest("Hero Not Found");
        
        dbHero.Name = request.Name;
        dbHero.FirstName = request.FirstName;
        dbHero.LastName = request.LastName;
        dbHero.Place = request.Place;
        await _dataContext.SaveChangesAsync();
        
        return Ok(await _dataContext.SuperHeroes.ToListAsync());
    }
    
    //Delete
    [HttpDelete("{Id}")]
    public async Task<ActionResult<List<Models.SuperHero>>> DeleteHero(int Id)
    {
        var dbHero = await _dataContext.SuperHeroes.FindAsync(Id);
        if (dbHero == null) 
            return BadRequest("Hero Not Found");

        _dataContext.Remove(dbHero);
        await _dataContext.SaveChangesAsync();
        
        return Ok(await _dataContext.SuperHeroes.ToListAsync());
    }
}