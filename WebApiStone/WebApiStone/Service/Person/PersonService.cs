using WebApiStone.Entities;
using WebApiStone.Settings;
using MongoDB.Driver;
using WebApiStone.Models;
using WebApiStone.Hubs;

namespace WebApiStone.Services;

public class PersonService: IPersonService
{
    private readonly IMongoCollection<Person> _personCollection;
    private readonly IStatisticsHub _statisticsHub;

    public PersonService(
        IDatabaseConnection databaseConnection,
        IStatisticsHub statisticsHub
        )
    {
        _personCollection = databaseConnection.GetPersonCollection();
        _statisticsHub = statisticsHub;
    }

    public async Task<ResultPerson> GetAll(int page, int perpage, string? name = null)
    {
        var totalDocuments = await _personCollection.CountDocumentsAsync(Builders<Person>.Filter.Empty);
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

        return GeneratePersonResult(people, totalDocuments);
    }

    public async Task<ResultStatistics> GetStatistics(string? name, string? skincolor, string? education, string? sex)
    {
        
        var resultDatabase = await _personCollection.FindAsync(BuildFilterStatistics(name, skincolor, education, sex));
        var items = resultDatabase.ToList();
        

        return new ResultStatistics(){
            Items = items,
            Total = items.Count()
        };
    }

    private FilterDefinition<Person> BuildFilterName(string name) {
        return Builders<Person>.Filter.Where(x => x.Name.ToLower().Contains(name.ToLower()));
    }

    private FilterDefinition<Person> BuildFilterSex(string sex) {
        return Builders<Person>.Filter.Where(x => x.Sex == sex);
    }
    private FilterDefinition<Person> BuildFilterEducation(string education) {
        return Builders<Person>.Filter.Where(x => x.Education == education);
    }
    private FilterDefinition<Person> BuildFilterSkinColor(string skinColor) {
        return Builders<Person>.Filter.Where(x => x.SkinColor == skinColor);
    }

    private FilterDefinition<Person> BuildFilterStatistics(string? name, string? skincolor, string? education, string? sex) {
        FilterDefinition<Person> filterStatistics = Builders<Person>.Filter.Empty; 

        if(!String.IsNullOrEmpty(name)){
            filterStatistics &= BuildFilterName(name);
        }
        if(!String.IsNullOrEmpty(sex)){
            filterStatistics &= BuildFilterSex(sex);
        }
        if(!String.IsNullOrEmpty(skincolor)){
            filterStatistics &= BuildFilterSkinColor(skincolor);
        }
        if(!String.IsNullOrEmpty(education)){
            filterStatistics &= BuildFilterEducation(education);
        }

        return filterStatistics;
    }

    private ResultPerson GeneratePersonResult(List<Person> people, long totalDocuments)
    {
        return new ResultPerson()
        {
            Items = people,
            Total = totalDocuments,
        };
    }
        
    public async Task<Person?> GetById(string id) =>
        await _personCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    public async Task<List<Person>> GetByIdParent(string id) =>
        await _personCollection.Find(x => x.FatherID == id || x.MotherID == id).ToListAsync();

    public async Task<Person> Create(Person newPerson) {
        try{
            await _personCollection.InsertOneAsync(newPerson);
            await _statisticsHub.NotifyAll();
            return newPerson;
        }catch(Exception e){
            throw e;
        }
    }

    public async Task Update(string id, Person updatedBook) =>
        await _personCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task Delete(string id){
        try{
            await _personCollection.DeleteOneAsync(x => x.Id == id);

            await _personCollection.UpdateManyAsync(
                x => x.FatherID == id, 
                Builders<Person>.Update.Set(p => p.FatherID, null)
            );
            await _personCollection.UpdateManyAsync(
                x => x.MotherID == id, 
                Builders<Person>.Update.Set(p => p.MotherID, null)
            );

            await _statisticsHub.NotifyAll();
        }catch(Exception e){
            throw e;
        }
        
    }
        
}