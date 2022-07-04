using WebApiStone.Entities;
using WebApiStone.Settings;
using MongoDB.Driver;
using WebApiStone.Models;

namespace WebApiStone.Services;

public class PersonService: IPersonService
{
    private readonly IMongoCollection<Person> _personCollection;

    public PersonService(
        IDatabaseConnection databaseConnection
        )
    {
        _personCollection = databaseConnection.GetPersonCollection();
    }

    public async Task<ResultPerson> GetAll(int page, int perpage, string? name = null)
    {
        List<Person>  people = new List<Person>();
        IAsyncCursor<Person> result;

        if (!String.IsNullOrEmpty(name))
        {
            result = await _personCollection.FindAsync(BuildFilterName(name));
        }
        else
        {
            result = await _personCollection.FindAsync(_ => true);
        }


        if(result != null)
        {
            people = result.ToList().Skip(perpage * (page - 1)).Take(perpage).ToList();
        }

        return GeneratePersonResult(people);
    }

    private FilterDefinition<Person> BuildFilterName(string name) {
           return Builders<Person>.Filter.Where(x => x.Name.ToLower().Contains(name.ToLower()) || x.LastName.Contains(name.ToLower()));
    }

    private ResultPerson GeneratePersonResult(List<Person> people)
    {
        return new ResultPerson()
        {
            Items = people,
            Total = people.Count,
        };
    }
        
    public async Task<Person?> GetById(string id) =>
        await _personCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    public async Task<List<Person>> GetByIdParent(string id) =>
        await _personCollection.Find(x => x.FatherID == id || x.MotherID == id).ToListAsync();

    public async Task<Person> Create(Person newPerson) {
        await _personCollection.InsertOneAsync(newPerson);
        return newPerson;
    }

    public async Task Update(string id, Person updatedBook) =>
        await _personCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task Delete(string id) =>
        await _personCollection.DeleteOneAsync(x => x.Id == id);
}