using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Net.Security;
using System.Data.SqlClient;


namespace WebApplication2.Pages.Employees
{
    public class CreateModel : PageModel
    {
        public EmployeeInfo employeeInfo = new EmployeeInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            employeeInfo.name = Request.Form["name"];
            employeeInfo.social = Request.Form["social"];
            employeeInfo.email = Request.Form["email"];
            employeeInfo.phone = Request.Form["phone"];
            employeeInfo.address = Request.Form["address"];

            if (employeeInfo.name.Length ==0 || employeeInfo.social.Length ==0 || employeeInfo.email.Length==0 || employeeInfo.phone.Length == 0 || employeeInfo.address.Length ==0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //save the new client into the database
            try //connect to database
            {
                String connectionString = "Data Source=houstonmuseumfinearts.database.windows.net;Initial Catalog=HoustonMuseumFineArts;Persist Security Info=True;User ID=igramos;Password=co$C3380group10"; 
                using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO employees " +
                                  "(name, social, email, phone, address) VALUES " +
                                  "(@name, @social, @email, @phone, @address);";

                    using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", employeeInfo.name);
                        command.Parameters.AddWithValue("@social", employeeInfo.social);
                        command.Parameters.AddWithValue("@email", employeeInfo.social);
                        command.Parameters.AddWithValue("@phone", employeeInfo.phone);
                        command.Parameters.AddWithValue("@address", employeeInfo.address);

                        command.ExecuteNonQuery(); //executes sqlquery
                    }
                }
            }
            catch (Exception ex) //we have an error
            {
                errorMessage = ex.Message;
                return;
            }

            employeeInfo.name = ""; employeeInfo.social = ""; employeeInfo.email = ""; employeeInfo.phone = ""; employeeInfo.address = "";
            successMessage = "New Employee Added Correctly";

            Response.Redirect("/Employees/Create");

        }
    }
}
