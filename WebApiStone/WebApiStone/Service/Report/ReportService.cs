using WebApiStone.Entities;
using WebApiStone.Models;

namespace WebApiStone.Services;

public class ReportService: IReportService
{
    private readonly IPersonService _personService;

    public ReportService(
        IPersonService personService
    )
    {
        _personService = personService;
    }


    public async Task<ResultStatistics> GetStatistics(string? name, string? skincolor, string? education, string? sex)
    {
        ResultStatistics result = await _personService.GetStatistics(name, skincolor, education, sex);
        return result;
    }
    public async Task<FamilyTree> GetFamilyTree(string id, int level)
    {
        List<FamilyTreePeople> familyTreePeople = await GetFamilyTreePeople(id, level);
        List<FamilyTreeRelations> familyTreeRelations = GetFamilyTreeRelations(familyTreePeople);

        return new FamilyTree()
        {
            People = familyTreePeople,
            Relations = familyTreeRelations
        };
    }

    private async Task<List<FamilyTreePeople>> GetFamilyTreePeople(string id, int level)
    {
        List<FamilyTreePeople> familyPeople = new List<FamilyTreePeople>();
        Person? person = await _personService.GetById(id);
        if(person != null)
        {
            FamilyTreePeople personTree = TransformPersonToFamilyTreePeople(person);

            
            await getAscendantsRecursive(person, familyPeople, level, 0);
            await getDescendantsRecursive(person, familyPeople, level, 0);

            if (familyPeople.Count > 0 && !familyPeople.Exists((e) => e.Id == personTree.Id))
            {
                familyPeople.Insert(0, personTree);
            }
        }

        return familyPeople;

    }

    private async Task getAscendantsRecursive(Person person, List<FamilyTreePeople> list, int levelLimit, int actualLevel)
    {
        bool alreadyIncremented = false;

        if(actualLevel < levelLimit){
            if (!String.IsNullOrEmpty(person.FatherID) && !list.Exists((e) => e.Id == person.FatherID))
            {
                Tuple<Person, FamilyTreePeople> retornoTupla = await GetPersonTree(person.FatherID);
                list.Add(retornoTupla.Item2);
                actualLevel++;
                alreadyIncremented = true;
                
               await getAscendantsRecursive(retornoTupla.Item1, list, levelLimit, actualLevel);
            }

            if(!String.IsNullOrEmpty(person.MotherID) && !list.Exists((e) => e.Id == person.MotherID))
            {
                Tuple<Person, FamilyTreePeople> retornoTupla = await GetPersonTree(person.MotherID);
                list.Add(retornoTupla.Item2);
                if(!alreadyIncremented){
                    actualLevel++;
                }
                
                await getAscendantsRecursive(retornoTupla.Item1, list, levelLimit, actualLevel);
            }    
        }
    }

    private async Task getDescendantsRecursive(Person? person, List<FamilyTreePeople> list, int levelLimit, int actualLevel)
    {
        if(actualLevel < levelLimit && person != null && !String.IsNullOrEmpty(person.Id)){
            Tuple<List<Person>, List<FamilyTreePeople>> retorno = await GetDescendantsPersonTree(person.Id);

            foreach (var item in retorno.Item2)
            {
                list.Add(item);
            }

            actualLevel++;

            foreach (var item in retorno.Item1)
            {
                await getAscendantsRecursive(item, list, levelLimit, actualLevel);
                await getDescendantsRecursive(item, list, levelLimit, actualLevel);
            }
        }
    }

 
    private async Task<Tuple<Person,FamilyTreePeople>> GetPersonTree(string id)
    {
        var result = new Tuple<Person, FamilyTreePeople>(new Person(), new FamilyTreePeople());
        Person? person = await _personService.GetById(id);
        if(person != null)
        {
            FamilyTreePeople tree = TransformPersonToFamilyTreePeople(person);
            result = Tuple.Create(person, tree);
        }

        return result;

    }

     private async Task<Tuple<List<Person>, List<FamilyTreePeople>>> GetDescendantsPersonTree(string id)
    {
        List<Person> people = await _personService.GetByIdParent(id);
        List<FamilyTreePeople> tree = (from person in people select TransformPersonToFamilyTreePeople(person)).ToList();
        return new Tuple<List<Person>, List<FamilyTreePeople>>(people,tree);
    }

    private FamilyTreePeople TransformPersonToFamilyTreePeople(Person person){
        if(person != null && !String.IsNullOrEmpty(person.Id))
        {
            return new FamilyTreePeople()
            {
                Id = person.Id,
                Name = person.Name,
                FatherId = person.FatherID,
                MotherId = person.MotherID
            };
        }

        return new FamilyTreePeople();
    }

    private List<FamilyTreeRelations> GetFamilyTreeRelations(List<FamilyTreePeople> people)
    {
        List<FamilyTreeRelations> familyRelations = new List<FamilyTreeRelations>();

        foreach (var person in people)
        {
            if (!String.IsNullOrEmpty(person.MotherId) && people.Find((element) => element.Id == person.MotherId) != null)
            {
                familyRelations.Add(new FamilyTreeRelations()
                {
                    ChildId = person.Id,
                    ParentId = person.MotherId
                });
            }

            if (!String.IsNullOrEmpty(person.FatherId) && people.Find((element) => element.Id == person.FatherId) != null)
            {
                familyRelations.Add(new FamilyTreeRelations()
                {
                    ChildId = person.Id,
                    ParentId = person.FatherId
                });
            }
        }

        return familyRelations;
    }
}