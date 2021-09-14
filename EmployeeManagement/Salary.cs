using System;
using System.Collections.Generic;
using System.Data;
using EmployeeManagement.SalaryModel;
using System.Data.SqlClient;
using EmployeeManagement.Model.SalaryModel;

namespace EmployeeManagement
{
    public class Salary
    {
        private static SqlConnection ConnectionSetup()
        {
            return new SqlConnection(@"Data Source=(LocalDb)\localdb;Initial Catalog=CompanyDB;Integrated Security=True");

        }
        public int UpdateEmployeeSalary(SalaryUpdateModel salaryUpdateModel)
        {
            SqlConnection SalaryConnection = ConnectionSetup();
            int salary = 0;
            try
            {
                using (SalaryConnection)
                {
                    SalaryDetailModel displayModel = new SalaryDetailModel();
                    //define the SQL command Object
                    SqlCommand command = new SqlCommand("spUpdateEmployeeSalary", SalaryConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", salaryUpdateModel.SalaryId);

                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                SalaryConnection.Close();
            }
            return salary;
        }
    }
}
