using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ContactManager
{
    class Database
    {
        public Database()
        {
            
        }

        public List<Contact> getContactList()
        {
            string sqltext = "select * from contacts";
            var con = new SqlConnection("data source =.; database = contactmanager; integrated security = SSPI");
            SqlCommand cmd = new SqlCommand(sqltext, con);

            List<Contact> contactList = new List<Contact>();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                Contact contact = new Contact((int)sdr["Id"], (string)sdr["first_name"], (string)sdr["last_name"], (string)sdr["phone_number"],
                    (string)sdr["email"]);
                
                    contactList.Add(contact);
            }
            con.Close();

            return contactList;
        }

        public void addContact(Contact cont)
        {
            string sqltext = "insert into contacts (first_name, last_name, phone_number, email) values(@fName, @lName, @pNumber, @eMail)";
            var con = new SqlConnection("data source =.; database = contactmanager; integrated security = SSPI");
            SqlCommand cmd = new SqlCommand(sqltext, con);
            cmd.Parameters.AddWithValue("@fName", cont.firstName);
            cmd.Parameters.AddWithValue("@lName", cont.lastName);
            cmd.Parameters.AddWithValue("@pNumber", cont.phoneNumber);
            cmd.Parameters.AddWithValue("@eMail", cont.email);
            try {
                con.Open();
                var rowsAffected = cmd.ExecuteNonQuery();

                string query = "Select @@Identity as newId from contacts";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                var newId = Convert.ToInt32(cmd.ExecuteScalar());

                Console.WriteLine("Records Inserted Successfully");
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally {
                con.Close();
            }
        }

        public void deleteContact(int ident)
        {
            string sqltext = "delete from contacts where id = @id";
            var con = new SqlConnection("data source =.; database = contactmanager; integrated security = SSPI");
            SqlCommand cmd = new SqlCommand(sqltext, con);
            cmd.Parameters.AddWithValue("@id", ident);

            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();
        }
    }
}
