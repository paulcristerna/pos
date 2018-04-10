using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Punto_Venta
{
    class Venta
    {
        public static DataTable venta()
        {
            DataTable retorno = new DataTable();
            using (MySqlConnection conexion = Conexion.MiConexion())
            {              
                string Consulta = "SELECT Codigo, Nombre, Descripcion, Precio FROM Productos";
                MySqlCommand Comando = new MySqlCommand(Consulta, conexion);
                MySqlDataAdapter Adaptador = new MySqlDataAdapter(Comando);
                Adaptador.Fill(retorno);
                DataColumn dc = retorno.Columns.Add("PrecioTotal", typeof(decimal));
            }
            return retorno;
        }
        
        public static List<Pro_Venta> BuscarProductos_Codigo(Int64 pCodigo, Int16 pCantidad)
        {            
                Pro_Venta pProducto = new Pro_Venta();

                //preciototal.Expression = "[Precio]*[Cantidad]";  
                List<Pro_Venta> Lista = new List<Pro_Venta>();
                using (MySqlConnection conexion = Conexion.MiConexion())
                {
                        MySqlCommand comando = new MySqlCommand(string.Format(
                            //"Select Codigo, Nombre,  Descripcion, Precio from Productos where Codigo like '%{0}%' or Nombre like '%{1}%'", pCodigo, pNombre), conexion);
                            "Select Codigo, Nombre, Descripcion, Precio from Productos where Codigo={0}", pCodigo), conexion);

                        MySqlDataReader reader = comando.ExecuteReader();
                        
                        //MySqlDataAdapter Adaptador = new MySqlDataAdapter(comando);

                        while (reader.Read())
                        {

                            //Pro_Venta pProducto = new Pro_Venta();
                            pProducto.Codigo = reader.GetInt64(0);
                            pProducto.Nombre = reader.GetString(1);
                            pProducto.Descripcion = reader.GetString(2);
                            pProducto.Cantidad = Convert.ToInt16(pCantidad);
                            pProducto.Precio = reader.GetDouble(3);

                            double preciototal = pProducto.Precio * pProducto.Cantidad;
                            pProducto.PrecioTotal = Convert.ToDouble(preciototal);
                            //pProducto.Cantidad = Convert.ToInt16(cantidad);

                            Lista.Add(pProducto);
                        }                                          
                        conexion.Close();
                    
                        return Lista;                                           
                }           
        }

        public static int AgregarVenta(Pro_Venta pVenta)
        {
            int retorno = 0;
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand Comando = new MySqlCommand(string.Format("Insert Into Ventas (Cliente, Total, PagoCon, Cambio, Estado) values ('{0}','{1}','{2}','{3}','A')",
                    pVenta.Cliente, pVenta.Total, pVenta.PagoCon, pVenta.Cambio), conexion);

                retorno = Comando.ExecuteNonQuery();
                conexion.Close();

            }
            return retorno;
        }

        public static int AgregarDetalleVenta(Pro_Venta pVenta)
        {
            int retorno = 0;
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand Comando = new MySqlCommand(string.Format("Insert Into Detalle_Ventas (Codigo, Cantidad, PrecioUnitario) values ('{0}','{1}','{2}')",
                    pVenta.Codigo, pVenta.Cantidad, pVenta.Precio), conexion);

                retorno = Comando.ExecuteNonQuery();
                conexion.Close();

            }
            return retorno;
        }



        public static List<Pro_Venta> BuscarProductos_Codigo_Venta(Int64 pCodigo)
        {
            Pro_Venta pProducto = new Pro_Venta();
            //preciototal.Expression = "[Precio]*[Cantidad]";  
            List<Pro_Venta> Lista = new List<Pro_Venta>();
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand comando = new MySqlCommand(string.Format(
                    "Select Codigo, Nombre, Descripcion, TipoUnidad, Precio from Productos where Codigo={0}", pCodigo), conexion);
                MySqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    pProducto.Codigo = reader.GetInt64(0);
                    pProducto.Nombre = reader.GetString(1);
                    pProducto.Descripcion = reader.GetString(2);
                    pProducto.TipoUnidad = reader.GetString(3);
                    pProducto.Precio = reader.GetDouble(4);
                    //pProducto.Cantidad = Convert.ToInt16(pCantidad);
                    double preciototal = pProducto.Precio * pProducto.Cantidad;
                    pProducto.PrecioTotal = Convert.ToDouble(preciototal);
                    //pProducto.Cantidad = Convert.ToInt16(cantidad);
                    Lista.Add(pProducto);
                }
                conexion.Close();

                return Lista;
            }
        }

        public static int AgregarVenta_Proceso(Pro_Venta pVenta)
        {
            int retorno = 0;
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand Comando = new MySqlCommand(string.Format("Insert Into Venta_Proceso  (Codigo, Nombre, Descripcion, TipoUnidad, PrecioUnitario, Cantidad, Importe) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                    pVenta.Codigo, pVenta.Nombre, pVenta.Descripcion, pVenta.TipoUnidad, pVenta.PrecioUnitario, pVenta.Cantidad, pVenta.Importe), conexion);
                retorno = Comando.ExecuteNonQuery();
                conexion.Close();
            }
            return retorno;
        }

        public static List<Pro_Venta> Cargar_Lista()
        {
            Pro_Venta pProducto = new Pro_Venta();
            List<Pro_Venta> Lista = new List<Pro_Venta>();
            DataTable retorno = new DataTable();
            using (MySqlConnection conexion = Conexion.MiConexion())
            {               
                MySqlCommand comando = new MySqlCommand(string.Format(
                    "Select * from Venta_Proceso"), conexion);
                MySqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    pProducto.Codigo = reader.GetInt64(0);
                    pProducto.Nombre = reader.GetString(1);
                    pProducto.Descripcion = reader.GetString(2);
                    pProducto.TipoUnidad = reader.GetString(3);
                    pProducto.Precio = reader.GetDouble(4);
                    pProducto.Cantidad = reader.GetDouble(5);
                    pProducto.PrecioUnitario = reader.GetDouble(6);                 
  
                    Lista.Add(pProducto);
                }
                conexion.Close();
                return Lista;
            }
            
        }


        public static bool Existe(Int64 Codigo)
        {
            string sql = @"SELECT COUNT(*) FROM Venta_Proceso WHERE Codigo = @Codigo";
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

        public static int Modificar_Venta(Pro_Venta pVenta)
        {
            int retorno = 0;
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand comando = new MySqlCommand(string.Format("Update Venta_Proceso set Cantidad='{0}', Importe='{1}' where Codigo={2}",
                    pVenta.Cantidad, pVenta.Importe, pVenta.Codigo), conexion);

                retorno = comando.ExecuteNonQuery();
                conexion.Close();
            }
            return retorno;
        }

        public static int Borrar_Tabla_Venta_Proceso()
        {
            int retorno = 0;
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand comando = new MySqlCommand(string.Format("Delete From Venta_Proceso"), conexion);

                retorno = comando.ExecuteNonQuery();
                conexion.Close();
            }
            return retorno;
        }

        public static int Quitar_Producto_Venta_Proceso(Int64 pCodigo)
        {
            int retorno = 0;
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                MySqlCommand comando = new MySqlCommand(string.Format("Delete from Venta_Proceso where Codigo={0}", pCodigo), conexion);
                retorno = comando.ExecuteNonQuery();
                conexion.Close();
            }
            return retorno;
        }
     
    }
}
