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
    class ActionTests : TestSetup
    {
        [Test]
        public async Task AddAction()
        {

            var client = new ActionDTO
            {
                SellerId = 2,
                ClientId = 2,
                Message  = "test message2"
            };

            var json = JsonConvert.SerializeObject(client);


            var handleRequest =  await httpClient.PostAsync("http://localhost:50555/api/Action",
                new StringContent(json, Encoding.UTF8, "application/json"));

            Assert.True(handleRequest.StatusCode == System.Net.HttpStatusCode.Created);

            var response = await httpClient.GetAsync("http://localhost:50555/api/Action");
            var jsonContent = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<ActionDTO>>(jsonContent);
            var newSeller = result.Single(a => a.ID==2);
            

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.AreEqual(2, newSeller.ID);
            Assert.AreEqual(2, newSeller.SellerId);
            Assert.AreEqual(2, newSeller.ClientId);
            Assert.AreEqual("test message2", newSeller.Message);
            Assert.AreEqual(DateTime.Now.Day, newSeller.MessageDate.Day);
            
            // Assert.AreEqual(1, result.TypeId);
        }

        [Test]
        public async Task ClientMessages()
        {
            int id = 1;
            var response = await httpClient.GetAsync("http://localhost:50555/api/Action/"+id);
            var jsonContent = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<ActionDTO>>(jsonContent);
            var firstmessage = result.Single(a => a.ID==1);

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);

            Assert.AreEqual(1, firstmessage.SellerId);
            Assert.AreEqual(1, firstmessage.ClientId);
            Assert.AreEqual("test message", firstmessage.Message);
            
        }
    }
}
