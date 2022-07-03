using MongoDB.Driver;
using WebApiStone.Entities;

namespace WebApiStone.Settings
{
    public interface IDatabaseConnection
    {
        IMongoCollection<Person> GetPersonCollection();
    }
}
