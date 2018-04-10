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
    public partial class Proveedores : Form
    {
        public Proveedores()
        {
            InitializeComponent();
        }

        void limpiar()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtRazon_Social.Clear();
            txtDireccion.Clear();
            txtTelefonoOficina.Clear();
            txtCelular.Clear();
            txtEmail.Clear();
            txtRazon_Buscar.Focus();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu Open = new Menu();
            Open.ShowDialog();
        }

        private void Proveedores_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = Met_Proveedores.CargarProveedores();
            txtRazon_Buscar.Focus();
        }

        private void btnGuardar_Proveedor_Click(object sender, EventArgs e)
        {
            try
            {
                Pro_Proveedores Pro_Proveedores = new Pro_Proveedores();
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
                if (txtRazon_Social.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Razon Social", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRazon_Social.Focus();
                    return;
                }
                if (txtDireccion.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Direccion", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDireccion.Focus();
                    return;
                }
                if (txtTelefonoOficina.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Telefono Oficina", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTelefonoOficina.Focus();
                    return;
                }
                if (txtCelular.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Celular", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCelular.Focus();
                    return;
                }
                if (txtEmail.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Email", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Focus();
                    return;
                }

                if (!Met_Proveedores.Existe(txtEmail.Text) == false)
                {
                    Pro_Proveedores.Nombre = txtNombre.Text;
                    Pro_Proveedores.Apellido = txtApellido.Text;
                    Pro_Proveedores.Razon_Social = txtRazon_Social.Text;
                    Pro_Proveedores.Direccion = txtDireccion.Text;
                    Pro_Proveedores.TelefonoOficina = txtTelefonoOficina.Text;
                    Pro_Proveedores.Celular = txtCelular.Text;
                    Pro_Proveedores.Email = txtEmail.Text;
                    Met_Proveedores.Modificar(Pro_Proveedores);
                    MessageBox.Show("Datos Modificados Correctamente", "Datos Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Proveedores.CargarProveedores();
                    limpiar();
                }
                else
                {
                    Pro_Proveedores.Nombre = txtNombre.Text;
                    Pro_Proveedores.Apellido = txtApellido.Text;
                    Pro_Proveedores.Razon_Social = txtRazon_Social.Text;
                    Pro_Proveedores.Direccion = txtDireccion.Text;
                    Pro_Proveedores.TelefonoOficina = txtTelefonoOficina.Text;
                    Pro_Proveedores.Celular = txtCelular.Text;
                    Pro_Proveedores.Email = txtEmail.Text;
                    Met_Proveedores.Agregar(Pro_Proveedores);
                    MessageBox.Show("Datos Guardados Correctamente", "Datos Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Proveedores.CargarProveedores();
                    limpiar();
                }
            }
            catch
            {
                MessageBox.Show("No se pudieron Guardar lo datos", "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEliminar_Proveedor_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea eliminar este Proveedor??", "Esta seguro?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int resultado = Met_Proveedores.Eliminar(txtEmail.Text);
                if (resultado > 0)
                {
                    MessageBox.Show("Proveedor Eliminado Correctamente", "Proveedor Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Proveedores.CargarProveedores();
                    limpiar();
                }

                else
                {
                    MessageBox.Show("No se pudo Eliminar el Proveedor", "Ocurrio un error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void txtRazon_Buscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = Met_Proveedores.BuscarProveedores_Razon(txtRazon_Buscar.Text);
            }
            catch (Exception)
            {
                dataGridView1.DataSource = Met_Proveedores.CargarProveedores();
            }
        }

        private void txtNombre_Buscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = Met_Proveedores.BuscarProveedores_Nombre(txtNombre_Buscar.Text);
            }
            catch (Exception)
            {
                dataGridView1.DataSource = Met_Proveedores.CargarProveedores();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNombre.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[1].Value);
            txtApellido.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[2].Value);
            txtRazon_Social.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[3].Value);
            txtDireccion.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[4].Value);
            txtTelefonoOficina.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[5].Value);
            txtCelular.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[6].Value);
            txtEmail.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[7].Value);
        }

        //Labels
        private void lblGuardar_Proveedor_Click(object sender, EventArgs e)
        {
            try
            {
                Pro_Proveedores Pro_Proveedores = new Pro_Proveedores();
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
                if (txtRazon_Social.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Razon Social", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRazon_Social.Focus();
                    return;
                }
                if (txtDireccion.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Direccion", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDireccion.Focus();
                    return;
                }
                if (txtTelefonoOficina.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Telefono Oficina", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTelefonoOficina.Focus();
                    return;
                }
                if (txtCelular.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Celular", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCelular.Focus();
                    return;
                }
                if (txtEmail.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Email", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Focus();
                    return;
                }

                if (!Met_Proveedores.Existe(txtEmail.Text) == false)
                {
                    Pro_Proveedores.Nombre = txtNombre.Text;
                    Pro_Proveedores.Apellido = txtApellido.Text;
                    Pro_Proveedores.Razon_Social = txtRazon_Social.Text;
                    Pro_Proveedores.Direccion = txtDireccion.Text;
                    Pro_Proveedores.TelefonoOficina = txtTelefonoOficina.Text;
                    Pro_Proveedores.Celular = txtCelular.Text;
                    Pro_Proveedores.Email = txtEmail.Text;
                    Met_Proveedores.Modificar(Pro_Proveedores);
                    MessageBox.Show("Datos Modificados Correctamente", "Datos Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Proveedores.CargarProveedores();
                    limpiar();
                }
                else
                {
                    Pro_Proveedores.Nombre = txtNombre.Text;
                    Pro_Proveedores.Apellido = txtApellido.Text;
                    Pro_Proveedores.Razon_Social = txtRazon_Social.Text;
                    Pro_Proveedores.Direccion = txtDireccion.Text;
                    Pro_Proveedores.TelefonoOficina = txtTelefonoOficina.Text;
                    Pro_Proveedores.Celular = txtCelular.Text;
                    Pro_Proveedores.Email = txtEmail.Text;
                    Met_Proveedores.Agregar(Pro_Proveedores);
                    MessageBox.Show("Datos Guardados Correctamente", "Datos Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Proveedores.CargarProveedores();
                    limpiar();
                }
            }
            catch
            {
                MessageBox.Show("No se pudieron Guardar lo datos", "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lblEliminar_Proveedor_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea eliminar este Proveedor??", "Esta seguro?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int resultado = Met_Proveedores.Eliminar(txtEmail.Text);
                if (resultado > 0)
                {
                    MessageBox.Show("Proveedor Eliminado Correctamente", "Proveedor Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Proveedores.CargarProveedores();
                    limpiar();
                }

                else
                {
                    MessageBox.Show("No se pudo Eliminar el Proveedor", "Ocurrio un error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void txtRazon_Social_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtTelefonoOficina_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
            Validaciones.Numeros(e);
        }

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
            Validaciones.Numeros(e);
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtRazon_Buscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtNombre_Buscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }
        
    }
}
