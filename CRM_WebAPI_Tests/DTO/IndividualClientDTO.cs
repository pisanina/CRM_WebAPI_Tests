using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_WebAPI_Tests.DTO
{
    class IndividualClientDTO
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
