using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_Venta
{
    public class Pro_Clientes
    {
        public Int64 Id_Cliente { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String Direccion { get; set; }
        public String TelefonoCasa { get; set; }
        public String Celular { get; set; }
        public String Email { get; set; }
        public String Fecha_Nacimiento { get; set; }

        public Pro_Clientes() { }

        public Pro_Clientes(Int64 pId_Cliente, String pNombre, String pApellido, String pDireccion, String pTelefonoCasa, String pCelular, String pEmail, String pFecha_Nacimiento)

        {
            this.Id_Cliente = pId_Cliente;
            this.Nombre = pNombre;
            this.Apellido = pApellido;
            this.Direccion = pDireccion;
            this.TelefonoCasa = pTelefonoCasa;
            this.Celular = pCelular;
            this.Email = pEmail;
            this.Fecha_Nacimiento = pFecha_Nacimiento;
        }
    }
}
