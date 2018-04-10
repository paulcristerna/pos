using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices; //Servicio para activar user32.dll

namespace Punto_Venta
{
    public partial class Form1 : Form
    {
        public static string user;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            user = txtNombreUsuario.Text;

            /*this.Hide();
            Menu_Principal Abrir = new Menu_Principal();
            Abrir.ShowDialog();*/

            if (Met_Usuarios.Buscar(txtNombreUsuario.Text, txtContrasena.Text) > 0)
            {
                this.Hide();
                Menu Abrir = new Menu();
                Abrir.ShowDialog();
            }
            else
            {
                MessageBox.Show("Error en los datos");
                txtNombreUsuario.Clear();
                txtContrasena.Clear();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblAceptar_Click(object sender, EventArgs e)
        {
            user = txtNombreUsuario.Text;

            /*this.Hide();
            Menu_Principal Abrir = new Menu_Principal();
            Abrir.ShowDialog();*/

            if (Met_Usuarios.Buscar(txtNombreUsuario.Text, txtContrasena.Text) > 0)
            {
                this.Hide();
                Menu Abrir = new Menu();
                Abrir.ShowDialog();
            }
            else
            {
                MessageBox.Show("Error en los datos");
                txtNombreUsuario.Clear();
                txtContrasena.Clear();
            }
        }

        private void lblCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
                   
        private void txtContrasena_KeyPress(object sender, KeyPressEventArgs e)
        {
            user = txtNombreUsuario.Text;

            /*this.Hide();
            Menu_Principal Abrir = new Menu_Principal();
            Abrir.ShowDialog();*/

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Met_Usuarios.Buscar(txtNombreUsuario.Text, txtContrasena.Text) > 0)
                {
                    this.Hide();
                    Menu Abrir = new Menu();
                    Abrir.ShowDialog();
                }
                else
                MessageBox.Show("Error en los datos");
                txtNombreUsuario.Clear();
                txtContrasena.Clear();
                txtNombreUsuario.Focus();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtNombreUsuario.Focus();
        }       

        private void txtNombreUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }             

    }
}
