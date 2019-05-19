using Dapper;
using NUnit.Framework;
using Oracle.ManagedDataAccess.Client;
using System.Net.Http;

namespace CRM_WebAPI_Tests
{
    public abstract class TestSetup
    {
        protected HttpClient httpClient;
        private string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl.home)));User Id=System;Password=oracle;";


        [SetUp]
        public void PrepareData()
        {
            httpClient = new HttpClient();





             using (OracleConnection SQLConnect =
                new OracleConnection(connectionString))
            {
      
                SQLConnect.Execute("ALTER TABLE System.IC_Message MODIFY(ID Generated as Identity (START WITH 1))");
                SQLConnect.Execute("Delete From System.IC_Message");

                SQLConnect.Execute("ALTER TABLE System.IC_IndividualClient MODIFY(ID Generated as Identity (START WITH 1))");
                SQLConnect.Execute("Delete From System.IC_IndividualClient");
                SQLConnect.Execute("Insert into System.IC_IndividualClient(NAME, EMail, Street, City, PostalCode, TypeId) values('Tomasz Wozniak', 'tom@a.com', 'Malinowa 3', 'Radom', '09169', 1)");
                SQLConnect.Execute("Insert into System.IC_IndividualClient(NAME, EMail, Street, City, PostalCode, TypeId) values('Katarzyna Droga', 'cate@a.com', 'Filemona 4', 'Rzeszow', '99134', 2)");
                SQLConnect.Execute("Insert into System.IC_IndividualClient(NAME, EMail, Street, City, PostalCode, TypeId) values('Anna Zawada', 'ann@a.com', 'Poziomkowa 11', 'Torun', '45169', 3)");
                SQLConnect.Execute("Insert into System.IC_IndividualClient(NAME, EMail, Street, City, PostalCode, TypeId) values('Jolanta Kowalska', 'jk@a.com', 'Dluga 3', 'Krakow', '11169', 3)");

                SQLConnect.Execute("ALTER TABLE System.IC_Seller MODIFY(ID Generated as Identity (START WITH 1))");
                SQLConnect.Execute("Delete From System.IC_Seller");
                SQLConnect.Execute("Insert into System.IC_Seller (Name) values('Tom Hardy')");
                SQLConnect.Execute("Insert into System.IC_Seller (Name) values('John Snow')");

                SQLConnect.Execute("Insert into System.IC_Message (SellerId, ClientId, Message) values(1,1,'test message')");

            }
        }
    }
}