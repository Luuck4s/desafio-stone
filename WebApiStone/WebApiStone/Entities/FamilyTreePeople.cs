namespace WebApiStone.Entities
{
    public class FamilyTreePeople
    {
        public string Id { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty; 
        public string? FatherId { get; set; } = String.Empty;
        public string? MotherId { get; set; } = String.Empty;
    }
}
