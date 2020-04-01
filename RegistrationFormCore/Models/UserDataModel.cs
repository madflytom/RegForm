using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
        public string PhoneNumber { get; set; }

        public int SaveDetails()
        {
            SqlConnection con = new SqlConnection(GetConString.ConString());
            string query = "INSERT INTO Registrations(FirstName, LastName, EmailAddress, Address1, Address2, City, State, ZipCode, Birthday, DLPhoto, AdditionalInfo, PhoneNumber) VALUES (@FirstName, @LastName, @EmailAddress, @Address1, @Address2, @City, @State, @ZipCode, @Birthday, @DLPhoto, @AdditionalInfo, @PhoneNumber)";

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
            cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);

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

                byte[] photo;

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
                    udm.PhoneNumber = rdr["PhoneNumber"].ToString();

                    if (rdr["DLPhoto"].ToString() != "")
                    {
                        udm.DLPhotoBytes = (byte[])rdr["DLPhoto"];
                    }
                    

                    registrants.Add(udm);
                }
            }

            return registrants;

        }

        public byte[] GetImage(string emailAddress)
        {
            byte[] photo = null;
            using (SqlConnection con = new SqlConnection(GetConString.ConString()))
            {
                List<UserDataModel> registrant = new List<UserDataModel>();
                SqlCommand cmd = new SqlCommand("select DlPhoto from Registrations where EmailAddress = '" + emailAddress +"'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (rdr["DLPhoto"].ToString() != "")
                    {
                        photo = (byte[])rdr["DLPhoto"];
                    }
                }
            }
            return photo;

        }
    }
}