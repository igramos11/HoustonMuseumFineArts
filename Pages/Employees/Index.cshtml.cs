using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using System.Data.SqlClient;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace WebApplication2.Pages.Employees
{
    public class IndexModel : PageModel
    {
        public List<EmployeeInfo> listEmployees = new List<EmployeeInfo>(); //to store data of all employees, list of employee info
        public void OnGet() //gets list
        { 
            try
            {
                String connectionString = "Data Source=houstonmuseumfinearts.database.windows.net; Initial Catalog = HoustonMuseumFineArts; Persist Security Info = True; User ID = igramos; Password =co$C3380group10 ";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM employees"; //query that reads all rows/data from employee table
                    using (SqlCommand command = new SqlCommand(sql, connection)) //command that allows us to execute sql query
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read()) //while loop reads data from table
                            {
                                EmployeeInfo employeeInfo = new EmployeeInfo();
                                employeeInfo.id = "" + reader.GetInt32(0); //id is type string here and int in database, this will convert int to string
                                employeeInfo.name = reader.GetString(1);
                                employeeInfo.social =reader.GetString(2);
                                employeeInfo.email = reader.GetString(3);
                                employeeInfo.phone = reader.GetString(4);
                                employeeInfo.address = reader.GetString(5);
                                employeeInfo.created_at = reader.GetDateTime(6).ToString();

                                listEmployees.Add(employeeInfo);  //Adds object into list
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString()); //catches error in case of exception
            }
        }
    }
    public class EmployeeInfo //class allows us to store data of one employee
    {
        public String id;
        public String name;
        public String social;
        public String email;
        public String phone;
        public String address;
        public String created_at;
    }
}