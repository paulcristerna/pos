using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_Venta
{
    public class Pro_Proveedores
    {
        public Int64 Id_Proveedor { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String Razon_Social { get; set; }
        public String Direccion { get; set; }
        public String TelefonoOficina { get; set; }
        public String Celular { get; set; }
        public String Email { get; set; }

        public Pro_Proveedores() { }

        public Pro_Proveedores(Int64 pId_Proveedor, String pNombre, String pApellido, String pRazon_Social, String pDireccion, String pTelefonoOficina, String pCelular, String pEmail)

        {
            this.Id_Proveedor = pId_Proveedor;
            this.Nombre = pNombre;
            this.Apellido = pApellido;
            this.Razon_Social = pRazon_Social;
            this.Direccion = pDireccion;
            this.TelefonoOficina = pTelefonoOficina;
            this.Celular = pCelular;
            this.Email = pEmail;
        }
    }
}
