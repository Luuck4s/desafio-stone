using MongoDB.Driver;
using Moq;
using WebApiStone.Entities;
using WebApiStone.Entities.Values;
using WebApiStone.Services;
using WebApiStone.Settings;
using Xunit;

namespace WebApiStone.Tests
{
    public class PersonTests
    {

        private Person PERSON_VALID = new(string.Empty, "Lucas", "Lima", SexValues.MALE, SkinColorValues.CAUCASIAN, EducationValues.HIGH_SCHOOL);

        private Mock<IDatabaseConnection> mockDatabaseConnection;
        private PersonService personService;

        public PersonTests()
        {
            var collection = new Mock<IMongoCollection<Person>>();
            var asyncCursor = new Mock<IAsyncCursor<Person>>();
            
            mockDatabaseConnection = new Mock<IDatabaseConnection>();
            mockDatabaseConnection.Setup(x => x.GetPersonCollection()).Returns(collection.Object);

            personService = new PersonService(mockDatabaseConnection.Object);
        }

        [Fact]
        public async void Test_Create_Valid_Person()
        {
           
            var result = await personService.Create(PERSON_VALID);

            Assert.Equal(PERSON_VALID, result);
            Assert.NotNull(result);
        }

        [Fact]
        public async void Test_GetAll_Person()
        {
            var result = await personService.GetAll(page: 0, perpage: 10);

            Assert.NotNull(result);
        }
    }
}
