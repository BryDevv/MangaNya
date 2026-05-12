using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

namespace MangaNya
{
    public partial class UC_PuntoVenta : UserControl
    {
        private FlowLayoutPanel flowCatalogo;
        private Panel panelCarrito;
        private DataGridView dgvCarrito;
        private Label lblSubtotal;
        private Label lblSubtotalMonto;
        private Button btnCobrar;

        public UC_PuntoVenta()
        {
            InitializeComponent();
            this.Load += (s, e) => CargarCatalogo();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(18, 18, 18);
            this.Font = new Font("Segoe UI", 10);

            // Carrito
            panelCarrito = new Panel { Dock = DockStyle.Right, Width = 350, BackColor = Color.FromArgb(26, 26, 26), Padding = new Padding(15) };
            
            Label lblTitulo = new Label { Text = "CARRITO DE COMPRAS", ForeColor = Color.White, Font = new Font("Segoe UI", 12, FontStyle.Bold), Dock = DockStyle.Top, Height = 40 };
            
            dgvCarrito = new DataGridView
            {
                Dock = DockStyle.Top, Height = 400, BackgroundColor = Color.FromArgb(30, 30, 30), 
                ForeColor = Color.FromArgb(221, 160, 221), BorderStyle = BorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect, AllowUserToAddRows = false,
                RowHeadersVisible = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                EnableHeadersVisualStyles = false, ReadOnly = true
            };
            dgvCarrito.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            dgvCarrito.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCarrito.DefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
            dgvCarrito.Columns.Add("Producto", "Producto");
            dgvCarrito.Columns.Add("Cant", "Cant");
            dgvCarrito.Columns.Add("Precio", "Precio");
            dgvCarrito.Columns.Add("Codigo", "Codigo");
            dgvCarrito.Columns["Codigo"].Visible = false;

            lblSubtotal = new Label { Text = "TOTAL A PAGAR", ForeColor = Color.Gray, Location = new Point(15, 480), AutoSize = true };
            lblSubtotalMonto = new Label { Text = "Q 0.00", ForeColor = Color.FromArgb(221, 160, 221), Font = new Font("Segoe UI", 24, FontStyle.Bold), Location = new Point(15, 505), Size = new Size(320, 50) };
            
            Button btnQuitar = new Button
            {
                Text = "QUITAR SELECCIONADO", Location = new Point(15, 440), Size = new Size(320, 30),
                FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold), Cursor = Cursors.Hand
            };
            btnQuitar.FlatAppearance.BorderSize = 0;
            btnQuitar.Click += BtnQuitar_Click;

            btnCobrar = new Button
            {
                Text = "COBRAR AHORA", Dock = DockStyle.Bottom, Height = 60, FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(221, 160, 221), ForeColor = Color.Black, Font = new Font("Segoe UI", 12, FontStyle.Bold), Cursor = Cursors.Hand
            };
            btnCobrar.FlatAppearance.BorderSize = 0;
            btnCobrar.Click += BtnCobrar_Click;

            panelCarrito.Controls.Add(lblSubtotalMonto);
            panelCarrito.Controls.Add(lblSubtotal);
            panelCarrito.Controls.Add(btnQuitar);
            panelCarrito.Controls.Add(dgvCarrito);
            panelCarrito.Controls.Add(lblTitulo);
            panelCarrito.Controls.Add(btnCobrar);

            // Catálogo
            flowCatalogo = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(20), BackColor = Color.FromArgb(18, 18, 18) };

            this.Controls.Add(flowCatalogo);
            this.Controls.Add(panelCarrito);
        }

        private void CargarCatalogo()
        {
            flowCatalogo.Controls.Clear();
            Datos.CargarDatos();
            foreach (var p in Datos.Productos)
            {
                ProductoCard card = new ProductoCard(p.codigo, p.nombre, p.precioVenta, p.rutaImagen);
                card.OnProductoAgregado += (s, e) => ActualizarCarritoVisual();
                flowCatalogo.Controls.Add(card);
            }
        }

        private void ActualizarCarritoVisual()
        {
            dgvCarrito.Rows.Clear();
            foreach (var detalle in Datos.CarritoTemporal)
            {
                string nombre = "Producto";
                decimal precio = 0;
                foreach (var p in Datos.Productos)
                {
                    if (p.codigo == detalle.codigoProducto) { nombre = p.nombre; precio = p.precioVenta; break; }
                }
                dgvCarrito.Rows.Add(nombre, detalle.cantidad, (precio * detalle.cantidad).ToString("N2"), detalle.codigoProducto);
            }
            lblSubtotalMonto.Text = "Q " + Datos.CalcularSubtotal().ToString("N2");
        }

        private void BtnQuitar_Click(object? sender, EventArgs e)
        {
            if (dgvCarrito.SelectedRows.Count > 0)
            {
                string codigo = dgvCarrito.SelectedRows[0].Cells["Codigo"].Value.ToString();
                Datos.QuitarDelCarrito(codigo);
                ActualizarCarritoVisual();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto del carrito para quitar.");
            }
        }

        private void BtnCobrar_Click(object? sender, EventArgs e)
        {
            if (Datos.CarritoTemporal.Count == 0)
            {
                MessageBox.Show("El carrito está vacío.", "MangaNya");
                return;
            }

            DialogResult res = MessageBox.Show("¿Confirmar venta?", "MangaNya", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                // Finalizar venta y obtener factura
                Factura f = Datos.FinalizarVenta();
                
                // Generar texto de la factura
                string textoFactura = ImpresoraFactura.GenerarTextoFactura(f);
                
                // Mostrar "Vista Previa" o simular impresión
                MessageBox.Show(textoFactura, "FACTURA GENERADA - MANGANYA", MessageBoxButtons.OK);
                
                // Refrescar UI
                ActualizarCarritoVisual();
                CargarCatalogo();
            }
        }
    }
}
