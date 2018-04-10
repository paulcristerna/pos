using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Punto_Venta
{
    public class Conexion
    {
        public static MySqlConnection MiConexion()
        {
            MySqlConnection Conectar = new MySqlConnection("Server=localhost; DataBase=MiniSuper; Uid=root; Pwd=1234;");

            Conectar.Open();
            return Conectar;
        }

        /*
        public MySqlConnection con;
        public MySqlCommand com;

        public Conexion()
        {
            con = new MySqlConnection();
            con.ConnectionString = "Server=localhost; Database=MiniSuper; Uid=root; Pwd=1234";           
        }

        public Conexion(string cadenaConexion)
        {
            con = new MySqlConnection();
            con.ConnectionString = cadenaConexion;
        }

        public int EjecutaComando(string SQL)
        {
            int resultado = 0;

            com = new MySqlCommand(SQL, con);

            try
            {
                con.Open();
                resultado = com.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw error;
            }
            finally
            {
                con.Close();
            }
            return resultado;
        }

        public void EjecutaConsulta(string SQL, System.Data.DataTable dt)
        {
            // Se especifica la instrucción que se lanzará al servidor
            com = new MySqlCommand(SQL, con);

            MySqlDataAdapter adaptador = new MySqlDataAdapter();
            adaptador.SelectCommand = com;

            dt.Clear();
            try
            {
                adaptador.Fill(dt);
            }
            catch (Exception error)
            {
                throw error;
            }
        }
        */ 

    }
}
