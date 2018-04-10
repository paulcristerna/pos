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
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace Punto_Venta
{
    public partial class PuntoVenta : Form
    {
        //Servicio de PlaceHolder en textboxs
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32
                SendMessage(
                                IntPtr hWnd,
                                int msg,
                                int wParam,
                                [MarshalAs(UnmanagedType.LPWStr)]string lParam
                            );
        private const int EM_SETCUEBANNER = 0x1501;

        DataTable DTProducto = new DataTable();

        public PuntoVenta()
        {
            InitializeComponent();
            //Aqui se definen los mensajes que apareceran en los textboxs de placeholder
            SendMessage(txtCliente.Handle, EM_SETCUEBANNER, 0, "Publico en general");
            //SendMessage(txtNombre.Handle, EM_SETCUEBANNER, 0, "Ingrese codigo de producto");
            //SendMessage(txtNombre.Handle, EM_SETCUEBANNER, 0, "Busque nombre del producto");
            //txtNombreUsuario.Text = "asdadadad";
        }

        //autocomplete
        long Codigo_Nombre;
        string Nombre_Nombre, Descripcion_Nombre, TipoUnidad_Nombre;
        double PrecioUnitario_Nombre, Cantidad_Nombre, Importe_Nombre;

        private void DatosProductos()
        {
            Met_Productos producto = new Met_Productos();           
            producto.Consultar(DTProducto);
        }

        private void ActualizarAutoCompletes()
        {
            txtNombre.AutoCompleteCustomSource = ListadoProductos();
            txtNombre.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtNombre.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        public AutoCompleteStringCollection ListadoProductos()
        {
            string Dato;
            AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
            //recorrer y cargar los items para el autocompletado
            foreach (DataRow row in DTProducto.Rows)
            {
                Dato = Convert.ToString(row["Nombre"]) + " " + Convert.ToString(row["Descripcion"]);
                coleccion.Add(Dato);
            }

            return coleccion;
        }

        private void btnPagar_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnAgregarPago, "Ingresar pago a la caja");
        }

        private void btnAgregar_Carrito_Click(object sender, EventArgs e)
        {
            Pro_Productos Pro_Productos = new Pro_Productos();

            //Validar si los campos codigo de producto y nombre de producto estan vacios
            if (txtProducto.Text.Length == 0 && txtNombre.Text.Length == 0)
            {
                MessageBox.Show("Error en campo codigo o nombre estan vacios", "Campo Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProducto.Focus();
                return;
            }
            //Validar si el campo codigo de producto es diferencte de vacio y si nombre de producto esta vacio
            else if (txtProducto.Text.Length != 0 && txtNombre.Text.Length == 0)
            {
                long Codigo;
                string Nombre, Descripcion, TipoUnidad;
                double PrecioUnitario, Cantidad, Importe;
                listView1.Items.Clear();

                if (txtProducto.Text.Length != 0)
                {
                    if (!Venta.Existe(Convert.ToInt64(txtProducto.Text)) == true)
                    {
                        MySqlConnection conexion = Conexion.MiConexion();
                        string query = "SELECT * FROM Productos WHERE Codigo ='" + this.txtProducto.Text + "' ";
                        MySqlCommand comando = new MySqlCommand(query, conexion);
                        MySqlDataReader leer = comando.ExecuteReader();

                        if (leer.Read() == true)
                        {
                            Codigo = long.Parse(leer["Codigo"].ToString());
                            Nombre = leer["Nombre"].ToString();
                            Descripcion = leer["Descripcion"].ToString();
                            TipoUnidad = leer["TipoUnidad"].ToString();
                            PrecioUnitario = double.Parse(leer["Precio"].ToString());
                            Cantidad = double.Parse(txtCantidad.Text);
                            Importe = double.Parse(PrecioUnitario.ToString()) * double.Parse(Cantidad.ToString());

                            //Provisional
                            /*
                            txtCodigo1.Text = Convert.ToString(Codigo);
                            txtNombre1.Text = Nombre;
                            txtDescripcion1.Text = Descripcion;
                            txtTipoUnidad1.Text = TipoUnidad;
                            txtPrecio1.Text = Convert.ToString(PrecioUnitario);
                            txtCantidad1.Text = Convert.ToString(Cantidad);
                            txtImporte1.Text = Convert.ToString(Importe);
                            */
                            ///////////////////////////////////////////////////////////////

                            //Guardar productos en tabla temporal
                            Pro_Venta Pro_Venta = new Pro_Venta();
                            Pro_Venta.Codigo = Convert.ToInt64(Codigo);
                            Pro_Venta.Nombre = Nombre;
                            Pro_Venta.Descripcion = Descripcion;
                            Pro_Venta.TipoUnidad = TipoUnidad;
                            Pro_Venta.PrecioUnitario = Convert.ToDouble(PrecioUnitario);
                            Pro_Venta.Cantidad = Convert.ToDouble(Cantidad);
                            Pro_Venta.Importe = Convert.ToDouble(Importe);
                            Venta.AgregarVenta_Proceso(Pro_Venta);
                            //////////////////////////////////////////////////////////////                                             
                        }
                        else
                        {
                            MessageBox.Show("Producto no encontrado");
                        }

                    }
                    else
                    {
                        //MessageBox.Show("Error, datos repetidos");
                        if (txtCantidad.Text == "1")
                        {
                            MySqlConnection conexion = Conexion.MiConexion();
                            string query = "SELECT * FROM Venta_Proceso WHERE Codigo ='" + this.txtProducto.Text + "' ";
                            MySqlCommand comando = new MySqlCommand(query, conexion);
                            MySqlDataReader leer = comando.ExecuteReader();
                            if (leer.Read() == true)
                            {
                                Codigo = long.Parse(leer["Codigo"].ToString());
                                Nombre = leer["Nombre"].ToString();
                                Descripcion = leer["Descripcion"].ToString();
                                TipoUnidad = leer["TipoUnidad"].ToString();
                                PrecioUnitario = double.Parse(leer["PrecioUnitario"].ToString());
                                double Cant = double.Parse(leer["Cantidad"].ToString());
                                double quantity = Cant = Cant + 1;
                                Importe = double.Parse(PrecioUnitario.ToString()) * double.Parse(quantity.ToString());
                                ///////////////////////////////////////////////////////////////////////////
                                //Guardar productos en tabla temporal
                                Pro_Venta Pro_Venta = new Pro_Venta();
                                Pro_Venta.Codigo = Convert.ToInt64(Codigo);
                                Pro_Venta.Nombre = Nombre;
                                Pro_Venta.Descripcion = Descripcion;
                                Pro_Venta.TipoUnidad = TipoUnidad;
                                Pro_Venta.PrecioUnitario = Convert.ToDouble(PrecioUnitario);
                                Pro_Venta.Cantidad = Convert.ToDouble(quantity);
                                Pro_Venta.Importe = Convert.ToDouble(Importe);
                                Venta.Modificar_Venta(Pro_Venta);
                            }
                        }
                        else
                        {
                            //MessageBox.Show("txtCantidad es mayor que 1");
                            MySqlConnection conexion = Conexion.MiConexion();
                            string query = "SELECT * FROM Venta_Proceso WHERE Codigo ='" + this.txtProducto.Text + "' ";
                            MySqlCommand comando = new MySqlCommand(query, conexion);
                            MySqlDataReader leer = comando.ExecuteReader();
                            if (leer.Read() == true)
                            {
                                Codigo = long.Parse(leer["Codigo"].ToString());
                                Nombre = leer["Nombre"].ToString();
                                Descripcion = leer["Descripcion"].ToString();
                                TipoUnidad = leer["TipoUnidad"].ToString();
                                PrecioUnitario = double.Parse(leer["PrecioUnitario"].ToString());
                                double Cant = double.Parse(leer["Cantidad"].ToString());
                                double quantity = Cant + double.Parse(txtCantidad.Text);
                                Importe = double.Parse(PrecioUnitario.ToString()) * double.Parse(quantity.ToString());
                                ///////////////////////////////////////////////////////////////////////////
                                //Guardar productos en tabla temporal
                                Pro_Venta Pro_Venta = new Pro_Venta();
                                Pro_Venta.Codigo = Convert.ToInt64(Codigo);
                                Pro_Venta.Nombre = Nombre;
                                Pro_Venta.Descripcion = Descripcion;
                                Pro_Venta.TipoUnidad = TipoUnidad;
                                Pro_Venta.PrecioUnitario = Convert.ToDouble(PrecioUnitario);
                                Pro_Venta.Cantidad = Convert.ToDouble(quantity);
                                Pro_Venta.Importe = Convert.ToDouble(Importe);
                                Venta.Modificar_Venta(Pro_Venta);
                            }

                        }
                    }
                }
                //Cargar productos de tabla de venta temporal
                MySqlConnection conexion2 = Conexion.MiConexion();
                MySqlDataAdapter Consulta = new MySqlDataAdapter("Select * from Venta_Proceso", conexion2);
                DataTable dt = new DataTable();
                Consulta.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListViewItem List;
                    List = listView1.Items.Add(dt.Rows[i][0].ToString());
                    List.SubItems.Add(dt.Rows[i][1].ToString());
                    List.SubItems.Add(dt.Rows[i][2].ToString());
                    List.SubItems.Add(dt.Rows[i][3].ToString());
                    List.SubItems.Add(dt.Rows[i][4].ToString());
                    List.SubItems.Add(dt.Rows[i][5].ToString());
                    List.SubItems.Add(dt.Rows[i][6].ToString());
                }                
                
                double Total = 0;
                foreach (ListViewItem I in listView1.Items)
                {
                    Total += double.Parse(listView1.Items[I.Index].SubItems[6].Text);
                }
                txtTotal.Text = Total.ToString();
                txtProducto.Clear();
                txtNombre.Text = "";
                txtCantidad.Text = "1";
                txtProducto.Focus();                
            }                           
            else if (txtProducto.Text.Length == 0 && txtNombre.Text.Length != 0)
            {
                long Codigo;
                string Nombre, Descripcion, TipoUnidad;
                double PrecioUnitario, Importe;
                listView1.Items.Clear();
                MySqlConnection conexion3 = Conexion.MiConexion();
                string query2 = "Select * from Productos where concat_ws(' ',Nombre,Descripcion) LIKE '%" + this.txtNombre.Text + "%';";
                MySqlCommand comando2 = new MySqlCommand(query2, conexion3);
                MySqlDataReader leer2 = comando2.ExecuteReader();

                if (leer2.Read() == true)
                {
                    Codigo_Nombre = long.Parse(leer2["Codigo"].ToString());
                }

                if (!Venta.Existe(Convert.ToInt64(Codigo_Nombre)) == false)
                {
                    //MessageBox.Show("Ya existe");
                    if (txtCantidad.Text == "1")
                    {
                        MySqlConnection conexion = Conexion.MiConexion();
                        string query = "SELECT * FROM Venta_Proceso WHERE Codigo ='" + Codigo_Nombre + "' ";
                        MySqlCommand comando = new MySqlCommand(query, conexion);
                        MySqlDataReader leer = comando.ExecuteReader();
                        if (leer.Read() == true)
                        {
                            Codigo = long.Parse(leer["Codigo"].ToString());
                            Nombre = leer["Nombre"].ToString();
                            Descripcion = leer["Descripcion"].ToString();
                            TipoUnidad = leer["TipoUnidad"].ToString();
                            PrecioUnitario = double.Parse(leer["PrecioUnitario"].ToString());
                            double Cant = double.Parse(leer["Cantidad"].ToString());
                            double quantity = Cant = Cant + 1;
                            Importe = double.Parse(PrecioUnitario.ToString()) * double.Parse(quantity.ToString());
                            ///////////////////////////////////////////////////////////////////////////
                            //Guardar productos en tabla temporal
                            Pro_Venta Pro_Venta = new Pro_Venta();
                            Pro_Venta.Codigo = Convert.ToInt64(Codigo);
                            Pro_Venta.Nombre = Nombre;
                            Pro_Venta.Descripcion = Descripcion;
                            Pro_Venta.TipoUnidad = TipoUnidad;
                            Pro_Venta.PrecioUnitario = Convert.ToDouble(PrecioUnitario);
                            Pro_Venta.Cantidad = Convert.ToDouble(quantity);
                            Pro_Venta.Importe = Convert.ToDouble(Importe);
                            Venta.Modificar_Venta(Pro_Venta);
                        }
                    }
                    else
                    {
                        //MessageBox.Show("txtCantidad es mayor que 1");
                        MySqlConnection conexion = Conexion.MiConexion();
                        string query = "SELECT * FROM Venta_Proceso WHERE Codigo ='" + Codigo_Nombre + "' ";
                        MySqlCommand comando = new MySqlCommand(query, conexion);
                        MySqlDataReader leer = comando.ExecuteReader();
                        if (leer.Read() == true)
                        {
                            Codigo = long.Parse(leer["Codigo"].ToString());
                            Nombre = leer["Nombre"].ToString();
                            Descripcion = leer["Descripcion"].ToString();
                            TipoUnidad = leer["TipoUnidad"].ToString();
                            PrecioUnitario = double.Parse(leer["PrecioUnitario"].ToString());
                            double Cant = double.Parse(leer["Cantidad"].ToString());
                            double quantity = Cant + double.Parse(txtCantidad.Text);
                            Importe = double.Parse(PrecioUnitario.ToString()) * double.Parse(quantity.ToString());
                            ///////////////////////////////////////////////////////////////////////////
                            //Guardar productos en tabla temporal
                            Pro_Venta Pro_Venta = new Pro_Venta();
                            Pro_Venta.Codigo = Convert.ToInt64(Codigo);
                            Pro_Venta.Nombre = Nombre;
                            Pro_Venta.Descripcion = Descripcion;
                            Pro_Venta.TipoUnidad = TipoUnidad;
                            Pro_Venta.PrecioUnitario = Convert.ToDouble(PrecioUnitario);
                            Pro_Venta.Cantidad = Convert.ToDouble(quantity);
                            Pro_Venta.Importe = Convert.ToDouble(Importe);
                            Venta.Modificar_Venta(Pro_Venta);
                        }
                    }                    
                }
                else
                {
                    //MessageBox.Show("No existe, bla bla");
                    long Codigo2;
                    string Nombre2, Descripcion2, TipoUnidad2;
                    double PrecioUnitario2, Cantidad2, Importe2;
                    listView1.Items.Clear();
                    MySqlConnection conexion = Conexion.MiConexion();
                    string query = "Select * from Productos where concat_ws(' ',Nombre,Descripcion) LIKE '%" + this.txtNombre.Text + "%';";
                    MySqlCommand comando = new MySqlCommand(query, conexion);
                    MySqlDataReader leer = comando.ExecuteReader();

                    if (leer.Read() == true)
                    {
                        Codigo2 = long.Parse(leer["Codigo"].ToString());
                        Nombre2 = leer["Nombre"].ToString();
                        Descripcion2 = leer["Descripcion"].ToString();
                        TipoUnidad2 = leer["TipoUnidad"].ToString();
                        PrecioUnitario2 = double.Parse(leer["Precio"].ToString());
                        Cantidad2 = double.Parse(txtCantidad.Text);
                        Importe2 = double.Parse(PrecioUnitario2.ToString()) * double.Parse(Cantidad2.ToString());

                        //Provisional
                        /*
                        txtCodigo1.Text = Convert.ToString(Codigo);
                        txtNombre1.Text = Nombre;
                        txtDescripcion1.Text = Descripcion;
                        txtTipoUnidad1.Text = TipoUnidad;
                        txtPrecio1.Text = Convert.ToString(PrecioUnitario);
                        txtCantidad1.Text = Convert.ToString(Cantidad);
                        txtImporte1.Text = Convert.ToString(Importe);
                        */
                        ///////////////////////////////////////////////////////////////

                        //Guardar productos en tabla temporal
                        Pro_Venta Pro_Venta = new Pro_Venta();
                        Pro_Venta.Codigo = Convert.ToInt64(Codigo2);
                        Pro_Venta.Nombre = Nombre2;
                        Pro_Venta.Descripcion = Descripcion2;
                        Pro_Venta.TipoUnidad = TipoUnidad2;
                        Pro_Venta.PrecioUnitario = Convert.ToDouble(PrecioUnitario2);
                        Pro_Venta.Cantidad = Convert.ToDouble(Cantidad2);
                        Pro_Venta.Importe = Convert.ToDouble(Importe2);
                        Venta.AgregarVenta_Proceso(Pro_Venta);
                        //////////////////////////////////////////////////////////////         
                    }
                    else
                    {
                        MessageBox.Show("Producto no encontrado");
                    }
                }
                
                //Cargar productos de tabla de venta temporal
                MySqlConnection conexion2 = Conexion.MiConexion();
                MySqlDataAdapter Consulta = new MySqlDataAdapter("Select * from Venta_Proceso", conexion2);
                DataTable dt = new DataTable();
                Consulta.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListViewItem List;
                    List = listView1.Items.Add(dt.Rows[i][0].ToString());
                    List.SubItems.Add(dt.Rows[i][1].ToString());
                    List.SubItems.Add(dt.Rows[i][2].ToString());
                    List.SubItems.Add(dt.Rows[i][3].ToString());
                    List.SubItems.Add(dt.Rows[i][4].ToString());
                    List.SubItems.Add(dt.Rows[i][5].ToString());
                    List.SubItems.Add(dt.Rows[i][6].ToString());
                }

                double Total = 0;
                foreach (ListViewItem I in listView1.Items)
                {
                    Total += double.Parse(listView1.Items[I.Index].SubItems[6].Text);
                }
                txtTotal.Text = Total.ToString();
                txtProducto.Clear();
                txtNombre.Text = "";
                txtCantidad.Text = "1";
                txtProducto.Focus();

            }
            else
            {
                MessageBox.Show("Error en campo codigo y nombre no deben estar llenos en el mismo formulario", "Campo Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProducto.Focus();
                return;
            }
        }       

        private void PuntoVenta_Load(object sender, EventArgs e)
        {
            tssUsuarioVenta.Text = "Usuario: " + Form1.user;
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            txtProducto.Focus();           

            DatosProductos();
            ActualizarAutoCompletes();
        }

        private void btnAgregarPago_Click(object sender, EventArgs e)
        {
            if (txtPagoCon.Text.Length == 0)
            {
                MessageBox.Show("Error en campo pagon con", "Campo Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPagoCon.Focus();
                return;
            }
            else if (Convert.ToDouble(txtPagoCon.Text) < Convert.ToDouble(txtTotal.Text))
            {
                MessageBox.Show("El campo de pago con debe ser mayor o igual al total", "Error en campo pago con, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPagoCon.Focus();
                return;
            }

            //guardar venta
            Pro_Venta Pro_Venta = new Pro_Venta();
            Pro_Venta.Cliente = txtCliente.Text;
            Pro_Venta.Total = Convert.ToDouble(txtTotal.Text);
            Pro_Venta.PagoCon= Convert.ToDouble(txtPagoCon.Text);
            
            //sacar cambio
            double total = double.Parse(txtPagoCon.Text) - double.Parse(txtTotal.Text);
            txtCambio.Text = total.ToString();
            Pro_Venta.Cambio = Convert.ToDouble(txtCambio.Text);

            int resultado = Venta.AgregarVenta(Pro_Venta);

            if (resultado > 0)
            {
                //MessageBox.Show("Datos Guardados Correctamente", "Datos Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //limpiar();
            }
            else
            {
                MessageBox.Show("No se pudieron Guardar lo datos", "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            //guardar detalle de las ventas
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                Pro_Venta.Codigo = Convert.ToInt64(listView1.Items[i].SubItems[0].Text);
                Pro_Venta.Cantidad = Convert.ToDouble(listView1.Items[i].SubItems[5].Text);
                Pro_Venta.Precio = Convert.ToDouble(listView1.Items[i].SubItems[4].Text);
                Venta.AgregarDetalleVenta(Pro_Venta);

                //Modificar Stock
                MySqlConnection conexion = Conexion.MiConexion();
                string ada = "Select Stock from Productos where Codigo='" + listView1.Items[i].SubItems[0].Text + "';";
                MySqlCommand comando = new MySqlCommand(ada, conexion);
                MySqlDataReader leer = comando.ExecuteReader();
                if (leer.Read() == true)
                {
                    double cantidad = 0;
                    Pro_Productos Pro_Productos = new Pro_Productos();
                    double stock = Convert.ToDouble(leer["Stock"].ToString());
                    cantidad = Convert.ToDouble(listView1.Items[i].SubItems[5].Text);
                    double stock_control = double.Parse(stock.ToString()) - cantidad;
                    Pro_Productos.Codigo = Convert.ToInt64(listView1.Items[i].SubItems[0].Text);
                    Pro_Productos.Stock = Convert.ToDouble(stock_control);
                    Met_Productos.Modificar_Stock(Pro_Productos);
                    
                }
            }

            if (checkBox1.Checked)
            {
                //generar ticket                      

                Ticket ticket = new Ticket();
                //ticket.HeaderImage = "C:\imagen.jpg"; //esta propiedad no es obligatoria
                ticket.AddHeaderLine("Ferreteria y Materiales");
                ticket.AddHeaderLine("Del Rio");
                ticket.AddHeaderLine("Villa Toledo #2719");
                ticket.AddHeaderLine("Culiacan, Sin");
                ticket.AddHeaderLine("RFC: VILA-841113-3R4");
                //ticket.AddSubHeaderLine("Ticket # 1");
                ticket.AddSubHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    //ticket.AddItem(FormPadre.listView1.Items[4].Text, FormPadre.listView1.Items[1].Text, FormPadre.listView1.Items[5].Text);
                    ticket.AddItem(listView1.Items[i].SubItems[5].Text, listView1.Items[i].SubItems[1].Text, listView1.Items[i].SubItems[6].Text);
                }
                //ticket.AddTotal("SubTotal", txtSubTotal.Text);
                //ticket.AddTotal("IVA", txtIVA.Text);
                //Add total utiliza dos variables para poder funcionar
                ticket.AddTotal("Total", txtTotal.Text); //Ponemos un total en blanco que sirve de espacio
                ticket.AddTotal("Pago Con", txtPagoCon.Text);
                ticket.AddTotal("Cambio", txtCambio.Text);
                //ticket.AddHeaderLine("Le atendio", tssUsuarioVenta.Text);
                ticket.AddFooterLine("GRACIAS POR TU VISITA");
                //ticket.PrintTicket("Printer");
                ticket.PrintTicket("EPSON TM-U220 Receipt");
            }
        }       

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu Open = new Menu();
            Open.ShowDialog();
        }

        private void txtProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.Numeros(e);
            if (e.KeyChar == (char)13)
            {
                long Codigo;
                string Nombre, Descripcion, TipoUnidad;
                double PrecioUnitario, Cantidad, Importe;
                listView1.Items.Clear();

                if (txtProducto.Text.Length != 0)
                {
                    if (!Venta.Existe(Convert.ToInt64(txtProducto.Text)) == true)
                    {
                        MySqlConnection conexion = Conexion.MiConexion();
                        string query = "SELECT * FROM Productos WHERE Codigo ='" + this.txtProducto.Text + "' ";
                        MySqlCommand comando = new MySqlCommand(query, conexion);
                        MySqlDataReader leer = comando.ExecuteReader();

                        if (leer.Read() == true)
                        {
                            Codigo = long.Parse(leer["Codigo"].ToString());
                            Nombre = leer["Nombre"].ToString();
                            Descripcion = leer["Descripcion"].ToString();
                            TipoUnidad = leer["TipoUnidad"].ToString();
                            PrecioUnitario = double.Parse(leer["Precio"].ToString());
                            Cantidad = double.Parse(txtCantidad.Text);
                            Importe = double.Parse(PrecioUnitario.ToString()) * double.Parse(Cantidad.ToString());

                            //Provisional
                            /*
                            txtCodigo1.Text = Convert.ToString(Codigo);
                            txtNombre1.Text = Nombre;
                            txtDescripcion1.Text = Descripcion;
                            txtTipoUnidad1.Text = TipoUnidad;
                            txtPrecio1.Text = Convert.ToString(PrecioUnitario);
                            txtCantidad1.Text = Convert.ToString(Cantidad);
                            txtImporte1.Text = Convert.ToString(Importe);
                            */
                            ///////////////////////////////////////////////////////////////

                            //Guardar productos en tabla temporal
                            Pro_Venta Pro_Venta = new Pro_Venta();
                            Pro_Venta.Codigo = Convert.ToInt64(Codigo);
                            Pro_Venta.Nombre = Nombre;
                            Pro_Venta.Descripcion = Descripcion;
                            Pro_Venta.TipoUnidad = TipoUnidad;
                            Pro_Venta.PrecioUnitario = Convert.ToDouble(PrecioUnitario);
                            Pro_Venta.Cantidad = Convert.ToDouble(Cantidad);
                            Pro_Venta.Importe = Convert.ToDouble(Importe);
                            Venta.AgregarVenta_Proceso(Pro_Venta);
                            //////////////////////////////////////////////////////////////                                             
                        }
                        else
                        {
                            MessageBox.Show("Producto no encontrado");
                        }

                    }
                    else
                    {
                        //MessageBox.Show("Error, datos repetidos");
                        if (txtCantidad.Text == "1")
                        {
                            MySqlConnection conexion = Conexion.MiConexion();
                            string query = "SELECT * FROM Venta_Proceso WHERE Codigo ='" + this.txtProducto.Text + "' ";
                            MySqlCommand comando = new MySqlCommand(query, conexion);
                            MySqlDataReader leer = comando.ExecuteReader();
                            if (leer.Read() == true)
                            {
                                Codigo = long.Parse(leer["Codigo"].ToString());
                                Nombre = leer["Nombre"].ToString();
                                Descripcion = leer["Descripcion"].ToString();
                                TipoUnidad = leer["TipoUnidad"].ToString();
                                PrecioUnitario = double.Parse(leer["PrecioUnitario"].ToString());
                                double Cant = double.Parse(leer["Cantidad"].ToString());
                                double quantity = Cant = Cant + 1;
                                Importe = double.Parse(PrecioUnitario.ToString()) * double.Parse(quantity.ToString());
                                ///////////////////////////////////////////////////////////////////////////
                                //Guardar productos en tabla temporal
                                Pro_Venta Pro_Venta = new Pro_Venta();
                                Pro_Venta.Codigo = Convert.ToInt64(Codigo);
                                Pro_Venta.Nombre = Nombre;
                                Pro_Venta.Descripcion = Descripcion;
                                Pro_Venta.TipoUnidad = TipoUnidad;
                                Pro_Venta.PrecioUnitario = Convert.ToDouble(PrecioUnitario);
                                Pro_Venta.Cantidad = Convert.ToDouble(quantity);
                                Pro_Venta.Importe = Convert.ToDouble(Importe);
                                Venta.Modificar_Venta(Pro_Venta);
                            }
                        }
                        else
                        {
                            //MessageBox.Show("txtCantidad es mayor que 1");
                            MySqlConnection conexion = Conexion.MiConexion();
                            string query = "SELECT * FROM Venta_Proceso WHERE Codigo ='" + this.txtProducto.Text + "' ";
                            MySqlCommand comando = new MySqlCommand(query, conexion);
                            MySqlDataReader leer = comando.ExecuteReader();
                            if (leer.Read() == true)
                            {
                                Codigo = long.Parse(leer["Codigo"].ToString());
                                Nombre = leer["Nombre"].ToString();
                                Descripcion = leer["Descripcion"].ToString();
                                TipoUnidad = leer["TipoUnidad"].ToString();
                                PrecioUnitario = double.Parse(leer["PrecioUnitario"].ToString());
                                double Cant = double.Parse(leer["Cantidad"].ToString());
                                double quantity = Cant + double.Parse(txtCantidad.Text);
                                Importe = double.Parse(PrecioUnitario.ToString()) * double.Parse(quantity.ToString());
                                ///////////////////////////////////////////////////////////////////////////
                                //Guardar productos en tabla temporal
                                Pro_Venta Pro_Venta = new Pro_Venta();
                                Pro_Venta.Codigo = Convert.ToInt64(Codigo);
                                Pro_Venta.Nombre = Nombre;
                                Pro_Venta.Descripcion = Descripcion;
                                Pro_Venta.TipoUnidad = TipoUnidad;
                                Pro_Venta.PrecioUnitario = Convert.ToDouble(PrecioUnitario);
                                Pro_Venta.Cantidad = Convert.ToDouble(quantity);
                                Pro_Venta.Importe = Convert.ToDouble(Importe);
                                Venta.Modificar_Venta(Pro_Venta);
                            }
                        }
                    }
                }
                //Cargar productos de tabla de venta temporal
                MySqlConnection conexion2 = Conexion.MiConexion();
                MySqlDataAdapter Consulta = new MySqlDataAdapter("Select * from Venta_Proceso", conexion2);
                DataTable dt = new DataTable();
                Consulta.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListViewItem List;
                    List = listView1.Items.Add(dt.Rows[i][0].ToString());
                    List.SubItems.Add(dt.Rows[i][1].ToString());
                    List.SubItems.Add(dt.Rows[i][2].ToString());
                    List.SubItems.Add(dt.Rows[i][3].ToString());
                    List.SubItems.Add(dt.Rows[i][4].ToString());
                    List.SubItems.Add(dt.Rows[i][5].ToString());
                    List.SubItems.Add(dt.Rows[i][6].ToString());
                }

                double Total = 0;
                foreach (ListViewItem I in listView1.Items)
                {
                    Total += double.Parse(listView1.Items[I.Index].SubItems[6].Text);
                }
                txtTotal.Text = Total.ToString();
                txtProducto.Clear();
                txtNombre.Text = "";
                txtCantidad.Text = "1";
                txtProducto.Focus();
              
            }                       
        }

        private void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            txtTotal.Clear();
            txtPagoCon.Clear();
            txtCambio.Clear();
            txtProducto.Clear();
            txtNombre.Clear();
            txtProducto.Focus();
            Venta.Borrar_Tabla_Venta_Proceso();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = DateTime.Now.ToShortDateString().ToString() + " - " + DateTime.Now.ToLongTimeString().ToString(); 
        }

        private void txtPagoCon_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.Numeros(e);
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
            if (e.KeyChar == (char)13)
            {                                               
                MySqlConnection conexion = Conexion.MiConexion();
                string query = "Select * from Productos where concat_ws(' ',Nombre,Descripcion) LIKE '%" + this.txtNombre.Text + "%';";
                MySqlCommand comando = new MySqlCommand(query, conexion);
                MySqlDataReader leer = comando.ExecuteReader();
                
                if (leer.Read() == true)
                {
                    Codigo_Nombre = long.Parse(leer["Codigo"].ToString());
                    Nombre_Nombre = leer["Nombre"].ToString();
                    Descripcion_Nombre = leer["Descripcion"].ToString();
                    TipoUnidad_Nombre = leer["TipoUnidad"].ToString();
                    PrecioUnitario_Nombre = double.Parse(leer["Precio"].ToString());
                    Cantidad_Nombre = double.Parse(txtCantidad.Text);
                    Importe_Nombre = double.Parse(PrecioUnitario.ToString()) * double.Parse(Cantidad.ToString());                                                       
                }                              
            }
        }

        private void lblNuevaVenta_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            txtTotal.Clear();
            txtPagoCon.Clear();
            txtCambio.Clear();
            txtProducto.Clear();
            txtNombre.Clear();
            txtProducto.Focus();
            Venta.Borrar_Tabla_Venta_Proceso();
        }

        private void lblAtras_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu Open = new Menu();
            Open.ShowDialog();
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.Numeros(e);
        }
      
        private void PuntoVenta_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.D))
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    ListViewItem item = listView1.SelectedItems[0];
                    int resultado = Venta.Quitar_Producto_Venta_Proceso(Convert.ToInt64(listView1.SelectedItems[0].Text));
                    if (resultado > 0)
                    {
                        MessageBox.Show("Producto Eliminado Correctamente", "Producto Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        double resra = Convert.ToDouble(txtTotal.Text);
                        resra = resra - Convert.ToDouble(item.SubItems[6].Text);                      
                        txtTotal.Text = resra.ToString();
                        listView1.Items.Clear();
                        //Cargar productos de tabla de venta temporal
                        MySqlConnection conexion2 = Conexion.MiConexion();
                        MySqlDataAdapter Consulta = new MySqlDataAdapter("Select * from Venta_Proceso", conexion2);
                        DataTable dt = new DataTable();
                        Consulta.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ListViewItem List;
                            List = listView1.Items.Add(dt.Rows[i][0].ToString());
                            List.SubItems.Add(dt.Rows[i][1].ToString());
                            List.SubItems.Add(dt.Rows[i][2].ToString());
                            List.SubItems.Add(dt.Rows[i][3].ToString());
                            List.SubItems.Add(dt.Rows[i][4].ToString());
                            List.SubItems.Add(dt.Rows[i][5].ToString());
                            List.SubItems.Add(dt.Rows[i][6].ToString());
                        }
                        txtProducto.Focus();                  
                    }

                    else
                    {
                        MessageBox.Show("No se pudo Eliminar el Producto", "Ocurrio un error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    
                }
                else
                    MessageBox.Show("Error al retirar el producto, verifique que haya seleccionado un producto");
            }
            if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Add))
            {
                if (txtPagoCon.Text.Length == 0)
                {
                    MessageBox.Show("Error en campo pagon con", "Campo Obligatorio, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPagoCon.Focus();
                    return;
                }
                else if (Convert.ToDouble(txtPagoCon.Text) < Convert.ToDouble(txtTotal.Text))
                {
                    MessageBox.Show("El campo de pago con debe ser mayor o igual al total", "Error en campo pago con, verifique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPagoCon.Focus();
                    return;
                }

                //guardar venta
                Pro_Venta Pro_Venta = new Pro_Venta();
                Pro_Venta.Cliente = txtCliente.Text;
                Pro_Venta.Total = Convert.ToDouble(txtTotal.Text);
                Pro_Venta.PagoCon = Convert.ToDouble(txtPagoCon.Text);

                //sacar cambio
                double total = double.Parse(txtPagoCon.Text) - double.Parse(txtTotal.Text);
                txtCambio.Text = total.ToString();
                Pro_Venta.Cambio = Convert.ToDouble(txtCambio.Text);

                int resultado = Venta.AgregarVenta(Pro_Venta);

                if (resultado > 0)
                {
                    //MessageBox.Show("Datos Guardados Correctamente", "Datos Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //limpiar();
                }
                else
                {
                    MessageBox.Show("No se pudieron Guardar lo datos", "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                //guardar detalle de las ventas
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    Pro_Venta.Codigo = Convert.ToInt64(listView1.Items[i].SubItems[0].Text);
                    Pro_Venta.Cantidad = Convert.ToDouble(listView1.Items[i].SubItems[5].Text);
                    Pro_Venta.Precio = Convert.ToDouble(listView1.Items[i].SubItems[4].Text);
                    Venta.AgregarDetalleVenta(Pro_Venta);
                }

                if (checkBox1.Checked)
                {
                    //generar ticket                      

                    Ticket ticket = new Ticket();
                    //ticket.HeaderImage = "C:\imagen.jpg"; //esta propiedad no es obligatoria
                    ticket.AddHeaderLine("Ferreteria y Materiales");
                    ticket.AddHeaderLine("Del Rio");
                    ticket.AddHeaderLine("Villa Toledo #2719");
                    ticket.AddHeaderLine("Culiacan, Sin");
                    ticket.AddHeaderLine("RFC: VILA-841113-3R4");
                    //ticket.AddSubHeaderLine("Ticket # 1");
                    ticket.AddSubHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        //ticket.AddItem(FormPadre.listView1.Items[4].Text, FormPadre.listView1.Items[1].Text, FormPadre.listView1.Items[5].Text);
                        ticket.AddItem(listView1.Items[i].SubItems[5].Text, listView1.Items[i].SubItems[1].Text, listView1.Items[i].SubItems[6].Text);
                    }
                    //ticket.AddTotal("SubTotal", txtSubTotal.Text);
                    //ticket.AddTotal("IVA", txtIVA.Text);
                    //Add total utiliza dos variables para poder funcionar
                    ticket.AddTotal("Total", txtTotal.Text); //Ponemos un total en blanco que sirve de espacio
                    ticket.AddTotal("Pago Con", txtPagoCon.Text);
                    ticket.AddTotal("Cambio", txtCambio.Text);
                    //ticket.AddHeaderLine("Le atendio", tssUsuarioVenta.Text);
                    ticket.AddFooterLine("GRACIAS POR TU VISITA");
                    //ticket.PrintTicket("Printer");
                    ticket.PrintTicket("EPSON TM-U220 Receipt");
                }
            }
        }
                                           
        /*
        Metodo para comprobar si existe un dato en un listview busca en todos los items
        private void button1_Click(object sender, EventArgs e)
        {
            ListViewItem item = listView1.FindItemWithText(txtProducto.Text);
            if (item != null)
            {
                // it exists
                MessageBox.Show("Existe");
            }
            else
            {
                // doesn't exist
                MessageBox.Show("No existe");
            }
        }
        */
        /*
            Metodo para extraer datos de una tabla y mostrarlos en listview
            MySqlConnection conexion = Conexion.MiConexion();
            MySqlDataAdapter Consulta = new MySqlDataAdapter("Select * from Venta_Proceso", conexion);
            DataTable dt = new DataTable();
            Consulta.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListViewItem List;
                List = listView1.Items.Add(dt.Rows[i][0].ToString());
                List.SubItems.Add(dt.Rows[i][1].ToString());                
            }
        */
        /*
            Recorrer el listview 1
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (txtProducto.Text == listView1.Items[i].SubItems[0].Text)
                {
                    MessageBox.Show("Producto existe");
                }
                else
                {
                    MessageBox.Show("Producto No existe");
                }
            }
        */
        /*
            Recorrer el listview 2
            for (int i = 0; i <= listView1.Items.Count; i++)
            {
                  if (Convert.ToInt64(txtProducto.Text) == Convert.ToInt64(listView1.Items[i].SubItems[0].Text))
                  {
                       MessageBox.Show("Producto existe");
                  }
                     else
                     {
                          MessageBox.Show("Producto No existe");
                          return;
                     }                                                                              
            }
        */
        /*
            Pro_Venta Pro_Venta = new Pro_Venta();
            Venta.Cargar_Lista();
            //textBox1.Text = (Pro_Venta.Nombre.ToString());
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                //Venta.Cargar_Lista;
                ListViewItem List;
                //listView1.Items[i].SubItems[0].Text = (Pro_Venta.Nombre.ToString());
                
                List = listView1.Items.Add(Pro_Venta.Codigo.ToString());
                List.SubItems.Add(Pro_Venta.Nombre.ToString());
                List.SubItems.Add(Pro_Venta.Descripcion.ToString());
                List.SubItems.Add(Pro_Venta.TipoUnidad.ToString());
                List.SubItems.Add(Pro_Venta.PrecioUnitario.ToString());
                List.SubItems.Add(Pro_Venta.Cantidad.ToString());
                List.SubItems.Add(Pro_Venta.Importe.ToString());
                
                //List = listView1.Items.Add(dt.Rows[i][0].ToString());
                //List.SubItems.Add(dt.Rows[i][1].ToString());
                //List.SubItems.Add(dt.Rows[i][2].ToString());
                //List.SubItems.Add(dt.Rows[i][3].ToString());
                //List.SubItems.Add(dt.Rows[i][4].ToString());
                //List.SubItems.Add(dt.Rows[i][5].ToString());
                //List.SubItems.Add(dt.Rows[i][6].ToString());
                
            }
            */
    }
}
