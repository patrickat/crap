using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRAP.Models;
using MySql.Data.MySqlClient;

namespace CRAP.Helper
{
    public class MySqlHelper
    {
        private const string connectionString = "server=localhost;database=tagDB;uid=root;pwd=password;";

        public static void AddTag(Tag tag)
        {
            using (var db = new MySqlConnection(connectionString))
            {
                db.Open();
                db.Close();
            }
        }
    }
}
