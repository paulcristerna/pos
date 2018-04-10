using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Punto_Venta
{
    class Reporte
    {
        public static DataTable GenerarReporte(string tipo)
        {
            DataTable retorno = new DataTable();
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                string Consulta = "";

                switch(tipo)
                {
                    case "Todo":
                        Consulta = "SELECT DATE_FORMAT(Fecha, '%d/%m/%Y') AS Fecha, CONCAT('$ ', CAST(Total AS CHAR)) AS Total FROM Ventas WHERE Estado = 'A';";
                        break;
                    case "Día":
                        Consulta = "SELECT DATE_FORMAT(Fecha, '%d/%m/%Y') AS DÃ­a, CONCAT('$ ', CAST(SUM(Total) AS CHAR)) AS Total FROM Ventas WHERE Estado = 'A' GROUP BY DATE(Fecha) ORDER BY IdVenta ASC;";                                       
                    break;
                    case "Semana":
                        Consulta = "SELECT DATE_FORMAT(Fecha, '%d/%m/%Y') AS Semana, CONCAT('$ ', CAST(SUM(Total) AS CHAR)) AS Total FROM Ventas WHERE Estado = 'A' GROUP BY WEEK(Fecha) ORDER BY IdVenta ASC;";
                    break;
                    case "Mes":
                        Consulta = "SELECT DATE_FORMAT(Fecha, '%m') AS Mes, CONCAT('$ ', CAST(SUM(Total) AS CHAR)) AS Total FROM Ventas WHERE Estado = 'A' GROUP BY MONTH(Fecha) ORDER BY IdVenta ASC;";
                    break;
                    case "Año":
                        Consulta = "SELECT DATE_FORMAT(Fecha, '%Y') AS AÃ±o, CONCAT('$ ', CAST(SUM(Total) AS CHAR)) AS Total FROM Ventas WHERE Estado = 'A' GROUP BY YEAR(Fecha) ORDER BY IdVenta ASC;";
                    break;
                }

                byte[] bytes = Encoding.Default.GetBytes(Consulta);
                Consulta = Encoding.UTF8.GetString(bytes);
                
                MySqlCommand Comando = new MySqlCommand(Consulta, conexion);
                MySqlDataAdapter Adaptador = new MySqlDataAdapter(Comando);
                Adaptador.Fill(retorno);
            }

            return retorno;
        }

        public static DataTable GenerarReporteDetalle()
        {
            DataTable retorno = new DataTable();
            using (MySqlConnection conexion = Conexion.MiConexion())
            {
                string Consulta = "SELECT DV.Codigo, P.Nombre, P.Descripcion, Cantidad, PrecioUnitario, TipoUnidad FROM Detalle_Ventas AS DV INNER JOIN Productos AS P ON DV.Codigo = P.Codigo;";

                MySqlCommand Comando = new MySqlCommand(Consulta, conexion);
                MySqlDataAdapter Adaptador = new MySqlDataAdapter(Comando);
                Adaptador.Fill(retorno);
            }

            return retorno;
        }
    }
}
