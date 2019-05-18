using Dapper;
using NUnit.Framework;
using Oracle.ManagedDataAccess.Client;
using System.Net.Http;

namespace CRM_WebAPI_Tests
{
    public abstract class TestSetup
    {
        protected HttpClient httpClient;

        [SetUp]
        public void PrepareData()
        {
            httpClient = new HttpClient();

            string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl.home)));User Id=System;Password=oracle;";
            using (OracleConnection SQLConnect =
                new OracleConnection(connectionString))
            {
                SQLConnect.Execute("ALTER TABLE System.IC_IndividualClient MODIFY(ID Generated as Identity (START WITH 1))");
                SQLConnect.Execute("Delete From System.IC_IndividualClient");
                SQLConnect.Execute("Insert into System.IC_IndividualClient(NAME, EMail, Street, City, PostalCode, TypeId) values('Tomasz Wozniak', 'tom@a.com', 'Malinowa 3', 'Radom', '09169', 1)");
                SQLConnect.Execute("Insert into System.IC_IndividualClient(NAME, EMail, Street, City, PostalCode, TypeId) values('Katarzyna Droga', 'cate@a.com', 'Filemona 4', 'Rzeszow', '99134', 2)");
                SQLConnect.Execute("Insert into System.IC_IndividualClient(NAME, EMail, Street, City, PostalCode, TypeId) values('Anna Zawada', 'ann@a.com', 'Poziomkowa 11', 'Torun', '45169', 3)");
                SQLConnect.Execute("Insert into System.IC_IndividualClient(NAME, EMail, Street, City, PostalCode, TypeId) values('Jolanta Kowalska', 'jk@a.com', 'Dluga 3', 'Krakow', '11169', 3)");

            }
        }
    }
}