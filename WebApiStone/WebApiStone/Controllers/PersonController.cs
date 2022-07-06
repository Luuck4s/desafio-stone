using WebApiStone.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApiStone.Services;
using WebApiStone.Models;

namespace WebApiStone.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService) =>
        _personService = personService;

    /// <summary>
    /// Retorna todos os usuários cadastrados
    /// </summary>
    /// <returns>Usuários cadastrados</returns>
    [HttpGet("GetAll")]
    public async Task<ResultPerson> GetAll(int page, string? name, int perpage = 10)
    {
        return await _personService.GetAll(page, perpage, name);
    }
     
    /// <summary>
    /// Retorna um usuário buscado através do ID
    /// </summary>
    /// <returns>Usuário através do id passado</returns>
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

    /// <summary>
    /// Realiza a criação de um novo registro
    /// </summary>
    /// <returns>Usuário criado</returns>
    [HttpPost]
    public async Task<ActionResult<Person>> Post(Person person)
    {
        return await _personService.Create(person);
    }

    /// <summary>
    /// Realiza a atualização de um registro
    /// </summary>
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

    /// <summary>
    /// Deleta de um registro através do ID
    /// </summary>
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