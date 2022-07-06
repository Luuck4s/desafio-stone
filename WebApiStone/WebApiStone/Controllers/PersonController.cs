using WebApiStone.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApiStone.Services;
using WebApiStone.Models;
using WebApiStone.Hubs;

namespace WebApiStone.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService) =>
        _personService = personService;

    [HttpGet("GetAll")]
    public async Task<ResultPerson> GetAll(int page, string? name, int perpage = 10)
    {
        return await _personService.GetAll(page, perpage, name);
    }
        

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Person>> Get(string id)
    {
       var person = await _personService.GetById(id);

       if (person is null)
       {
           return NotFound();
       }

       return Ok(person);
    }

    [HttpPost]
    public async Task<ActionResult<Person>> Post(Person person)
    {
        return await _personService.Create(person);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Person personUpdated)
    {
        var person = await _personService.GetById(id);

        if (person is null)
        {
            return NotFound();
        }

        personUpdated.Id = person.Id;

        await _personService.Update(id, personUpdated);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var person = await _personService.GetById(id);

        if (person is null)
        {
            return NotFound();
        }

        await _personService.Delete(id);

        return NoContent();
    }
}