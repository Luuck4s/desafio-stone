using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace WebApiStone.Entities;

public class Person
{
    public Person(string id, string name, string lastname, string sex, string skincolor, string education)
    {
        this.Id = id;
        this.Name = name;
        this.LastName = lastname;
        this.Sex = sex; 
        this.SkinColor = skincolor; 
        this.Education = education;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } = null!;

    [Required]
    [BsonElement("name")]
    public string Name { get; set; } = null!;

    [Required]
    [BsonElement("lastName")]
    public string LastName { get; set; } = null!;

    [Required]
    [BsonElement("sex")]
    public string Sex { get; set; } = null!;

    [Required]
    [BsonElement("skinColor")]
    public string SkinColor { get; set; } = null!;

    [Required]
    [BsonElement("education")]
    public string Education { get; set; } = null!;

    [BsonElement("fatherid")]
    public string? FatherID { get; set; } = null!;

    [BsonElement("motherid")]
    public string? MotherID { get; set; } = null!;
}