using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Punto_Venta
{
    public partial class Usuarios : Form
    {
        public Usuarios()
        {
            InitializeComponent();
        }

        void limpiar()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtDireccion.Clear();
            txtNombreUsuario.Clear();
            txtContrasena.Clear();
            txtConfirmar_Contrasena.Clear();
            cmbTipo.Text = "Tipo";
            txtUsuario_Buscar.Focus();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu Open = new Menu();
            Open.ShowDialog();
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = Met_Usuarios.CargarUsuarios();
            txtUsuario_Buscar.Focus();
        }

        private void btnGuardar_Usuario_Click(object sender, EventArgs e)
        {
            try
            {
                Pro_Usuarios Pro_Usuarios = new Pro_Usuarios();
                if (txtNombre.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Nombre", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNombre.Focus();
                    return;
                }
                if (txtApellido.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Apellido", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtApellido.Focus();
                    return;
                }                
                if (txtDireccion.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Direccion", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDireccion.Focus();
                    return;
                }
                if (txtNombreUsuario.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Nombre de Usuario", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNombreUsuario.Focus();
                    return;
                }
                if (txtContrasena.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Contraseña", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContrasena.Focus();
                    return;
                }
                if (txtConfirmar_Contrasena.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Confirmar Contraseña", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtConfirmar_Contrasena.Focus();
                    return;
                }
                if (cmbTipo.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Tipo de Usuario", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbTipo.Focus();
                    return;
                }

                if (!Met_Usuarios.Existe(txtNombreUsuario.Text) == false)
                {
                    Pro_Usuarios.Nombre = txtNombre.Text;
                    Pro_Usuarios.Apellido = txtApellido.Text;
                    Pro_Usuarios.Direccion = txtDireccion.Text;
                    Pro_Usuarios.Usuario = txtNombreUsuario.Text;
                    Pro_Usuarios.Contrasena = txtContrasena.Text;
                    Pro_Usuarios.Tipo = cmbTipo.Text;
                    Met_Usuarios.Modificar(Pro_Usuarios);
                    MessageBox.Show("Datos Modificados Correctamente", "Datos Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Usuarios.CargarUsuarios();
                    limpiar();
                }
                else
                {
                    Pro_Usuarios.Nombre = txtNombre.Text;
                    Pro_Usuarios.Apellido = txtApellido.Text;
                    Pro_Usuarios.Direccion = txtDireccion.Text;
                    Pro_Usuarios.Usuario = txtNombreUsuario.Text;
                    Pro_Usuarios.Contrasena = txtContrasena.Text;
                    Pro_Usuarios.Tipo = cmbTipo.Text;
                    Met_Usuarios.Agregar(Pro_Usuarios);
                    MessageBox.Show("Datos Guardados Correctamente", "Datos Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Usuarios.CargarUsuarios();
                    limpiar();
                }
            }
            catch
            {
                MessageBox.Show("No se pudieron Guardar lo datos", "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEliminar_Usuario_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea eliminar este Usuario??", "Esta seguro?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int resultado = Met_Usuarios.Eliminar(txtNombreUsuario.Text);
                if (resultado > 0)
                {
                    MessageBox.Show("Usuario Eliminado Correctamente", "Usuario Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Usuarios.CargarUsuarios();
                    limpiar();
                }

                else
                {
                    MessageBox.Show("No se pudo Eliminar el Usuario", "Ocurrio un error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            else
                MessageBox.Show("Se cancelo la eliminacion", "Cancelado");
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {

        }

        private void txtUsuario_Buscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = Met_Usuarios.BuscarUsuarios_Usuario(txtUsuario_Buscar.Text);
            }
            catch (Exception)
            {
                dataGridView1.DataSource = Met_Usuarios.CargarUsuarios();
            }
        }

        private void txtNombre_Buscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = Met_Usuarios.BuscarUsuarios_Nombre(txtNombre_Buscar.Text);
            }
            catch (Exception)
            {
                dataGridView1.DataSource = Met_Usuarios.CargarUsuarios();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNombre.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[0].Value);
            txtApellido.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[1].Value);
            txtDireccion.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[2].Value);
            txtNombreUsuario.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[3].Value);
            txtContrasena.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[4].Value);
            txtConfirmar_Contrasena.Text= Convert.ToString(this.dataGridView1.CurrentRow.Cells[4].Value);
            cmbTipo.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[5].Value);           
        }

        //Labels
        private void lblGuardar_Usuario_Click(object sender, EventArgs e)
        {
            try
            {
                Pro_Usuarios Pro_Usuarios = new Pro_Usuarios();
                if (txtNombre.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Nombre", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNombre.Focus();
                    return;
                }
                if (txtApellido.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Apellido", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtApellido.Focus();
                    return;
                }
                if (txtDireccion.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Direccion", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDireccion.Focus();
                    return;
                }
                if (txtNombreUsuario.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Nombre de Usuario", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNombreUsuario.Focus();
                    return;
                }
                if (txtContrasena.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Contraseña", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContrasena.Focus();
                    return;
                }
                if (txtConfirmar_Contrasena.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Confirmar Contraseña", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtConfirmar_Contrasena.Focus();
                    return;
                }
                if (cmbTipo.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Tipo de Usuario", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbTipo.Focus();
                    return;
                }

                if (!Met_Usuarios.Existe(txtNombreUsuario.Text) == false)
                {
                    Pro_Usuarios.Nombre = txtNombre.Text;
                    Pro_Usuarios.Apellido = txtApellido.Text;
                    Pro_Usuarios.Direccion = txtDireccion.Text;
                    Pro_Usuarios.Usuario = txtNombreUsuario.Text;
                    Pro_Usuarios.Contrasena = txtContrasena.Text;
                    Pro_Usuarios.Tipo = cmbTipo.Text;
                    Met_Usuarios.Modificar(Pro_Usuarios);
                    MessageBox.Show("Datos Modificados Correctamente", "Datos Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Usuarios.CargarUsuarios();
                    limpiar();
                }
                else
                {
                    Pro_Usuarios.Nombre = txtNombre.Text;
                    Pro_Usuarios.Apellido = txtApellido.Text;
                    Pro_Usuarios.Direccion = txtDireccion.Text;
                    Pro_Usuarios.Usuario = txtNombreUsuario.Text;
                    Pro_Usuarios.Contrasena = txtContrasena.Text;
                    Pro_Usuarios.Tipo = cmbTipo.Text;
                    Met_Usuarios.Agregar(Pro_Usuarios);
                    MessageBox.Show("Datos Guardados Correctamente", "Datos Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Usuarios.CargarUsuarios();
                    limpiar();
                }
            }
            catch
            {
                MessageBox.Show("No se pudieron Guardar lo datos", "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lblEliminar_Usuario_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea eliminar este Usuario??", "Esta seguro?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int resultado = Met_Usuarios.Eliminar(txtNombreUsuario.Text);
                if (resultado > 0)
                {
                    MessageBox.Show("Usuario Eliminado Correctamente", "Usuario Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Usuarios.CargarUsuarios();
                    limpiar();
                }

                else
                {
                    MessageBox.Show("No se pudo Eliminar el Usuario", "Ocurrio un error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            else
                MessageBox.Show("Se cancelo la eliminacion", "Cancelado");
        }

        private void lblLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void lblAtras_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu Open = new Menu();
            Open.ShowDialog();
        }

        private void lblAyuda_Click(object sender, EventArgs e)
        {

        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtNombreUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtContrasena_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtConfirmar_Contrasena_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }
        
    }
}
