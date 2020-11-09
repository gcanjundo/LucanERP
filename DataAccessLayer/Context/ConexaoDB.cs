
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;

namespace DataAccessLayer
{
    public class ConexaoDB : IDisposable
    {
        #region Declaracao de Variaveis globais

        public string StringConnection { get; set; }

        private MySqlCommand comando = new MySqlCommand();

        #endregion

        public ConexaoDB()
        {
            StringConnection = "server=localhost; user id=root;password=!2u80oklb@@13%; port=3307; database=base_kitanda_erp;";
            
            MySqlConnection conexao = new MySqlConnection(StringConnection); 
            Comando.Connection = conexao; 
            Comando.CommandType = CommandType.StoredProcedure;
        }

        public DataSet ExecuteDataSet()
        {
            DataSet ds = new DataSet();
            try
            { 
                MySqlDataAdapter da = new MySqlDataAdapter(Comando);
                AbrirConexao();
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                FecharConexao();
                throw new Exception(ex.Message);
            }
            finally
            {
                FecharConexao();
            }

            return ds;
        }



        public int ExecuteNonQuery()
        {
             
            try
            {
                AbrirConexao();
                return Comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public int ExecuteInsert()
        {  
            try
            {
                Comando.Parameters.Add("LSTID", MySqlDbType.Int32);
                Comando.Parameters["LSTID"].Direction = ParameterDirection.Output;

                FecharConexao();
                AbrirConexao(); 

                Comando.ExecuteNonQuery();
                return Convert.ToInt32(comando.Parameters["LSTID"].Value);

            }
            catch (Exception ex)
            {
                FecharConexao();
                throw new Exception(ex.Message);
            }

             
        }

         

        public MySqlDataReader ExecuteReader()
        {
            try
            {
                this.AbrirConexao();
                return Comando.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception ex)
            {
                FecharConexao();
                throw new Exception(ex.Message);
            }
        }

        public void AddParameter(string pNome, object dto)
        {
            //MySqlParameter param = new MySqlParameter(pNome, dto);
            Comando.Parameters.AddWithValue(pNome, dto);
        }


        public void AddParameter(string pNome)
        {
            //MySqlParameter param = new MySqlParameter(pNome, dto);
            Comando.Parameters.Add(pNome, MySqlDbType.Int32, 4);
        }

        public void ParametroRetorno()
        {
            Comando.Parameters.Add("RecordCount", MySqlDbType.Int32);
            Comando.Parameters["RecordCount"].Direction = ParameterDirection.Output;
        }

        public int NumeroRegistos()
        {
            return Convert.ToInt32(Comando.Parameters["RecordCount"].Value);
        }
        public string ComandText
        {
            get
            {
                return Comando.CommandText;
            }
            set
            {
                Comando.CommandText = value;
                Comando.Parameters.Clear();
            }
        }

         

        public MySqlCommand Comando { get => comando; set => comando = value; }

        public void AbrirConexao()
        {
            Comando.Connection.ConnectionString = this.StringConnection;
            if (Comando.Connection.State == ConnectionState.Open)
                Comando.Connection.Close();

            Comando.Connection.Open();

        }

        public void FecharConexao()
        {
            if (Comando.Connection.State == ConnectionState.Open)
                Comando.Connection.Close();
        } 
         

        public void Dispose()
        {
            comando.Connection.Close();
        }
    }
}
