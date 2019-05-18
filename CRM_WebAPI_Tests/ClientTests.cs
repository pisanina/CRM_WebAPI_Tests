using CRM_WebAPI_Tests.DTO;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CRM_WebAPI_Tests
{
    public class ClientTests : TestSetup
    {
        

        [Test]
        public async Task AddClient()
        {

            var client = new IndividualClientDTO
            {
                Name       = "Anna Kowalska2",
                EMail      = "anka@a.com",
                Street     = "Mazowiecka 12a",
                City       = "Kozienice",
                PostalCode = "09-678",
                //TypeId = 1
            };

            var json = JsonConvert.SerializeObject(client);


            var handleRequest =  await httpClient.PostAsync("http://localhost:50555/api/IndividualClient",
                new StringContent(json, Encoding.UTF8, "application/json"));

            Assert.True(handleRequest.StatusCode == System.Net.HttpStatusCode.Created);

            var response = await httpClient.GetAsync("http://localhost:50555/api/IndividualClient/5");
            var jsonContent = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<IndividualClientDTO>(jsonContent);

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.AreEqual(5, result.ID);
            Assert.AreEqual("Anna Kowalska2", result.Name);
            Assert.AreEqual("anka@a.com", result.EMail);
            Assert.AreEqual("Mazowiecka 12a", result.Street);
            Assert.AreEqual("Kozienice", result.City);
            Assert.AreEqual("09-678", result.PostalCode);
           // Assert.AreEqual(1, result.TypeId);
        }

        [Test]
        public async Task DeleteClient()
        {
            int idToDelete = 4;
            var response = await httpClient.DeleteAsync("http://localhost:50555/api/IndividualClient/"+idToDelete);

         
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);

            var list = await httpClient.GetAsync("http://localhost:50555/api/IndividualClient");
            var jsonContent = list.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<ClientType>>(jsonContent);

            Assert.True(result.Count == 3);
            Assert.False(result.Any(a => a.ID == 4));
        }

        [Test]
        public async Task ClientDetails()
        {
            int id = 4;
            var response = await httpClient.GetAsync("http://localhost:50555/api/IndividualClient/"+id);
            var jsonContent = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<IndividualClientDTO>(jsonContent);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.AreEqual(4, result.ID);
            Assert.AreEqual("Jolanta Kowalska", result.Name);
            Assert.AreEqual("jk@a.com", result.EMail);
            Assert.AreEqual("Dluga 3", result.Street);
            Assert.AreEqual("Krakow", result.City);
            Assert.AreEqual("11169", result.PostalCode);
           // Assert.AreEqual(3, result.TypeId);
        }

    

        [Test]
        public async Task UpdateClient()
        {
            int id = 4;
            var response = await httpClient.GetAsync("http://localhost:50555/api/IndividualClient/"+id);
            var jsonContent = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<IndividualClientDTO>(jsonContent);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.AreEqual(4, result.ID);
            Assert.AreEqual("Jolanta Kowalska", result.Name);
            Assert.AreEqual("jk@a.com", result.EMail);
            Assert.AreEqual("Dluga 3", result.Street);
            Assert.AreEqual("Krakow", result.City);
            Assert.AreEqual("11169", result.PostalCode);

            var updatedClient = new IndividualClientDTO
            {
                ID = 4,
                Name       = "Changed",
                EMail      = "t2@a.com",
                Street     = "Zielonkawa 2",
                City       = "Kozienice2",
                PostalCode = "22-222"
            };

            var json = JsonConvert.SerializeObject(updatedClient);

            var change = await httpClient.PutAsync("http://localhost:50555/api/IndividualClient",
                 new StringContent(json, Encoding.UTF8, "application/json"));
            Assert.True(change.StatusCode == System.Net.HttpStatusCode.OK);


            var updated = await httpClient.GetAsync("http://localhost:50555/api/IndividualClient/"+id);
            var jsonContentUpdated = updated.Content.ReadAsStringAsync().Result;
            var changedClient = JsonConvert.DeserializeObject<IndividualClientDTO>(jsonContentUpdated);

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);

            Assert.AreEqual(4, changedClient.ID);
            Assert.AreEqual("Changed", changedClient.Name);
            Assert.AreEqual("t2@a.com", changedClient.EMail);
            Assert.AreEqual("Zielonkawa 2", changedClient.Street);
            Assert.AreEqual("Kozienice2", changedClient.City);
            Assert.AreEqual("22-222", changedClient.PostalCode);

        }

        [Test]
        public async Task ClientsList()
        {
           
            var response = await httpClient.GetAsync("http://localhost:50555/api/IndividualClient");
            var jsonContent = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<ClientType>>(jsonContent);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.True(result.Count == 4);
            Assert.AreEqual("Tomasz Wozniak", result[3].Name);
            Assert.AreEqual("Katarzyna Droga", result[2].Name);
            Assert.AreEqual("Anna Zawada", result[0].Name);
            Assert.AreEqual("Jolanta Kowalska", result[1].Name);
        }

        [Test]
        public async Task ClientTypes()
        {

            var response = await httpClient.GetAsync("http://localhost:50555/api/IndividualClient/Type");
            var jsonContent = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<ClientType>>(jsonContent);

           
            Assert.True(result.Count == 3);
            Assert.AreEqual(1, result[0].ID);
            Assert.AreEqual(2, result[1].ID);
            Assert.AreEqual(3, result[2].ID);

            Assert.AreEqual("Potential", result[0].Name);
            Assert.AreEqual("Loyal", result[1].Name);
            Assert.AreEqual("Former", result[2].Name);
        }
    }
}
