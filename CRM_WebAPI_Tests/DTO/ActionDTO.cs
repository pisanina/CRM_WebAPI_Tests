using System;

namespace CRM_WebAPI_Tests.DTO
{
    internal class ActionDTO
    {
        public int ID { get; set; }
        public int SellerId { get; set; }
        public int ClientId { get; set; }
        public string Message { get; set; }
        public DateTime MessageDate { get; set; }
    }
}