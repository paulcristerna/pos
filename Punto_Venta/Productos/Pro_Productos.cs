using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Punto_Venta
{
    public class Pro_Productos
    {
      public Int64 Codigo { get; set; }
      public String Nombre { get; set; }
      public String Descripcion { get; set; }
      public Double Precio { get; set; }
      public Double Stock { get; set; }
      public String TipoUnidad { get; set; }

      public Pro_Productos() { }

      public Pro_Productos(Int64 pCodigo, String pNombre, String pDescripcion, Double pPrecio, Double pStock, String pTipoUnidad)

      {
          this.Codigo = pCodigo;
          this.Nombre = pNombre;
          this.Descripcion = pDescripcion;
          this.Precio = pPrecio;
          this.Stock = pStock;
          this.TipoUnidad=pTipoUnidad;
      
      }
    }
}
