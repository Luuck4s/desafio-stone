namespace WebApiStone.Models
{
    public class FamilyTree
    {
        public List<FamilyTreePeople> People { get; set; } = new List<FamilyTreePeople>();
        public List<FamilyTreeRelations> Relations { get; set; } = new List<FamilyTreeRelations>();
    }
}
