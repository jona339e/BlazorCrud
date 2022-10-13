using System.Data.SqlClient;

namespace BlazorCrud.Data
{
    public class CrudData
    {
        private int weaponRank;
        private string? weaponName;
        private string? dropLocation;
        private int ilvl;



        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=UHDKDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


        public CrudData(string? weaponRank, string? weaponName, string? dropLocation, string? ilvl)
        {
            this.weaponRank = Convert.ToInt32(weaponRank);
            this.weaponName = weaponName;
            this.dropLocation = dropLocation;
            this.ilvl = Convert.ToInt32(ilvl);
        }

        public string CreateUser()
        {

            conn.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM WeaponsP1", conn);


            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (weaponRank == reader.GetInt32(0) || weaponName == reader.GetString(1) || dropLocation == reader.GetString(2) || ilvl == reader.GetInt32(3))
                    {
                        return "";
                    }
                }
            }

            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                adapter.InsertCommand = new SqlCommand("INSERT INTO WeaponsP1 VALUES (@weaponRank,@weaponName,@dropLocation,@ilvl)", conn);
                adapter.InsertCommand.Parameters.AddWithValue("@weaponRank", weaponRank);
                adapter.InsertCommand.Parameters.AddWithValue("@weaponName", weaponName);
                adapter.InsertCommand.Parameters.AddWithValue("@dropLocation", dropLocation);
                adapter.InsertCommand.Parameters.AddWithValue("@ilvl", ilvl);
                adapter.InsertCommand.ExecuteNonQuery();

            }

            return "objects added to db";
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
                    content f = new()
                    {
                        weaponRank = reader.GetInt32(0),
                        weaponName = reader.GetString(1),
                        dropLocation = reader.GetString(2),
                        ilvl = reader.GetInt32(3)
                    };
                    contentList.Add(f);
                }
            }
            conn.Close();
            return contentList;


        }
    }
}
