using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        public Menu(string Tipo)
        {
            InitializeComponent();            
        }

        //Metodo de Load

        private void Menu_Load(object sender, EventArgs e)
        {
            tssUsuario.Text = "Usuario: " + Form1.user;
            string tipo = Met_Usuarios.BuscarTipo(Form1.user);

            switch (tipo)
            {
                case "Cajero":
                    btnProductos.Enabled = false;
                    btnAgregarUsuarios.Enabled = false;
                    btnAgregarClientes.Enabled = false;
                    btnAgregarProveedores.Enabled = false;
                    btnReportes.Enabled = false;
                    break;
                case "Inventarios":
                    btnPuntoVenta.Enabled = false;
                    btnProductos.Enabled = false;
                    btnAgregarUsuarios.Enabled = false;
                    btnAgregarClientes.Enabled = false;
                    btnAgregarProveedores.Enabled = false;
                    btnReportes.Enabled = false;
                    break;
            }
        }

        //Metodo para abrir ventanas

        private void btnPuntoVenta_Click(object sender, EventArgs e)
        {
            this.Hide();
            PuntoVenta Open = new PuntoVenta();
            Open.ShowDialog();
        }

        private void btnInventarios_Click(object sender, EventArgs e)
        {
            this.Hide();
            Inventarios Open = new Inventarios();
            Open.ShowDialog();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            this.Hide();
            Productos Open = new Productos();
            Open.ShowDialog();
        }

        private void btnAgregarUsuarios_Click(object sender, EventArgs e)
        {
            this.Hide();
            Usuarios Open = new Usuarios();
            Open.ShowDialog();
        }

        private void btnAgregarClientes_Click(object sender, EventArgs e)
        {
            this.Hide();
            Clientes Open = new Clientes();
            Open.ShowDialog();
        }

        private void btnAgregarProveedores_Click(object sender, EventArgs e)
        {
            this.Hide();
            Proveedores Open = new Proveedores();
            Open.ShowDialog();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            this.Hide();
            Reportes_Ventas Open = new Reportes_Ventas();
            Open.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 Open = new Form1();
            Open.ShowDialog();
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            this.Hide();
            Ayuda Open = new Ayuda();
            Open.ShowDialog();
        }

        private void lblPuntoVenta_Click(object sender, EventArgs e)
        {
            this.Hide();
            PuntoVenta Open = new PuntoVenta();
            Open.ShowDialog();
        }

        private void lblInventarios_Click(object sender, EventArgs e)
        {
            this.Hide();
            Inventarios Open = new Inventarios();
            Open.ShowDialog();
        }

        private void lblProductos_Click(object sender, EventArgs e)
        {
            this.Hide();
            Productos Open = new Productos();
            Open.ShowDialog();
        }

        private void lblAgregarUsuarios_Click(object sender, EventArgs e)
        {
            this.Hide();
            Usuarios Open = new Usuarios();
            Open.ShowDialog();
        }

        private void lblAgregarClientes_Click(object sender, EventArgs e)
        {
            this.Hide();
            Clientes Open = new Clientes();
            Open.ShowDialog();
        }

        private void lblAgregarProveedores_Click(object sender, EventArgs e)
        {
            this.Hide();
            Proveedores Open = new Proveedores();
            Open.ShowDialog();
        }

        private void lblReportes_Click(object sender, EventArgs e)
        {
            this.Hide();
            Reportes_Ventas Open = new Reportes_Ventas();
            Open.ShowDialog();
        }

        private void lblSalir_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 Open = new Form1();
            Open.ShowDialog();
        }

        private void lblAyuda_Click(object sender, EventArgs e)
        {
            this.Hide();
            Ayuda Open = new Ayuda();
            Open.ShowDialog();
        }

        //Metodo de ToolTip

        private void btnPuntoVenta_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnPuntoVenta, "Ir a punto de venta");
        }

        private void lblPuntoVenta_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnPuntoVenta, "Ir a punto de venta");
        }

        private void btnInventarios_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnInventarios, "Inventarios");
        }

        private void lblInventarios_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnInventarios, "Inventarios");
        }

        private void btnProductos_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnProductos, "Productos");
        }

        private void lblProductos_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnProductos, "Productos");
        }

        private void btnAgregarUsuarios_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnAgregarUsuarios, "Agregar usuarios");
        }

        private void lblAgregarUsuarios_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnAgregarUsuarios, "Agregar usuarios");
        }

        private void btnAgregarClientes_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnAgregarClientes, "Agregar clientes");
        }

        private void lblAgregarClientes_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnAgregarClientes, "Agregar clientes");
        }

        private void btnAgregarProveedores_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnAgregarProveedores, "Proveedores");
        }

        private void lblAgregarProveedores_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnAgregarProveedores, "Proveedores");
        }

        private void btnReportes_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnReportes, "Ver reportes de ventas");
        }

        private void lblReportes_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnReportes, "Ver reportes de ventas");
        }

        private void btnSalir_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnSalir, "Cerrar sesion y salir");
        }

        private void lblSalir_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnSalir, "Cerrar sesion y salir");
        }

        private void btnAyuda_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnAyuda, "Ver Ayuda");
        }

        private void lblAyuda_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnAyuda, "Ver Ayuda");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = DateTime.Now.ToShortDateString().ToString() + " - " + DateTime.Now.ToLongTimeString().ToString();
        }

    }
}
