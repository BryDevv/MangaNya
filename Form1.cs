using System.Data;

using System.Drawing;
using System.Drawing.Printing;

namespace MangaNya
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Datos.CargarDatos();
            ActualizarTablaProductos();

            // Wire up events
            button4.Click += button4_Click;
            button5.Click += button5_Click;
            button6.Click += button6_Click;
            DataP_admin.CellClick += DataP_admin_CellClick;
        }


        private void ActualizarTablaProductos()
        {

            DataP.DataSource = null;
            DataP_admin.DataSource = null;

            DataP.DataSource = Datos.Productos;
            DataP_admin.DataSource = Datos.Productos;

            // Hide internal columns
            if (DataP.Columns.Contains("precioCompra")) DataP.Columns["precioCompra"].Visible = false;
            if (DataP.Columns.Contains("informacionExtra")) DataP.Columns["informacionExtra"].Visible = false;
            if (DataP.Columns.Contains("unidadesVendidas")) DataP.Columns["unidadesVendidas"].Visible = false;

            if (DataP_admin.Columns.Contains("precioCompra")) DataP_admin.Columns["precioCompra"].Visible = false;
            if (DataP_admin.Columns.Contains("informacionExtra")) DataP_admin.Columns["informacionExtra"].Visible = false;
            if (DataP_admin.Columns.Contains("unidadesVendidas")) DataP_admin.Columns["unidadesVendidas"].Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Producto nuevo = new Producto
            {
                nombre = textBox1.Text,
                codigo = textBox2.Text,
                marca = textBox3.Text,
                precioVenta = decimal.Parse(textBox4.Text),
                    cantidad = int.Parse(textBox5.Text),
                   
                    precioCompra = 0 
                };

                Datos.AgregarNuevoProducto(nuevo);
                ActualizarTablaProductos();
                LimpiarCampos();
                MessageBox.Show("Producto agregado correctamente.");
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string codigo = textBox2.Text;
                Producto modificado = new Producto
                {
                    nombre = textBox1.Text,
                    codigo = codigo,
                    marca = textBox3.Text,
                    precioVenta = decimal.Parse(textBox4.Text),
                    cantidad = int.Parse(textBox5.Text),
                    
                    precioCompra = 0 // Not in UI
                };

                Datos.ModificarProducto(codigo, modificado);
                ActualizarTablaProductos();
                LimpiarCampos();
                MessageBox.Show("Producto modificado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar producto: " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Sort by most purchased
            var productosOrdenados = Datos.Productos.OrderByDescending(p => p.unidadesVendidas).ToList();
            
            DataP_admin.DataSource = null;
            DataP_admin.DataSource = productosOrdenados;

            // Re-hide internal columns after rebinding
            if (DataP_admin.Columns.Contains("precioCompra")) DataP_admin.Columns["precioCompra"].Visible = false;
            if (DataP_admin.Columns.Contains("informacionExtra")) DataP_admin.Columns["informacionExtra"].Visible = false;
            if (DataP_admin.Columns.Contains("unidadesVendidas")) DataP_admin.Columns["unidadesVendidas"].Visible = false;

            MessageBox.Show("Productos ordenados por más vendidos.");
        }

        private void DataP_admin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DataP_admin.Rows[e.RowIndex];
                textBox1.Text = row.Cells["nombre"].Value.ToString();
                textBox2.Text = row.Cells["codigo"].Value.ToString();
                textBox3.Text = row.Cells["marca"].Value.ToString();
                textBox4.Text = row.Cells["precioVenta"].Value.ToString();
                textBox5.Text = row.Cells["cantidad"].Value.ToString();
                
            }
        }

        private void LimpiarCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
           
        }




        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (DataP.CurrentRow != null)
            {

                string codigoSeleccionado = DataP.CurrentRow.Cells["codigo"].Value.ToString();


                Datos.AgregarAlCarrito(codigoSeleccionado);


                Data2.DataSource = null!;
                Data2.DataSource = Datos.CarritoTemporal;
                decimal total = Datos.CalcularSubtotal();
                label9.Text = "Total: Q " + total.ToString("00.0");

            }


        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (Data2.CurrentRow != null)
            {

                DialogResult respuesta = MessageBox.Show("�Est�s seguro de que deseas eliminar este producto del carrito?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    decimal total = Datos.CalcularSubtotal() - (Convert.ToDecimal(Data2.CurrentRow.Cells["precio"].Value) * Convert.ToInt32(Data2.CurrentRow.Cells["cantidad"].Value));
                    string codigoSeleccionado = Data2.CurrentRow.Cells["codigoProducto"].Value.ToString();

                    Datos.EliminarDelCarrito(codigoSeleccionado);
                    label9.Text = "Total: Q " + total.ToString("00.0");

                    Data2.DataSource = null!;
                    Data2.DataSource = Datos.CarritoTemporal;


                }
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            label10.Visible = true;
            label11.Visible = true;
            txtNombre.Visible = true;
            txtNit.Visible = true;
            button8.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // 1. Validaciones
            if (Datos.CarritoTemporal.Count == 0)
            {
                MessageBox.Show("El carrito est� vac�o.", "Atenci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNit.Text) || string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Por favor ingresa el NIT y Nombre del cliente.", "Faltan Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           
            PrintDocument documentoImpresion = new PrintDocument();

           
            documentoImpresion.PrintPage += new PrintPageEventHandler(GenerarDisenoTicket);

           
               
                documentoImpresion.Print();

               
                foreach (DetalleFactura detalle in Datos.CarritoTemporal)
                {
                    Datos.DescontarStock(detalle.codigoProducto, detalle.cantidad);
                }

               
                Datos.CarritoTemporal.Clear();
                Data2.DataSource = null!;
                Data2.DataSource = Datos.CarritoTemporal;
                txtNit.Clear();
                txtNombre.Clear();
                label9.Text = "Total: Q " +("00.0");

                ActualizarTablaProductos();
                label10.Visible = false;
                label11.Visible = false;
                txtNombre.Visible = false;
                txtNit.Visible = false;
                button8.Visible = false;

                
            }
            
        



        private void GenerarDisenoTicket(object sender, PrintPageEventArgs e)
        {
            // Configuramos los tipos de letra (Courier New es ideal para tickets de caja)
            Font fuenteTitulo = new Font("Courier New", 12, FontStyle.Bold);
            Font fuenteNormal = new Font("Courier New", 10, FontStyle.Regular);
            SolidBrush pincel = new SolidBrush(Color.Black);

            // Coordenadas de inicio para imprimir (X = margen izquierdo, Y = margen superior)
            int x = 10;
            int y = 20;

            // Dibujamos el encabezado
            e.Graphics.DrawString("======= TIENDA MANGA NYA =======", fuenteTitulo, pincel, x, y);
            y += 25; // Sumamos a 'y' para bajar al siguiente rengl�n
            e.Graphics.DrawString("Fecha: " + DateTime.Now.ToString(), fuenteNormal, pincel, x, y);
            y += 20;
            e.Graphics.DrawString("NIT: " + txtNit.Text, fuenteNormal, pincel, x, y);
            y += 20;
            e.Graphics.DrawString("Cliente: " + txtNombre.Text, fuenteNormal, pincel, x, y);
            y += 30;

            // Dibujamos los t�tulos de las columnas
            e.Graphics.DrawString("--------------------------------", fuenteNormal, pincel, x, y);
            y += 20;
            e.Graphics.DrawString("CANT  | CODIGO       | PRECIO", fuenteNormal, pincel, x, y);
            y += 20;
            e.Graphics.DrawString("--------------------------------", fuenteNormal, pincel, x, y);
            y += 20;

            // Dibujamos cada producto del carrito
            foreach (DetalleFactura detalle in Datos.CarritoTemporal)
            {
                Producto productoEncontrado = Datos.Productos.Find(p => p.codigo == detalle.codigoProducto);

                if (productoEncontrado != null)
                {
                    // Formateamos la l�nea para que cuadre (PadRight rellena con espacios)
                    string lineaCant = (detalle.cantidad.ToString() + "x").PadRight(6);
                    string lineaCod = detalle.codigoProducto.PadRight(13);
                    string lineaPre = "Q " + productoEncontrado.precioVenta.ToString("0.00");

                    e.Graphics.DrawString($"{lineaCant}| {lineaCod}| {lineaPre}", fuenteNormal, pincel, x, y);
                    y += 20; // Bajamos al siguiente rengl�n para el pr�ximo producto
                }
            }

            // Dibujamos el total y el pie de p�gina
            y += 10;
            e.Graphics.DrawString("--------------------------------", fuenteNormal, pincel, x, y);
            y += 20;
            e.Graphics.DrawString("TOTAL A PAGAR:  Q " + Datos.CalcularSubtotal().ToString("0.00"), fuenteTitulo, pincel, x, y);
            y += 30;
            e.Graphics.DrawString("================================", fuenteNormal, pincel, x, y);
            y += 20;
            e.Graphics.DrawString("    �Gracias por tu compra!", fuenteNormal, pincel, x, y);
        }






    }
}

