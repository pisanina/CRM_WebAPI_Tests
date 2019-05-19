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
    class SellerTests : TestSetup
    {

        [Test]
        public async Task SellersList()
        {

            var response = await httpClient.GetAsync("http://localhost:50555/api/Seller");
            var jsonContent = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<SellerDTO>>(jsonContent);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.True(result.Count == 2);
         
            Assert.AreEqual("John Snow", result[0].Name);
            Assert.AreEqual("Tom Hardy", result[1].Name);
        }

        [Test]
        public async Task AddSeller()
        {

            var seller = new SellerDTO
            {
                Name       = "Micky Mouse"
            };

            var json = JsonConvert.SerializeObject(seller);

            var handleRequest =  await httpClient.PostAsync("http://localhost:50555/api/Seller",
                new StringContent(json, Encoding.UTF8, "application/json"));

            Assert.True(handleRequest.StatusCode == System.Net.HttpStatusCode.Created);

            var response = await httpClient.GetAsync("http://localhost:50555/api/Seller");
            var jsonContent = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<SellerDTO>>(jsonContent);

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("Micky Mouse", result[1].Name);
           
        }
    }
}
