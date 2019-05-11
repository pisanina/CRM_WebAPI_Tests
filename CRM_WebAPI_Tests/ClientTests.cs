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
                Name       = "Anna Kowalska",
                EMail      = "anka@a.com",
                Street     = "Mazowiecka 12a",
                City       = "Kozienice",
                PostalCode = "09-678"
            };

            var json = JsonConvert.SerializeObject(client);

            var handleRequest =  await httpClient.PostAsync("http://localhost:50555/api/IndividualClient",
                new StringContent(json, Encoding.UTF8, "application/json"));

            Assert.True(handleRequest.StatusCode == System.Net.HttpStatusCode.Created);
        }

        [Test]
        public async Task DeleteClient()
        {
            int idToDelete = 4;
            var response = await httpClient.DeleteAsync("http://localhost:50555/api/IndividualClient/"+idToDelete);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);

        }
    }
}
