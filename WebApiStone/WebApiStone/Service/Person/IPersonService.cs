using WebApiStone.Entities;
using WebApiStone.Models;

namespace WebApiStone.Services
{
    public interface IPersonService
    {
        public Task<ResultPerson> GetAll(int page, int perpage,string? name);
        public Task<Person?> GetById(string id);
        public Task<List<Person>> GetByIdParent(string id);
        public Task<Person> Create(Person person);
        public Task Update(string id,Person person);
        public Task Delete(string id);
    }
}
