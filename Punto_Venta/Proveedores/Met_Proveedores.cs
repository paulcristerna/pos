using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Punto_Venta
{
    class Met_Proveedores
    {
        public static List<Pro_Proveedores> CargarProveedores()
        {
            List<Pro_Proveedores> ListaProveedores = new List<Pro_Proveedores>();

            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                string Consulta = "SELECT * FROM Proveedores";
                MySqlCommand Comando = new MySqlCommand(Consulta, conexion);
                MySqlDataReader reader = Comando.ExecuteReader();

                while (reader.Read())
                {
                    Pro_Proveedores pProveedor = new Pro_Proveedores();
                    pProveedor.Id_Proveedor = Convert.ToInt32(reader[0].ToString());
                    pProveedor.Nombre = reader[1].ToString();
                    pProveedor.Apellido = reader[2].ToString();
                    pProveedor.Razon_Social = reader[3].ToString();
                    pProveedor.Direccion = reader[4].ToString();
                    pProveedor.TelefonoOficina = reader[5].ToString();
                    pProveedor.Celular = reader[6].ToString();
                    pProveedor.Email = reader[7].ToString();

                    ListaProveedores.Add(pProveedor);
                }
            }
            return ListaProveedores;
        }

        public static int Agregar(Pro_Proveedores pProveedor)
        {
            int retorno = 0;
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand Comando = new MySqlCommand(string.Format("Insert Into Proveedores (Nombre, Apellido, Razon_Social, Direccion, TelefonoOficina, Celular, Email) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                    pProveedor.Nombre, pProveedor.Apellido, pProveedor.Razon_Social, pProveedor.Direccion, pProveedor.TelefonoOficina, pProveedor.Celular, pProveedor.Email), conexion);

                retorno = Comando.ExecuteNonQuery();
                conexion.Close();

            }
            return retorno;
        }

        public static int Modificar(Pro_Proveedores pProveedor)
        {
            int retorno = 0;
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand comando = new MySqlCommand(string.Format("Update Proveedores set Nombre='{0}', Apellido='{1}', Razon_Social='{2}', Direccion='{3}', TelefonoOficina='{4}', Celular='{5}', Email='{6}' where Email='{7}'",
                    pProveedor.Nombre, pProveedor.Apellido, pProveedor.Razon_Social, pProveedor.Direccion, pProveedor.TelefonoOficina, pProveedor.Celular, pProveedor.Email, pProveedor.Email), conexion);

                retorno = comando.ExecuteNonQuery();
                conexion.Close();
            }
            return retorno;
        }

        public static int Eliminar(String pProveedor)
        {
            int retorno = 0;
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand comando = new MySqlCommand(string.Format("Delete from Proveedores where Id_Proveedor='{0}'", pProveedor), conexion);
                retorno = comando.ExecuteNonQuery();
                conexion.Close();
            }
            return retorno;
        }

        public static List<Pro_Proveedores> BuscarProveedores_Razon(String pRazon_Social)
        {
            List<Pro_Proveedores> Lista = new List<Pro_Proveedores>();
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand comando = new MySqlCommand(string.Format(
                    //"Select Codigo, Nombre,  Descripcion, Precio from Clientes where Codigo like '%{0}%' or Nombre like '%{1}%'", pCodigo, pNombre), conexion);
                    "Select Id_Proveedor, Nombre,  Apellido, Razon_Social, Direccion, TelefonoOficina, Celular, Email from Proveedores where Razon_Social like '%{0}%'", pRazon_Social), conexion);
                MySqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Pro_Proveedores pProveedor = new Pro_Proveedores();
                    pProveedor.Id_Proveedor = Convert.ToInt32(reader[0].ToString());
                    pProveedor.Nombre = reader[1].ToString();
                    pProveedor.Apellido = reader[2].ToString();
                    pProveedor.Razon_Social = reader[3].ToString();
                    pProveedor.Direccion = reader[4].ToString();
                    pProveedor.TelefonoOficina = reader[5].ToString();
                    pProveedor.Celular = reader[6].ToString();
                    pProveedor.Email = reader[7].ToString();
                    Lista.Add(pProveedor);
                }
                conexion.Close();
                return Lista;
            }
        }

        public static List<Pro_Proveedores> BuscarProveedores_Nombre(String pNombre)
        {
            List<Pro_Proveedores> Lista = new List<Pro_Proveedores>();
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand comando = new MySqlCommand(string.Format(
                    "Select Id_Proveedor, Nombre,  Apellido, Razon_Social, Direccion, TelefonoOficina, Celular, Email from Proveedores where Nombre like '%{0}%'", pNombre), conexion);
                // "Select Codigo, Nombre,  Descripcion, Precio from Clientes where  Nombre={1}", pNombre), conexion);
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Pro_Proveedores pProveedor = new Pro_Proveedores();
                    pProveedor.Id_Proveedor = Convert.ToInt32(reader[0].ToString());
                    pProveedor.Nombre = reader[1].ToString();
                    pProveedor.Apellido = reader[2].ToString();
                    pProveedor.Razon_Social = reader[3].ToString();
                    pProveedor.Direccion = reader[4].ToString();
                    pProveedor.TelefonoOficina = reader[5].ToString();
                    pProveedor.Celular = reader[6].ToString();
                    pProveedor.Email = reader[7].ToString();
                    Lista.Add(pProveedor);
                }

                conexion.Close();
                return Lista;
            }
        }

        public static bool Existe(string Email)
        {
            string sql = @"SELECT COUNT(*) FROM Proveedores WHERE Email = @Email";
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
