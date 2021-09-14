using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Net;


namespace RestSharpTestCase
{
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string Salary { get; set; }
    }

    [TestClass]
    public class RestSharpTest
    {
        RestClient client;

        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:4000");
        }
        private IRestResponse getEmployeeList()
        {
            RestRequest request = new RestRequest("/employees", Method.GET);

            //act

            IRestResponse response = client.Execute(request);
            return response;
        }
        //UC 1
        [TestMethod]
        public void onCallingGETApi_ReturnEmployeeList()
        {
            IRestResponse response = getEmployeeList();

            //assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(14, dataResponse.Count);
            foreach (var item in dataResponse)
            {
                System.Console.WriteLine("id: " + item.id + "Name: " + item.name + "Salary: " + item.Salary);
            }
        }
        //UC 2
        [TestMethod]
        public void givenEmployee_OnPost_ShouldReturnAddedEmployee()
        {
            RestRequest request = new RestRequest("/employees", Method.POST);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Clark");
            jObjectbody.Add("Salary", "15000");
            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            //act
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Clark", dataResponse.name);
            Assert.AreEqual(15000, dataResponse.Salary);
        }
        //UC 3
        [TestMethod]
        public void givenEmployeeSalary_OnUpdate_ShouldReturnAddedEmployee()
        {
            //Arrange
            RestRequest request = new RestRequest("/employees/13", Method.PUT);
            JObject jObjectbody = new JObject();

            jObjectbody.Add("name", "Clark");
            jObjectbody.Add("Salary", "20000");
            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            //Act
            var response = client.Execute(request);

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            int salary = int.Parse(dataResponse.Salary);
            Assert.AreEqual("Clark", dataResponse.name);
            Assert.AreEqual(20000, salary);

        }
    }
}
