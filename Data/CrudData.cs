using System.Data;
using System.Data.SqlClient;

namespace BlazorCrud.Data
{
    public class CrudData
    {
        private int weaponRank;
        private string? weaponName;
        private string? dropLocation;
        private int ilvl;



        private SqlConnection conn = new SqlConnection("" + "Data Source=.;Initial Catalog=UHDKDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


        public bool CreateWeapon(content cont)
        {
                bool executed = false;
                conn.Open();
                var cmd = new SqlCommand("INSERT INTO WeaponsP1 " + "VALUES (@weaponRank, @weaponName, @dropLocation, @ilvl)", conn);
                cmd.Parameters.Add("@weaponRank", SqlDbType.Int).Value = cont.weaponRank;
                cmd.Parameters.Add("@weaponName", SqlDbType.NVarChar).Value = cont.weaponName;
                cmd.Parameters.Add("@dropLocation", SqlDbType.NVarChar).Value = cont.dropLocation;
                cmd.Parameters.Add("@ilvl", SqlDbType.Int).Value = cont.ilvl;
                if (cmd.ExecuteNonQuery() == 1) executed = true;
                conn.Close();
                return executed;
        }
        public List<content> ReadContent()
        {
            List<content> contentList = new List<content>();
            SqlCommand command = new SqlCommand("Select * from weaponsP1", conn);
            conn.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    content c = new()
                    {
                        weaponRank = reader.GetInt32(0),
                        weaponName = reader.GetString(1),
                        dropLocation = reader.GetString(2),
                        ilvl = reader.GetInt32(3)
                    };
                    contentList.Add(c);
                }
            }
            conn.Close();
            return contentList;


        }
    }
}
