using WebApiStone.Entities;

namespace WebApiStone.Services
{
    public interface IReportService
    {
        public Task<FamilyTree> GetFamilyTree(string id, int level);
    }
}
