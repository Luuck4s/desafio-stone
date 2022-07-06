using WebApiStone.Entities;

namespace WebApiStone.Models
{
    public class ResultStatistics
    {
        public long Total { get; set; }
        public List<Person> Items { get; set; } = new List<Person>();
    }
}
