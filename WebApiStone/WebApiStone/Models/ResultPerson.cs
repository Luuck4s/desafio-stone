using WebApiStone.Entities;

namespace WebApiStone.Models
{
    public class ResultPerson
    {
        public int Total { get; set; }
        public List<Person> Items { get; set; } = new List<Person>();
    }
}
