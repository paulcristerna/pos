using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Punto_Venta
{
    public class Pro_Usuarios
    {
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String Direccion { get; set; }
        public String Usuario { get; set; }
        public String Contrasena { get; set; }
        public String Tipo { get; set; }

        public Pro_Usuarios() { }

        public Pro_Usuarios(String pNombre, String pApellido, String pDireccion, String pUsuario, String pContrasena, String pTipo)

        {
            this.Nombre = pNombre;
            this.Apellido = pApellido;
            this.Direccion = pDireccion;
            this.Usuario = pUsuario;
            this.Contrasena = pContrasena;
            this.Tipo = pTipo;
        }
    }
}
