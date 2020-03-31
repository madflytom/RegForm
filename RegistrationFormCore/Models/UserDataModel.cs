using System.Data.SqlClient;

namespace RegistrationFormCore.Models
{
    public class UserDataModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }

        public int SaveDetails()
        {
            SqlConnection con = new SqlConnection(GetConString.ConString());
            string query = "INSERT INTO Profile(Name, Age, City) values ('" + Name + "','" + Age + "','" + City + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
    }
}