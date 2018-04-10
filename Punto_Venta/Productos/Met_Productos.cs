using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Punto_Venta
{
    class Met_Productos
    {
        public static List<Pro_Productos> CargarProductos()
        {
            List<Pro_Productos> ListaProductos = new List<Pro_Productos>();

            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                string Consulta = "SELECT * FROM Productos";
                MySqlCommand Comando = new MySqlCommand(Consulta, conexion);
                MySqlDataReader reader = Comando.ExecuteReader();

                while (reader.Read())
                {
                    Pro_Productos pProducto = new Pro_Productos();
                    pProducto.Codigo = Convert.ToInt64(reader[0].ToString());
                    pProducto.Nombre = reader[1].ToString();
                    pProducto.Descripcion = reader[2].ToString();
                    pProducto.Precio = Convert.ToDouble(reader[3].ToString());
                    pProducto.Stock = Convert.ToDouble(reader[4].ToString());
                    pProducto.TipoUnidad = reader[5].ToString();

                    ListaProductos.Add(pProducto);
                }
            }
            return ListaProductos;
        }

        public static int Agregar (Pro_Productos pProducto)
        {
            int retorno = 0;
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand Comando = new MySqlCommand(string.Format("Insert Into Productos (Codigo, Nombre, Descripcion, Precio, Stock, TipoUnidad) values ('{0}', '{1}','{2}','{3}','{4}','{5}')",
                    pProducto.Codigo, pProducto.Nombre, pProducto.Descripcion, pProducto.Precio, pProducto.Stock, pProducto.TipoUnidad), conexion);

                retorno = Comando.ExecuteNonQuery();
                conexion.Close();

            }
            return retorno;
        }

        public static int Modificar(Pro_Productos pProducto)
        {
            int retorno = 0;
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand comando = new MySqlCommand(string.Format("Update Productos set Nombre='{0}', Descripcion='{1}', Precio='{2}', Stock='{3}', TipoUnidad='{4}' where Codigo={5}",
                    pProducto.Nombre, pProducto.Descripcion, pProducto.Precio, pProducto.Stock, pProducto.TipoUnidad, pProducto.Codigo), conexion);

                retorno = comando.ExecuteNonQuery();
                conexion.Close();
            }
            return retorno;
        }

        public static int Eliminar(Int64 pCodigo)
        {
            int retorno = 0;
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand comando = new MySqlCommand(string.Format("Delete from Productos where Codigo={0}", pCodigo), conexion);
                retorno = comando.ExecuteNonQuery();
                conexion.Close();
            }
            return retorno;
        }

        
        public static List<Pro_Productos> BuscarProductos_Codigo (Int64 pCodigo)
        {
            List<Pro_Productos> Lista = new List<Pro_Productos>();
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand comando = new MySqlCommand(string.Format(
                    //"Select Codigo, Nombre,  Descripcion, Precio from Productos where Codigo like '%{0}%' or Nombre like '%{1}%'", pCodigo, pNombre), conexion);
                    "Select Codigo, Nombre,  Descripcion, Precio, Stock, TipoUnidad from Productos where Codigo={0}", pCodigo), conexion);
                MySqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Pro_Productos pProducto = new Pro_Productos();
                    pProducto.Codigo = reader.GetInt64(0);
                    pProducto.Nombre = reader.GetString(1);
                    pProducto.Descripcion = reader.GetString(2);
                    pProducto.Precio = reader.GetDouble(3);
                    pProducto.Stock = reader.GetInt16(4);
                    pProducto.TipoUnidad = reader.GetString(5);

                    Lista.Add(pProducto);
                }
                conexion.Close();
                return Lista;
            }
        }

        public static List<Pro_Productos> BuscarProductos_Nombre(String pNombre)
        {
            List<Pro_Productos> Lista = new List<Pro_Productos>();
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand comando = new MySqlCommand(string.Format(
                    "Select Codigo, Nombre,  Descripcion, Precio, Stock, TipoUnidad from Productos where Nombre like '%{0}%'", pNombre), conexion);
               // "Select Codigo, Nombre,  Descripcion, Precio from Productos where  Nombre={1}", pNombre), conexion);
                MySqlDataReader reader = comando.ExecuteReader();
                
                while (reader.Read())
                {
                    Pro_Productos pProducto = new Pro_Productos();
                    pProducto.Codigo = reader.GetInt64(0);
                    pProducto.Nombre = reader.GetString(1);
                    pProducto.Descripcion = reader.GetString(2);
                    pProducto.Precio = reader.GetDouble(3);
                    pProducto.Stock = reader.GetInt16(4);
                    pProducto.TipoUnidad = reader.GetString(5);

                    Lista.Add(pProducto);
                }
                 
                conexion.Close();
                return Lista;
            }
        }

        public static int Modificar_Stock(Pro_Productos pProducto)
        {
            int retorno = 0;
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand comando = new MySqlCommand(string.Format("Update Productos set Stock='{0}' where Codigo={1}",
                    pProducto.Stock, pProducto.Codigo), conexion);

                retorno = comando.ExecuteNonQuery();
                conexion.Close();
            }
            return retorno;
        }

        public void Consultar(System.Data.DataTable dt)
        {
            string SQL = "SELECT Nombre, Descripcion FROM Productos ORDER BY Nombre ASC;";
            MySqlConnection conexion = Conexion.MiConexion();
            
            MySqlCommand com = new MySqlCommand(SQL, conexion);

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

        public void Filtro(System.Data.DataTable dt)
        {
            string SQL = "SELECT * FROM Productos ORDER BY Nombre ASC;";
            MySqlConnection conexion = Conexion.MiConexion();

            MySqlCommand com = new MySqlCommand(SQL, conexion);

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

        public static bool Existe(Int64 Codigo)
        {
            string sql = @"SELECT COUNT(*) FROM Productos WHERE Codigo = @Codigo";
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand command = new MySqlCommand(sql, conexion);
                command.Parameters.AddWithValue("Codigo", Codigo);
                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count == 0)
                    return false;
                else
                    return true;
            }
        }

        public static bool Existe_Nombre(string Nombre)
        {
            string sql = @"SELECT COUNT(*) FROM Productos WHERE Nombre = @Nombre";
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand command = new MySqlCommand(sql, conexion);
                command.Parameters.AddWithValue("Nombre", Nombre);
                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count == 0)
                    return false;
                else
                    return true;
            }
        }

       
    }
}
