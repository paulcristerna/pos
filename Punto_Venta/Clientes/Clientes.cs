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
    public partial class Clientes : Form
    {
        public Clientes()
        {
            InitializeComponent();
        }

        void limpiar()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtDireccion.Clear();
            txtTelefonoCasa.Clear();
            txtCelular.Clear();
            txtEmail.Clear();
            FechaNacimiento.Value = DateTime.Now;
            txtEmail_Buscar.Focus();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu Open = new Menu();
            Open.ShowDialog();
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = Met_Clientes.CargarClientes();
            txtEmail_Buscar.Focus();
        }

        private void btnGuardar_Cliente_Click(object sender, EventArgs e)
        {
            try
            {
                Pro_Clientes Pro_Clientes = new Pro_Clientes();
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
                if (txtTelefonoCasa.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Telefono Casa", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTelefonoCasa.Focus();
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

                if (!Met_Clientes.Existe(txtEmail.Text) == false)
                {
                    Pro_Clientes.Nombre = txtNombre.Text;
                    Pro_Clientes.Apellido = txtApellido.Text;
                    Pro_Clientes.Direccion = txtDireccion.Text;
                    Pro_Clientes.TelefonoCasa = txtTelefonoCasa.Text;
                    Pro_Clientes.Celular = txtCelular.Text;
                    Pro_Clientes.Email = txtEmail.Text;
                    Pro_Clientes.Fecha_Nacimiento = string.Format("{0:yyyy-MM-dd}", FechaNacimiento.Value);
                    Met_Clientes.Modificar(Pro_Clientes);
                    MessageBox.Show("Datos Modificados Correctamente", "Datos Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Clientes.CargarClientes();
                    limpiar();
                }
                else
                {
                    Pro_Clientes.Nombre = txtNombre.Text;
                    Pro_Clientes.Apellido = txtApellido.Text;
                    Pro_Clientes.Direccion = txtDireccion.Text;
                    Pro_Clientes.TelefonoCasa = txtTelefonoCasa.Text;
                    Pro_Clientes.Celular = txtCelular.Text;
                    Pro_Clientes.Email = txtEmail.Text;
                    Pro_Clientes.Fecha_Nacimiento = string.Format("{0:yyyy-MM-dd}", FechaNacimiento.Value);
                    Met_Clientes.Agregar(Pro_Clientes);
                    MessageBox.Show("Datos Guardados Correctamente", "Datos Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Clientes.CargarClientes();
                    limpiar();
                }
            }
            catch
            {
                MessageBox.Show("No se pudieron Guardar lo datos", "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEliminar_Cliente_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea eliminar el cliente??", "Esta seguro?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int resultado = Met_Clientes.Eliminar(txtEmail.Text);
                if (resultado > 0)
                {
                    MessageBox.Show("Cliente Eliminado Correctamente", "Cliente Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Clientes.CargarClientes();
                    limpiar();
                }

                else
                {
                    MessageBox.Show("No se pudo Eliminar el Cliente", "Ocurrio un error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void txtEmail_Buscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = Met_Clientes.BuscarClientes_Email(txtEmail_Buscar.Text);
            }
            catch (Exception)
            {
                dataGridView1.DataSource = Met_Clientes.CargarClientes();
            }
        }

        private void txtNombre_Buscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = Met_Clientes.BuscarClientes_Nombre(txtNombre_Buscar.Text);
            }
            catch (Exception)
            {
                dataGridView1.DataSource = Met_Clientes.CargarClientes();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNombre.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[1].Value);
            txtApellido.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[2].Value);
            txtDireccion.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[3].Value);
            txtTelefonoCasa.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[4].Value);
            txtCelular.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[5].Value);
            txtEmail.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[6].Value);
            FechaNacimiento.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[7].Value);
        }

        //Labels
        private void lblGuardar_Cliente_Click(object sender, EventArgs e)
        {
            try
            {
                Pro_Clientes Pro_Clientes = new Pro_Clientes();
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
                if (txtTelefonoCasa.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Telefono Casa", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTelefonoCasa.Focus();
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

                if (!Met_Clientes.Existe(txtEmail.Text) == false)
                {
                    Pro_Clientes.Nombre = txtNombre.Text;
                    Pro_Clientes.Apellido = txtApellido.Text;
                    Pro_Clientes.Direccion = txtDireccion.Text;
                    Pro_Clientes.TelefonoCasa = txtTelefonoCasa.Text;
                    Pro_Clientes.Celular = txtCelular.Text;
                    Pro_Clientes.Email = txtEmail.Text;
                    Pro_Clientes.Fecha_Nacimiento = string.Format("{0:yyyy-MM-dd}", FechaNacimiento.Value);
                    Met_Clientes.Modificar(Pro_Clientes);
                    MessageBox.Show("Datos Modificados Correctamente", "Datos Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Clientes.CargarClientes();
                    limpiar();
                }
                else
                {
                    Pro_Clientes.Nombre = txtNombre.Text;
                    Pro_Clientes.Apellido = txtApellido.Text;
                    Pro_Clientes.Direccion = txtDireccion.Text;
                    Pro_Clientes.TelefonoCasa = txtTelefonoCasa.Text;
                    Pro_Clientes.Celular = txtCelular.Text;
                    Pro_Clientes.Email = txtEmail.Text;
                    Pro_Clientes.Fecha_Nacimiento = string.Format("{0:yyyy-MM-dd}", FechaNacimiento.Value);
                    Met_Clientes.Agregar(Pro_Clientes);
                    MessageBox.Show("Datos Guardados Correctamente", "Datos Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Clientes.CargarClientes();
                    limpiar();
                }
            }
            catch
            {
                MessageBox.Show("No se pudieron Guardar lo datos", "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lblEliminar_Cliente_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea eliminar el cliente??", "Esta seguro?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int resultado = Met_Clientes.Eliminar(txtEmail.Text);
                if (resultado > 0)
                {
                    MessageBox.Show("Cliente Eliminado Correctamente", "Cliente Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Clientes.CargarClientes();
                    limpiar();
                }

                else
                {
                    MessageBox.Show("No se pudo Eliminar el Cliente", "Ocurrio un error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void txtTelefonoCasa_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtEmail_Buscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtNombre_Buscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

    }
}
