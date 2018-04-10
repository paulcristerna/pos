using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace Punto_Venta
{
    public partial class Reportes_Ventas : Form
    {
        public Reportes_Ventas()
        {
            InitializeComponent();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu Open = new Menu();
            Open.ShowDialog();
        }

        private void Reportes_Ventas_Load(object sender, EventArgs e)
        {
            tssUsuarioVenta.Text = "Usuario: " + Form1.user;
            dataGridView1.DataSource = Reporte.GenerarReporte("Todo");
            dataGridView2.DataSource = Reporte.GenerarReporteDetalle();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Reporte.GenerarReporte(comboBox1.Text);
        }

        private void btnRegistrar_Producto_Click(object sender, EventArgs e)
        {
            string Nombre = "Reporte";
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(@"C:\PDFs\"+ Nombre +" "+ comboBox1.Text  +" "+  DateTime.Now.ToString("dd-MM-yyyy") +".pdf", FileMode.Create));

            // Le colocamos el título y el autor
            // **Nota: Esto no será visible en el documento
            doc.AddTitle("Mi primer PDF");
            doc.AddCreator("Hector Inzunza");

            // Abrimos el archivo
            doc.Open();

            //Linea Factura
            PdfContentByte Factura = writer.DirectContent;
            Factura.BeginText();
            BaseFont f_cn = BaseFont.CreateFont("c:\\windows\\fonts\\arial.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Factura.SetFontAndSize(f_cn, 10);
            Factura.SetTextMatrix(500, 750);  //(xPos, yPos)
            Factura.ShowText("Factura");
            Factura.EndText();

            //Codigo
            PdfContentByte Texto = writer.DirectContent;
            Texto.BeginText();
            BaseFont f_cn2 = BaseFont.CreateFont("c:\\windows\\fonts\\arial.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Texto.SetFontAndSize(f_cn2, 10);
            Texto.SetTextMatrix(500, 730);  //(xPos, yPos)
            Texto.ShowText("Codigo:     0000-1");
            Texto.EndText();

            //Fecha
            PdfContentByte Fecha = writer.DirectContent;
            Fecha.BeginText();
            BaseFont f_cn3 = BaseFont.CreateFont("c:\\windows\\fonts\\arial.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Fecha.SetFontAndSize(f_cn3, 10);
            Fecha.SetTextMatrix(500, 710);  //(xPos, yPos)
            Fecha.ShowText("Fecha:      " + DateTime.Now.ToString("dd-MM-yyyy"));
            Fecha.EndText();

            //Hora
            PdfContentByte Hora = writer.DirectContent;
            Fecha.BeginText();
            BaseFont f_cn4 = BaseFont.CreateFont("c:\\windows\\fonts\\arial.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Fecha.SetFontAndSize(f_cn4, 10);
            Fecha.SetTextMatrix(500, 690);  //(xPos, yPos)
            Fecha.ShowText("Hora:        " + DateTime.Now.ToString("HH:mm:ss"));
            Fecha.EndText();

            Paragraph Titulo = new Paragraph();
            Paragraph Domicilio = new Paragraph();
            Paragraph Domicilio2 = new Paragraph();
            Paragraph RFC = new Paragraph();
            Paragraph Telefonos = new Paragraph();
            Paragraph Descripcion = new Paragraph();

            Titulo.Alignment = Element.TITLE;
            Domicilio.Alignment = Element.ALIGN_CENTER;
            Domicilio2.Alignment = Element.ALIGN_CENTER;
            RFC.Alignment = Element.ALIGN_CENTER;
            Telefonos.Alignment = Element.ALIGN_CENTER;
            Descripcion.Alignment = Element.ALIGN_CENTER;

            Titulo.Font = FontFactory.GetFont("Arial", 16);
            Domicilio.Font = FontFactory.GetFont("Arial", 9);
            Domicilio2.Font = FontFactory.GetFont("Arial", 9);
            RFC.Font = FontFactory.GetFont("Arial", 9);
            Telefonos.Font = FontFactory.GetFont("Arial", 9);
            Descripcion.Font = FontFactory.GetFont("Arial", 9);

            Titulo.Add("Russell Corp, S.A. de C.V.");
            Domicilio.Add("Blvd. Diego Valadéz Ríos No.123, Desarrollo Urbano Tres Ríos");
            Domicilio2.Add("Culiacan, Sinaloa Mexico C.P. 80000");
            RFC.Add("R.F.C.: UICH5807193G3");
            Telefonos.Add("Tel. (667)7-54-06-57     Fax: (667)7-54-06-57    Email: russell_corp@hotmail.com");
            Descripcion.Add("Reporte de ventas Tipo: " + comboBox1.Text);

            doc.Add(Titulo);
            doc.Add(Domicilio);
            doc.Add(Domicilio2);
            doc.Add(RFC);
            doc.Add(Telefonos);

            // Creamos la imagen y le ajustamos el tamaño
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(@"C:\PDFs\RUSSELL ENTERPRISES.PNG");
            //Posicion en el eje carteciano de X y Y
            imagen.SetAbsolutePosition(30, 700);
            imagen.BorderWidth = 0;
            imagen.Alignment = Element.ALIGN_LEFT;
            float percentage = 0.0f;
            percentage = 120 / imagen.Width;
            imagen.ScalePercent(percentage * 100);

            // Insertamos la imagen en el documento
            doc.Add(imagen);

            doc.Add(new Paragraph("\n"));
            doc.Add(Descripcion);
            doc.Add(new Paragraph("\n"));

            PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);
            pdfTable.DefaultCell.Padding = 1;
            pdfTable.WidthPercentage = 50;
            pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfTable.DefaultCell.BorderWidth = 1;

            //Adding Header row
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                cell.BackgroundColor = new iTextSharp.text.Color(240, 240, 240);
                pdfTable.AddCell(cell);
            }

            //Adding DataRow
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {                
                foreach (DataGridViewCell cell in row.Cells)
                {
                    string Valor = Convert.ToString(cell.Value);
                    pdfTable.AddCell(Valor);
                }
            }

            doc.Add(pdfTable);
            doc.Close();
            writer.Close();

            MessageBox.Show("¡PDF creado!");

            /*
            //Exporting to PDF
            string folderPath = "C:\\PDFs\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            */
            /*
            using (FileStream stream = new FileStream(folderPath + "ReporteVentas.pdf", FileMode.Append))
            {
                Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.AddTitle("Reporte de Ventas");
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();
                stream.Close();
                MessageBox.Show("PDF Creado con exito.");
            }
            */ 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = DateTime.Now.ToShortDateString().ToString() + " - " + DateTime.Now.ToLongTimeString().ToString(); 
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void lblPDF_Click(object sender, EventArgs e)
        {
            string Nombre = "Reporte";
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(@"C:\PDFs\" + Nombre + " " + comboBox1.Text + " " + DateTime.Now.ToString("dd-MM-yyyy") + ".pdf", FileMode.Create));

            // Le colocamos el título y el autor
            // **Nota: Esto no será visible en el documento
            doc.AddTitle("Mi primer PDF");
            doc.AddCreator("Hector Inzunza");

            // Abrimos el archivo
            doc.Open();

            //Linea Factura
            PdfContentByte Factura = writer.DirectContent;
            Factura.BeginText();
            BaseFont f_cn = BaseFont.CreateFont("c:\\windows\\fonts\\arial.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Factura.SetFontAndSize(f_cn, 10);
            Factura.SetTextMatrix(500, 750);  //(xPos, yPos)
            Factura.ShowText("Factura");
            Factura.EndText();

            //Codigo
            PdfContentByte Texto = writer.DirectContent;
            Texto.BeginText();
            BaseFont f_cn2 = BaseFont.CreateFont("c:\\windows\\fonts\\arial.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Texto.SetFontAndSize(f_cn2, 10);
            Texto.SetTextMatrix(500, 730);  //(xPos, yPos)
            Texto.ShowText("Codigo:     0000-1");
            Texto.EndText();

            //Fecha
            PdfContentByte Fecha = writer.DirectContent;
            Fecha.BeginText();
            BaseFont f_cn3 = BaseFont.CreateFont("c:\\windows\\fonts\\arial.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Fecha.SetFontAndSize(f_cn3, 10);
            Fecha.SetTextMatrix(500, 710);  //(xPos, yPos)
            Fecha.ShowText("Fecha:      " + DateTime.Now.ToString("dd-MM-yyyy"));
            Fecha.EndText();

            //Hora
            PdfContentByte Hora = writer.DirectContent;
            Fecha.BeginText();
            BaseFont f_cn4 = BaseFont.CreateFont("c:\\windows\\fonts\\arial.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Fecha.SetFontAndSize(f_cn4, 10);
            Fecha.SetTextMatrix(500, 690);  //(xPos, yPos)
            Fecha.ShowText("Hora:        " + DateTime.Now.ToString("HH:mm:ss"));
            Fecha.EndText();

            Paragraph Titulo = new Paragraph();
            Paragraph Domicilio = new Paragraph();
            Paragraph Domicilio2 = new Paragraph();
            Paragraph RFC = new Paragraph();
            Paragraph Telefonos = new Paragraph();
            Paragraph Descripcion = new Paragraph();

            Titulo.Alignment = Element.TITLE;
            Domicilio.Alignment = Element.ALIGN_CENTER;
            Domicilio2.Alignment = Element.ALIGN_CENTER;
            RFC.Alignment = Element.ALIGN_CENTER;
            Telefonos.Alignment = Element.ALIGN_CENTER;
            Descripcion.Alignment = Element.ALIGN_CENTER;

            Titulo.Font = FontFactory.GetFont("Arial", 16);
            Domicilio.Font = FontFactory.GetFont("Arial", 9);
            Domicilio2.Font = FontFactory.GetFont("Arial", 9);
            RFC.Font = FontFactory.GetFont("Arial", 9);
            Telefonos.Font = FontFactory.GetFont("Arial", 9);
            Descripcion.Font = FontFactory.GetFont("Arial", 9);

            Titulo.Add("Russell Corp, S.A. de C.V.");
            Domicilio.Add("Blvd. Diego Valadéz Ríos No.123, Desarrollo Urbano Tres Ríos");
            Domicilio2.Add("Culiacan, Sinaloa Mexico C.P. 80000");
            RFC.Add("R.F.C.: UICH5807193G3");
            Telefonos.Add("Tel. (667)7-54-06-57     Fax: (667)7-54-06-57    Email: russell_corp@hotmail.com");
            Descripcion.Add("Reporte de ventas Tipo: " + comboBox1.Text);

            doc.Add(Titulo);
            doc.Add(Domicilio);
            doc.Add(Domicilio2);
            doc.Add(RFC);
            doc.Add(Telefonos);

            // Creamos la imagen y le ajustamos el tamaño
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(@"C:\PDFs\RUSSELL ENTERPRISES.PNG");
            //Posicion en el eje carteciano de X y Y
            imagen.SetAbsolutePosition(30, 700);
            imagen.BorderWidth = 0;
            imagen.Alignment = Element.ALIGN_LEFT;
            float percentage = 0.0f;
            percentage = 120 / imagen.Width;
            imagen.ScalePercent(percentage * 100);

            // Insertamos la imagen en el documento
            doc.Add(imagen);

            doc.Add(new Paragraph("\n"));
            doc.Add(Descripcion);
            doc.Add(new Paragraph("\n"));

            PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);
            pdfTable.DefaultCell.Padding = 1;
            pdfTable.WidthPercentage = 50;
            pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfTable.DefaultCell.BorderWidth = 1;

            //Adding Header row
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                cell.BackgroundColor = new iTextSharp.text.Color(240, 240, 240);
                pdfTable.AddCell(cell);
            }

            //Adding DataRow
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    string Valor = Convert.ToString(cell.Value);
                    pdfTable.AddCell(Valor);
                }
            }

            doc.Add(pdfTable);
            doc.Close();
            writer.Close();

            MessageBox.Show("¡PDF creado!");
        }
    }
}
