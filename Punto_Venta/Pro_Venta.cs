using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_Venta
{
    public class Pro_Venta
    {
      /*public Int64 Codigo { get; set; }
      public String Nombre { get; set; }
      public String Descripcion { get; set; }
      public Int16 Cantidad { get; set; }
      public Double Precio { get; set; }
      public Double PrecioTotal { get; set; }

      public Pro_Venta() { }

      public Pro_Venta(Int64 pCodigo, String pNombre, String pDescripcion, Int16 pCantidad, Double pPrecio, Double pPrecioTotal)

      {
          this.Codigo = pCodigo;
          this.Nombre = pNombre;
          this.Descripcion = pDescripcion;
          this.Cantidad = pCantidad;
          this.Precio = pPrecio;
          this.PrecioTotal = pPrecioTotal;
      }*/

        //propiedades de ventas
        public Int64 IdVenta { get; set; }
        public String Cliente { get; set; }
        public Double Total { get; set; }
        public Double PagoCon { get; set; }
        public Double Cambio { get; set; }

        //propiedades de detalle de ventas
        public Int64 IdDetalle { get; set; }
        public Int64 Codigo { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public String TipoUnidad { get; set; }
        public Double Cantidad { get; set; }
        public Double Precio { get; set; }
        public Double PrecioUnitario { get; set; }
        public Double PrecioTotal { get; set; }
        public Double Importe { get; set; }

        public Pro_Venta() { }

        public Pro_Venta(Int64 pIdVenta, String pCliente, Double pTotal, Double pPagoCon, Double pCambio,
                         Int64 pIdDetalle, Int64 pCodigo, String pNombre, String pDescripcion, String pTipoUnidad, Double pCantidad, Double pPrecio, Double pPrecioUnitario, Double pPrecioTotal, Double pImporte)
        {
            //atributos de ventas
            this.IdVenta= pIdVenta;
            this.Cliente = pCliente;
            this.Total = pTotal;
            this.PagoCon = pPagoCon;
            this.Cambio = pCambio;     
 
            //atributos de detalle de ventas
            this.IdDetalle = pIdDetalle;
            this.Codigo = pCodigo;
            this.Nombre= pNombre;
            this.Descripcion = pDescripcion;
            this.TipoUnidad = pTipoUnidad;
            this.Cantidad = pCantidad;
            this.Precio = pPrecio;
            this.PrecioUnitario = pPrecioUnitario;
            this.PrecioTotal = pPrecioTotal;
            this.Importe = pImporte;
        }
    }
}
