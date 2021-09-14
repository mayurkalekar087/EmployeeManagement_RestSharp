using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagement.Model.SalaryModel;
using EmployeeManagement;
 
namespace EmployeeManagementTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GivenSalaryDetail()
        {
            //Arrange
            Salary salary = new Salary();
            SalaryUpdateModel UpdateModel = new SalaryUpdateModel()
            {
                SalaryId = 1,
                Month = "jan",
                EmployeeSalary =0,
                EmployeeId = 5

            };
            //Act
            int EmpSalary = salary.UpdateEmployeeSalary(UpdateModel);

            //Assert
            Assert.AreEqual(UpdateModel.EmployeeSalary, EmpSalary);

        }
    }
}
