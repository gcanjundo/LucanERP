using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class Context:IDisposable
    { 
        public MySqlConnection Connection;

        public Context(string pConnectionString)
        {
            Connection = new MySqlConnection(pConnectionString);
            //this.Connection.Open();
        }

        public void Dispose()
        {
            Connection.Close();
        }


    }
}
