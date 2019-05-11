using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CRM_WebAPI_Tests
{
        public abstract class TestSetup
        {
            protected HttpClient httpClient;

            [SetUp]
            public void PrepareData()
            {
                httpClient = new HttpClient();
                
            }
        }
}
