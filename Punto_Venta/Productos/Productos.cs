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
    public partial class Productos : Form
    {
        public Productos()
        {
            InitializeComponent();
        }

        void limpiar()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            cmbTipo.Text = "Tipo Unidad";
            txtCodigo_Buscar.Focus();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu Open = new Menu();
            Open.ShowDialog();
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = Met_Productos.CargarProductos();
            txtCodigo_Buscar.Focus();
        }

        private void btnGuardar_Producto_Click(object sender, EventArgs e)
        {
            try
            {
                Pro_Productos Pro_Productos = new Pro_Productos();
                if (txtCodigo.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Codigo", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCodigo.Focus();
                    return;
                }
                if (txtNombre.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Nombre", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNombre.Focus();
                    return;
                }
                if (txtDescripcion.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Descripcion", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDescripcion.Focus();
                    return;
                }
                if (txtPrecio.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Precio", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrecio.Focus();
                    return;
                }
                if (txtStock.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Stock", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtStock.Focus();
                    return;
                }
                if (cmbTipo.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Tipo", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbTipo.Focus();
                    return;
                }

                if (!Met_Productos.Existe(Convert.ToInt64(txtCodigo.Text)) == false)
                {
                    Pro_Productos.Codigo = Convert.ToInt64(txtCodigo.Text);
                    Pro_Productos.Nombre = txtNombre.Text;
                    Pro_Productos.Descripcion = txtDescripcion.Text;
                    Pro_Productos.Precio = Convert.ToDouble(txtPrecio.Text);
                    Pro_Productos.Stock = Convert.ToInt32(txtStock.Text);
                    Pro_Productos.TipoUnidad = cmbTipo.Text;
                    Met_Productos.Modificar(Pro_Productos);
                    MessageBox.Show("Datos Modificados Correctamente", "Datos Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Productos.CargarProductos();
                    limpiar();
                }
                else
                {
                    Pro_Productos.Codigo = Convert.ToInt64(txtCodigo.Text);
                    Pro_Productos.Nombre = txtNombre.Text;
                    Pro_Productos.Descripcion = txtDescripcion.Text;
                    Pro_Productos.Precio = Convert.ToDouble(txtPrecio.Text);
                    Pro_Productos.Stock = Convert.ToInt32(txtStock.Text);
                    Pro_Productos.TipoUnidad = cmbTipo.Text;
                    Met_Productos.Agregar(Pro_Productos);
                    MessageBox.Show("Datos Guardados Correctamente", "Datos Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Productos.CargarProductos();
                    limpiar();
                }
            }
            catch
            {
                MessageBox.Show("No se pudieron Guardar lo datos", "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEliminar_Producto_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea eliminar este producto??", "Esta seguro?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int resultado = Met_Productos.Eliminar(Convert.ToInt64(txtCodigo.Text));
                if (resultado > 0)
                {
                    MessageBox.Show("Producto Eliminado Correctamente", "Producto Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Productos.CargarProductos();
                    limpiar();
                }

                else
                {
                    MessageBox.Show("No se pudo Eliminar el Producto", "Ocurrio un error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void txtCodigo_Buscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = Met_Productos.BuscarProductos_Codigo(Convert.ToInt64(txtCodigo_Buscar.Text));
            }
            catch (Exception)
            {
                dataGridView1.DataSource = Met_Productos.CargarProductos();
            }
        }

        private void txtNombre_Buscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = Met_Productos.BuscarProductos_Nombre(txtNombre_Buscar.Text);
            }
            catch (Exception)
            {
                dataGridView1.DataSource = Met_Productos.CargarProductos();
            }
        }

        private void txtCodigo_Buscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
            Validaciones.Numeros(e);
        }
                      
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCodigo.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[0].Value);
            txtNombre.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[1].Value);
            txtDescripcion.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[2].Value);
            txtPrecio.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[3].Value);
            txtStock.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[4].Value);
            cmbTipo.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[5].Value);
        }

        //Labels
        private void lblGuardar_Producto_Click(object sender, EventArgs e)
        {
            try
            {
                Pro_Productos Pro_Productos = new Pro_Productos();
                if (txtCodigo.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Codigo", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCodigo.Focus();
                    return;
                }
                if (txtNombre.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Nombre", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNombre.Focus();
                    return;
                }
                if (txtDescripcion.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Descripcion", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDescripcion.Focus();
                    return;
                }
                if (txtPrecio.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Precio", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrecio.Focus();
                    return;
                }
                if (txtStock.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Stock", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtStock.Focus();
                    return;
                }
                if (cmbTipo.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo Tipo", "Campos Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbTipo.Focus();
                    return;
                }

                if (!Met_Productos.Existe(Convert.ToInt32(txtCodigo.Text)) == false)
                {
                    Pro_Productos.Codigo = Convert.ToInt32(txtCodigo.Text);
                    Pro_Productos.Nombre = txtNombre.Text;
                    Pro_Productos.Descripcion = txtDescripcion.Text;
                    Pro_Productos.Precio = Convert.ToDouble(txtPrecio.Text);
                    Pro_Productos.Stock = Convert.ToInt32(txtStock.Text);
                    Pro_Productos.TipoUnidad = cmbTipo.Text;
                    Met_Productos.Modificar(Pro_Productos);
                    MessageBox.Show("Datos Modificados Correctamente", "Datos Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Productos.CargarProductos();
                    limpiar();
                }
                else
                {
                    Pro_Productos.Codigo = Convert.ToInt32(txtCodigo.Text);
                    Pro_Productos.Nombre = txtNombre.Text;
                    Pro_Productos.Descripcion = txtDescripcion.Text;
                    Pro_Productos.Precio = Convert.ToDouble(txtPrecio.Text);
                    Pro_Productos.Stock = Convert.ToInt32(txtStock.Text);
                    Pro_Productos.TipoUnidad = cmbTipo.Text;
                    Met_Productos.Agregar(Pro_Productos);
                    MessageBox.Show("Datos Guardados Correctamente", "Datos Guardados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Productos.CargarProductos();
                    limpiar();
                }
            }
            catch
            {
                MessageBox.Show("No se pudieron Guardar lo datos", "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lblEliminar_Producto_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea eliminar este producto??", "Esta seguro?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int resultado = Met_Productos.Eliminar(Convert.ToInt64(txtCodigo.Text));
                if (resultado > 0)
                {
                    MessageBox.Show("Producto Eliminado Correctamente", "Producto Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Met_Productos.CargarProductos();
                    limpiar();
                }

                else
                {
                    MessageBox.Show("No se pudo Eliminar el Producto", "Ocurrio un error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void txtNombre_Buscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
            Validaciones.Numeros(e);
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
            Validaciones.Numeros(e);
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
            Validaciones.Numeros(e);
        }     
                     
    }
}
