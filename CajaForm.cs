using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MangaNya
{
    public class CajaForm : Form
    {
        private FlowLayoutPanel flowCatalogo;
        private Panel panelCarrito;
        private DataGridView dgvCarrito;
        private Label lblSubtotal;
        private Label lblSubtotalMonto;
        private Button btnCobrar;

        public CajaForm()
        {
            InitializeComponent();

            // 1. Cargamos los datos guardados en el disco duro
            Datos.CargarDatos();

            // 2. TRUCO DE PRUEBA: Si no hay productos en el txt, agregamos dos falsos a la memoria 
            // solo para comprobar que el diseño visual funciona correctamente.
            if (Datos.Productos.Count == 0)
            {
                Datos.Productos.Add(new Producto
                {
                    codigo = "M001",
                    nombre = "Manga Naruto Vol. 1",
                    precioVenta = 75.50m,
                    rutaImagen = "M001.jpg" // Asegúrate de tener una imagen M001.jpg en tu carpeta Imagenes
                });

                Datos.Productos.Add(new Producto
                {
                    codigo = "F001",
                    nombre = "Figura Funko Pop",
                    precioVenta = 150.00m,
                    rutaImagen = "F001.jpg"
                });
            }

            // 3. Ahora sí, le decimos a la IA que dibuje las tarjetas
            CargarCatalogo();

            // 4. Actualizamos el carrito para que el total empiece en Q 0.00
            ActualizarCarritoVisual();
        }

        private void InitializeComponent()
        {
            // Formulario Principal
            this.Text = "MangaNya - Punto de Venta";
            this.Size = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(18, 18, 18); // #121212
            this.Font = new Font("Segoe UI", 10);

            // 1. FlowLayoutPanel (Catálogo)
            flowCatalogo = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(18, 18, 18)
            };

            // 2. Panel Derecho (Carrito)
            panelCarrito = new Panel
            {
                Dock = DockStyle.Right,
                Width = 350,
                BackColor = Color.FromArgb(26, 26, 26), // #1A1A1A
                Padding = new Padding(15)
            };

            Label lblTituloCarrito = new Label
            {
                Text = "CARRITO DE COMPRAS",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // DataGridView Minimalista
            dgvCarrito = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 400,
                BackgroundColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.FromArgb(221, 160, 221),
                BorderStyle = BorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                GridColor = Color.FromArgb(45, 45, 45),
                ColumnHeadersVisible = true,
                ReadOnly = true
            };

            dgvCarrito.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            dgvCarrito.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCarrito.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvCarrito.EnableHeadersVisualStyles = false;
            dgvCarrito.DefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
            dgvCarrito.DefaultCellStyle.SelectionBackColor = Color.FromArgb(221, 160, 221);
            dgvCarrito.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Configurar columnas del DGV
            dgvCarrito.Columns.Add("Producto", "Producto");
            dgvCarrito.Columns.Add("Cant", "Cant");
            dgvCarrito.Columns.Add("Precio", "Precio");

            // Subtotal
            lblSubtotal = new Label
            {
                Text = "TOTAL A PAGAR",
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 10),
                Location = new Point(15, 480),
                AutoSize = true
            };

            lblSubtotalMonto = new Label
            {
                Text = "Q 0.00",
                ForeColor = Color.FromArgb(221, 160, 221),
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                Location = new Point(15, 505),
                Size = new Size(320, 50),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Botón Cobrar
            btnCobrar = new Button
            {
                Text = "COBRAR AHORA",
                Dock = DockStyle.Bottom,
                Height = 60,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(221, 160, 221),
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCobrar.FlatAppearance.BorderSize = 0;
            btnCobrar.Click += BtnCobrar_Click;

            // Ensamblaje de controles
            panelCarrito.Controls.Add(lblSubtotalMonto);
            panelCarrito.Controls.Add(lblSubtotal);
            panelCarrito.Controls.Add(dgvCarrito);
            panelCarrito.Controls.Add(lblTituloCarrito);
            panelCarrito.Controls.Add(btnCobrar);

            this.Controls.Add(flowCatalogo);
            this.Controls.Add(panelCarrito);
        }

        private void CargarCatalogo()
        {
            flowCatalogo.Controls.Clear();
            // Aseguramos que los datos estén cargados
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
                // Buscar nombre y precio para el DGV
                string nombre = "Producto";
                decimal precio = 0;
                foreach (var p in Datos.Productos)
                {
                    if (p.codigo == detalle.codigoProducto)
                    {
                        nombre = p.nombre;
                        precio = p.precioVenta;
                        break;
                    }
                }
                
                dgvCarrito.Rows.Add(nombre, detalle.cantidad, (precio * detalle.cantidad).ToString("N2"));
            }

            lblSubtotalMonto.Text = "Q " + Datos.CalcularSubtotal().ToString("N2");
        }

        private void BtnCobrar_Click(object sender, EventArgs e)
        {
            if (Datos.CarritoTemporal.Count == 0)
            {
                MessageBox.Show("El carrito está vacío.", "MangaNya", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal total = Datos.CalcularSubtotal();
            DialogResult result = MessageBox.Show($"¿Confirmar cobro de Q {total:N2}?", "Finalizar Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Datos.FinalizarVenta();
                ActualizarCarritoVisual();
                CargarCatalogo(); // Recargar por si el stock cambió
                MessageBox.Show("Venta realizada con éxito.", "MangaNya", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
