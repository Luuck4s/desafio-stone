using Xunit;
using System.Net.Http;
using System.Net;
using WebApiStone.Entities;
using WebApiStone.Entities.Values;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System;

namespace WebApiStone.Tests.Integration
{
    public class ApiTests
    {
        Person VALID_PERSON = new(string.Empty, "Lucas", "Lima", SexValues.MALE, SkinColorValues.CAUCASIAN, EducationValues.HIGH_SCHOOL);
        private string URLBASE = "";
        
        public ApiTests()
        {
            URLBASE = Environment.GetEnvironmentVariable("API_URL") ?? "http://web_api_stone:5000/api";
        }

        [Fact]
        public async void Test_Create_Person()
        {
            Person person = await CreatePerson();
            await DeletePerson(person);
        }

        [Fact]
        public async void Test_Update_Person()
        {
            Person person = await CreatePerson();
            await UpdatePerson(person);
            await DeletePerson(person);
        }

        [Fact]
        public async void Test_Delete_Person()
        {
            Person person = await CreatePerson();
            await DeletePerson(person);
        }

        [Fact]
        public async void Test_GetAll_Person()
        {
            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"{URLBASE}/Person/GetAll");
            
                var stringResult = await response.Content.ReadAsStringAsync();
                var statusCode = response.StatusCode;


                Assert.Equal(HttpStatusCode.OK,statusCode);
                Assert.NotNull(stringResult);
            }   
        }

        private async Task<Person> CreatePerson()
        {
            Person personResult = new Person();
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(VALID_PERSON);
                var response = await client.PostAsync($"{URLBASE}/Person", new StringContent(JsonConvert.SerializeObject(VALID_PERSON), Encoding.UTF8, "application/json"));
                var statusCode = response.StatusCode;
                var stringResult = await response.Content.ReadAsStringAsync();

                Person? person = JsonConvert.DeserializeObject<Person>(stringResult);
                if (person != null)
                {
                    personResult = person;
                }

                Assert.Equal(HttpStatusCode.OK, statusCode);
                Assert.NotNull(stringResult);

            }

            return personResult;
        }

        private async Task UpdatePerson(Person person)
        {
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(person);
                var response = await client.PutAsync($"{URLBASE}/Person/{person.Id}", new StringContent(JsonConvert.SerializeObject(VALID_PERSON), Encoding.UTF8, "application/json"));
                var stringResult = await response.Content.ReadAsStringAsync();
                var statusCode = response.StatusCode;


                Assert.Equal(HttpStatusCode.NoContent, statusCode);
            }
        }

        private async Task DeletePerson(Person person)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{URLBASE}/Person/{person.Id}");
                var stringResult = await response.Content.ReadAsStringAsync();
                var statusCode = response.StatusCode;


                Assert.Equal(HttpStatusCode.NoContent, statusCode);
            }
        }
    }
}
