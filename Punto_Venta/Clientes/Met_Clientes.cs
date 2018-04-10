using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Punto_Venta
{
    class Met_Clientes
    {
        public static List<Pro_Clientes> CargarClientes()
        {
            List<Pro_Clientes> ListaClientes = new List<Pro_Clientes>();

            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                string Consulta = "SELECT * FROM Clientes";
                MySqlCommand Comando = new MySqlCommand(Consulta, conexion);
                MySqlDataReader reader = Comando.ExecuteReader();

                while (reader.Read())
                {
                    Pro_Clientes pCliente = new Pro_Clientes();
                    pCliente.Id_Cliente = Convert.ToInt32(reader[0].ToString());
                    pCliente.Nombre = reader[1].ToString();
                    pCliente.Apellido = reader[2].ToString();
                    pCliente.Direccion = reader[3].ToString();
                    pCliente.TelefonoCasa = reader[4].ToString();
                    pCliente.Celular = reader[5].ToString();
                    pCliente.Email = reader[6].ToString();
                    pCliente.Fecha_Nacimiento = reader[7].ToString();

                    ListaClientes.Add(pCliente);
                }
            }
            return ListaClientes;
        }

        public static int Agregar(Pro_Clientes pCliente)
        {
            int retorno = 0;
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand Comando = new MySqlCommand(string.Format("Insert Into Clientes (Nombre, Apellido, Direccion, TelefonoCasa, Celular, Email, FechaNacimiento) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                    pCliente.Nombre, pCliente.Apellido, pCliente.Direccion, pCliente.TelefonoCasa, pCliente.Celular, pCliente.Email, pCliente.Fecha_Nacimiento), conexion);

                retorno = Comando.ExecuteNonQuery();
                conexion.Close();

            }
            return retorno;
        }

        public static int Modificar(Pro_Clientes pCliente)
        {
            int retorno = 0;
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand comando = new MySqlCommand(string.Format("Update Clientes set Nombre='{0}', Apellido='{1}', Direccion='{2}', TelefonoCasa='{3}', Celular='{4}', Email='{5}' where Id_Cliente='{6}'",
                    pCliente.Nombre, pCliente.Apellido, pCliente.Direccion, pCliente.TelefonoCasa, pCliente.Celular,pCliente.Email, pCliente.Id_Cliente), conexion);

                retorno = comando.ExecuteNonQuery();
                conexion.Close();
            }
            return retorno;
        }

        public static int Eliminar(String pCliente)
        {
            int retorno = 0;
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand comando = new MySqlCommand(string.Format("Delete from Clientes where Email='{0}'", pCliente), conexion);
                retorno = comando.ExecuteNonQuery();
                conexion.Close();
            }
            return retorno;
        }

        public static List<Pro_Clientes> BuscarClientes_Email(String pEmail)
        {
            List<Pro_Clientes> Lista = new List<Pro_Clientes>();
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand comando = new MySqlCommand(string.Format(
                    //"Select Codigo, Nombre,  Descripcion, Precio from Clientes where Codigo like '%{0}%' or Nombre like '%{1}%'", pCodigo, pNombre), conexion);
                    "Select Id_Cliente, Nombre,  Apellido, Direccion, TelefonoCasa, Celular, Email, FechaNacimiento from Clientes where Email like '%{0}%'", pEmail), conexion);
                MySqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Pro_Clientes pCliente = new Pro_Clientes();
                    pCliente.Id_Cliente = Convert.ToInt32(reader[0].ToString());
                    pCliente.Nombre = reader[1].ToString();
                    pCliente.Apellido = reader[2].ToString();
                    pCliente.Direccion = reader[3].ToString();
                    pCliente.TelefonoCasa = reader[4].ToString();
                    pCliente.Celular = reader[5].ToString();
                    pCliente.Email = reader[6].ToString();
                    pCliente.Fecha_Nacimiento = reader[7].ToString();
                    Lista.Add(pCliente);
                }
                conexion.Close();
                return Lista;
            }
        }

        public static List<Pro_Clientes> BuscarClientes_Nombre(String pNombre)
        {
            List<Pro_Clientes> Lista = new List<Pro_Clientes>();
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand comando = new MySqlCommand(string.Format(
                    "Select Id_Cliente, Nombre,  Apellido, Direccion, TelefonoCasa, Celular, Email, FechaNacimiento from Clientes where Nombre like '%{0}%'", pNombre), conexion);
                // "Select Codigo, Nombre,  Descripcion, Precio from Clientes where  Nombre={1}", pNombre), conexion);
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Pro_Clientes pCliente = new Pro_Clientes();
                    pCliente.Id_Cliente = Convert.ToInt32(reader[0].ToString());
                    pCliente.Nombre = reader[1].ToString();
                    pCliente.Apellido = reader[2].ToString();
                    pCliente.Direccion = reader[3].ToString();
                    pCliente.TelefonoCasa = reader[4].ToString();
                    pCliente.Celular = reader[5].ToString();
                    pCliente.Email = reader[6].ToString();
                    pCliente.Fecha_Nacimiento = reader[7].ToString();
                    Lista.Add(pCliente);
                }

                conexion.Close();
                return Lista;
            }
        }

        public static bool Existe(string Email)
        {
            string sql = @"SELECT COUNT(*) FROM Clientes WHERE Email = @Email";
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand command = new MySqlCommand(sql, conexion);
                command.Parameters.AddWithValue("Email", Email);
                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count == 0)
                    return false;
                else
                    return true;
            }
        }


    }
}
