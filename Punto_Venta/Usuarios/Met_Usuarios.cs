using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Punto_Venta
{
    class Met_Usuarios
    {
        public static List<Pro_Usuarios> CargarUsuarios()
        {
            List<Pro_Usuarios> ListaUsuarios = new List<Pro_Usuarios>();

            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                string Consulta = "SELECT * FROM Usuarios";
                MySqlCommand Comando = new MySqlCommand(Consulta, conexion);
                MySqlDataReader reader = Comando.ExecuteReader();

                while (reader.Read())
                {
                    Pro_Usuarios pUsuario = new Pro_Usuarios();
                    pUsuario.Nombre = reader[0].ToString();
                    pUsuario.Apellido = reader[1].ToString();
                    pUsuario.Direccion = reader[2].ToString();
                    pUsuario.Usuario = reader[3].ToString();
                    pUsuario.Contrasena = reader[4].ToString();
                    pUsuario.Tipo = reader[5].ToString();
                    ListaUsuarios.Add(pUsuario);
                }
            }
            return ListaUsuarios;
        }     

        public static int Buscar (String NombreUsuario, String Contrasena)
        {
            int resultado = -1;
            MySqlConnection conexion = Conexion.MiConexion();
            MySqlCommand comando = new MySqlCommand(string.Format(
           "SELECT * FROM Usuarios where Usuario ='{0}' and Contrasena ='{1}'", NombreUsuario, Contrasena), conexion);
            MySqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                resultado = 50;
            }
            conexion.Close();
            return resultado;
        }

        public static string BuscarTipo(String NombreUsuario)
        {
            MySqlConnection conexion = Conexion.MiConexion();
            MySqlCommand comando = new MySqlCommand(string.Format(
           "SELECT Tipo FROM Usuarios where Usuario ='{0}' LIMIT 1", NombreUsuario), conexion);
            MySqlDataReader reader = comando.ExecuteReader();
            string Tipo = "";

            while (reader.Read())
            {
                Tipo = reader.GetString(0);
            }
            conexion.Close();
            return Tipo;
        }

        public static int Agregar(Pro_Usuarios pUsuario)
        {
            int retorno = 0;
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand Comando = new MySqlCommand(string.Format("Insert Into Usuarios (Nombre, Apellido, Direccion, Usuario, Contrasena, Tipo) values ('{0}','{1}','{2}','{3}','{4}','{5}')",
                    pUsuario.Nombre, pUsuario.Apellido, pUsuario.Direccion, pUsuario.Usuario, pUsuario.Contrasena, pUsuario.Tipo), conexion);

                retorno = Comando.ExecuteNonQuery();
                conexion.Close();

            }
            return retorno;
        }

        public static int Modificar(Pro_Usuarios pUsuario)
        {
            int retorno = 0;
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand comando = new MySqlCommand(string.Format("Update Usuarios set Nombre='{0}', Apellido='{1}', Direccion='{2}', Contrasena='{3}', Tipo='{4}' where Usuario='{5}'",
                    pUsuario.Nombre, pUsuario.Apellido, pUsuario.Direccion, pUsuario.Contrasena, pUsuario.Tipo, pUsuario.Usuario), conexion);

                retorno = comando.ExecuteNonQuery();
                conexion.Close();
            }
            return retorno;
        }

        public static int Eliminar(String pUsuario)
        {
            int retorno = 0;
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand comando = new MySqlCommand(string.Format("Delete from Usuarios where Usuario='{0}'", pUsuario), conexion);
                retorno = comando.ExecuteNonQuery();
                conexion.Close();
            }
            return retorno;
        }

        public static List<Pro_Usuarios> BuscarUsuarios_Usuario(String pUsuario)
        {
            List<Pro_Usuarios> ListaUsuarios = new List<Pro_Usuarios>();
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand comando = new MySqlCommand(string.Format(
                    //"Select Codigo, Nombre,  Descripcion, Precio from Clientes where Codigo like '%{0}%' or Nombre like '%{1}%'", pCodigo, pNombre), conexion);
                    "Select Nombre, Apellido, Direccion, Usuario, Contrasena, Tipo from Usuarios where Usuario like '%{0}%'", pUsuario), conexion);
                MySqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Pro_Usuarios pUsuarios = new Pro_Usuarios();
                    pUsuarios.Nombre = reader[0].ToString();
                    pUsuarios.Apellido = reader[1].ToString();
                    pUsuarios.Direccion = reader[2].ToString();
                    pUsuarios.Usuario = reader[3].ToString();
                    pUsuarios.Contrasena = reader[4].ToString();
                    pUsuarios.Tipo = reader[5].ToString();
                    ListaUsuarios.Add(pUsuarios);                   
                }
                conexion.Close();
                return ListaUsuarios;
            }
        }

        public static List<Pro_Usuarios> BuscarUsuarios_Nombre(String pNombre)
        {
            List<Pro_Usuarios> ListaUsuarios = new List<Pro_Usuarios>();
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand comando = new MySqlCommand(string.Format(
                    "Select Nombre, Apellido, Direccion, Usuario, Contrasena, Tipo from Usuarios where Nombre like '%{0}%'", pNombre), conexion);
                // "Select Codigo, Nombre,  Descripcion, Precio from Clientes where  Nombre={1}", pNombre), conexion);
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Pro_Usuarios pUsuarios = new Pro_Usuarios();
                    pUsuarios.Nombre = reader[0].ToString();
                    pUsuarios.Apellido = reader[1].ToString();
                    pUsuarios.Direccion = reader[2].ToString();
                    pUsuarios.Usuario = reader[3].ToString();
                    pUsuarios.Contrasena = reader[4].ToString();
                    pUsuarios.Tipo = reader[5].ToString();
                    ListaUsuarios.Add(pUsuarios);
                }

                conexion.Close();
                return ListaUsuarios;
            }
        }

        public static bool Existe(string Usuario)
        {
            string sql = @"SELECT COUNT(*) FROM Usuarios WHERE Usuario = @Usuario";
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand command = new MySqlCommand(sql, conexion);
                command.Parameters.AddWithValue("Usuario", Usuario);
                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count == 0)
                    return false;
                else
                    return true;
            }
        }
    }
}



