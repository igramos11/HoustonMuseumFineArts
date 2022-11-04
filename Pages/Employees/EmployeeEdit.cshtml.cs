using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using SqlConnection = Microsoft.Data.SqlClient.SqlConnection;
using SqlCommand = Microsoft.Data.SqlClient.SqlCommand;
using SqlDataReader = Microsoft.Data.SqlClient.SqlDataReader;
using static Humanizer.In;

namespace WebApplication2.Pages.Employees
{
    public class EmployeeEditModel : PageModel
    {
        public EmployeeInfo employeeInfo = new EmployeeInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet() //allows us to see data of current employee
        {
            String id = Request.Query["id"]; //reads id fills object that fills id.

            try
            {
                String connectionString = "Data Source = houstonmuseumfinearts.database.windows.net; Initial Catalog = HoustonMuseumFineArts; Persist Security Info = True; User ID = igramos; Password = co$C3380group10";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM employees WHERE id=@id"; //id we have received from request
                    using (SqlCommand command = new SqlCommand(sql, connection)) //allows us to execute sql query
                    {
                        command.Parameters.AddWithValue("@id", id); //replaces parameters with id that we received from request
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                employeeInfo.id = "" + reader.GetInt32(0);
                                employeeInfo.name = reader.GetString(1);
                                employeeInfo.social = reader.GetString(2);
                                employeeInfo.email = reader.GetString(3);
                                employeeInfo.phone = reader.GetString(4);
                                employeeInfo.address = reader.GetString(5);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
            public void OnPost() //allows us to update data of employee
            {
                employeeInfo.id = Request.Form["id"];
                employeeInfo.name = Request.Form["name"];
                employeeInfo.social = Request.Form["social"];
                employeeInfo.email = Request.Form["email"];
                employeeInfo.phone = Request.Form["phone"];
                employeeInfo.address = Request.Form["address"];

            if (employeeInfo.id.Length == 0 || employeeInfo.name.Length == 0 || employeeInfo.social.Length == 0 || employeeInfo.email.Length == 0
                || employeeInfo.phone.Length == 0 || employeeInfo.address.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }
            try //updates database and updates data of employee
            {
                String connectionString = "Data Source = houstonmuseumfinearts.database.windows.net; Initial Catalog = HoustonMuseumFineArts; Persist Security Info = True; User ID = igramos; Password = co$C3380group10";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE employees " +
                                 "SET name=@name, social=@social, email=@email, phone=@phone, address=@address " +
                                   "WHERE id=@id";



                    using (SqlCommand command = new SqlCommand(sql, connection)) //replaces data
                    {
                        command.Parameters.AddWithValue("@name", employeeInfo.name);
                        command.Parameters.AddWithValue("@social", employeeInfo.social);
                        command.Parameters.AddWithValue("@email", employeeInfo.email);
                        command.Parameters.AddWithValue("@phone", employeeInfo.phone);
                        command.Parameters.AddWithValue("@address", employeeInfo.address);
                        command.Parameters.AddWithValue("@id", employeeInfo.id);

                        command.ExecuteNonQuery(); //executes sql query
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Employees/Index");
            }
        }
}

