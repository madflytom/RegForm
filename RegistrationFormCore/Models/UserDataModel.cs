using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace RegistrationFormCore.Models
{
    public class UserDataModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public DateTime Birthday { get; set; }
        public string DLPhotoPath { get; set; }
        public string AdditionalInfo { get; set; }
        public IFormFile DLPhoto { get; set; }
        public byte[] DLPhotoBytes { get; set; }

        public int SaveDetails()
        {
            SqlConnection con = new SqlConnection(GetConString.ConString());
            string query = "INSERT INTO Registrations(FirstName, LastName, EmailAddress, Address1, Address2, City, State, ZipCode, Birthday, DLPhoto, AdditionalInfo) VALUES (@FirstName, @LastName, @EmailAddress, @Address1, @Address2, @City, @State, @ZipCode, @Birthday, @DLPhoto, @AdditionalInfo)";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@EmailAddress", EmailAddress);
            cmd.Parameters.AddWithValue("@Address1", Address1);
            cmd.Parameters.AddWithValue("@City", City);
            cmd.Parameters.AddWithValue("@State", State);
            cmd.Parameters.AddWithValue("@ZipCode", ZipCode);
            cmd.Parameters.AddWithValue("@Birthday", Birthday);
            cmd.Parameters.AddWithValue("@DLPhoto", DLPhotoBytes);
            cmd.Parameters.AddWithValue("@AdditionalInfo", AdditionalInfo);

            //TODO: Can any other fields be null?  Make sure form prevents it.

            if (String.IsNullOrEmpty(Address2))
            {
                cmd.Parameters.AddWithValue("@Address2", DBNull.Value);
            }

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }

        public List<UserDataModel> GetAll()
        {
            var registrants = new List<UserDataModel>();

            var imageString = "";

            using (SqlConnection con = new SqlConnection(GetConString.ConString()))
            {
                List<UserDataModel> registrant = new List<UserDataModel>();
                SqlCommand cmd = new SqlCommand("select * from Registrations", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    UserDataModel udm = new UserDataModel();
                    udm.FirstName = rdr["FirstName"].ToString();
                    udm.LastName = rdr["LastName"].ToString();
                    udm.EmailAddress = rdr["EmailAddress"].ToString();
                    udm.Address1 = rdr["Address1"].ToString();
                    udm.Address2 = rdr["Address2"].ToString();
                    udm.City = rdr["City"].ToString();
                    udm.State = rdr["State"].ToString();
                    udm.ZipCode = rdr["ZipCode"].ToString();
                    udm.Birthday = Convert.ToDateTime(rdr["Birthday"].ToString());
                    udm.AdditionalInfo = rdr["AdditionalInfo"].ToString();

                    imageString = rdr["DLPhoto"].ToString();


                    registrants.Add(udm);
                }
            }
            return registrants;

        }
    }
}