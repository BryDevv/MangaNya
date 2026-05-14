using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

namespace MangaNya
{
    public partial class UC_Gerente : UserControl
    {
        // Colores del Sistema
        private Color colorFondo = Color.FromArgb(18, 18, 18);
        private Color colorPanel = Color.FromArgb(28, 28, 28);
        private Color colorAcento = Color.FromArgb(187, 134, 252); // Morado suave
        private Color colorTexto = Color.FromArgb(240, 240, 240);
        private Color colorTextoSecundario = Color.FromArgb(180, 180, 180);

        private Panel pnlContenido;
        private Panel pnlSidebar;
        private List<Button> botonesMenu = new List<Button>();

        // Controles de Inventario
        private DataGridView dgvInventario;
        private TextBox txtCodigo, txtNombre, txtMarca, txtPrecioC, txtPrecioV, txtStock, txtImagen;

        // Controles de Reportes
        private DataGridView dgvMasVendidos;
        private DateTimePicker dtpInicio, dtpFin;
        private Label lblTotalVentas, lblGanancia;

        // Controles de Entregas
        private DataGridView dgvEntregas;

        public UC_Gerente()
        {
            InitializeComponent();
            MostrarPestaña("Inventario"); // Pestaña por defecto
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = colorFondo;

            // --- BARRA LATERAL (Sidebar) ---
            pnlSidebar = new Panel { Dock = DockStyle.Left, Width = 220, BackColor = Color.FromArgb(24, 24, 24) };
            
            Label lblBrand = new Label { 
                Text = "ADMINISTRACIÓN", ForeColor = colorAcento, 
                Font = new Font("Segoe UI", 12, FontStyle.Bold), 
                Dock = DockStyle.Top, Height = 60, TextAlign = ContentAlignment.MiddleCenter 
            };
            pnlSidebar.Controls.Add(lblBrand);

            CrearBotonMenu("Inventario", "📦  Inventario");
            CrearBotonMenu("Reportes", "📊  Reportes");
            CrearBotonMenu("Entregas", "🚚  Entregas");

            Button btnVolver = new Button {
                Text = "↩  Volver al POS", Dock = DockStyle.Bottom, Height = 60,
                FlatStyle = FlatStyle.Flat, ForeColor = Color.LightCoral,
                Font = new Font("Segoe UI", 10, FontStyle.Bold), Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.Click += (s, e) => {
                if (this.ParentForm is MainForm main) main.CambiarVista(new UC_PuntoVenta());
            };
            pnlSidebar.Controls.Add(btnVolver);

            // --- CONTENEDOR PRINCIPAL ---
            pnlContenido = new Panel { Dock = DockStyle.Fill, Padding = new Padding(25) };

            this.Controls.Add(pnlContenido);
            this.Controls.Add(pnlSidebar);
        }

        private void CrearBotonMenu(string id, string texto)
        {
            Button btn = new Button {
                Text = "      " + texto, Dock = DockStyle.Top, Height = 50,
                FlatStyle = FlatStyle.Flat, ForeColor = colorTextoSecundario,
                Font = new Font("Segoe UI", 10), Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleLeft, Tag = id
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(40, 40, 40);
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(35, 35, 35);
            btn.Click += (s, e) => MostrarPestaña(id);
            
            pnlSidebar.Controls.Add(btn);
            botonesMenu.Add(btn);
        }

        private void MostrarPestaña(string id)
        {
            pnlContenido.Controls.Clear();
            foreach (var b in botonesMenu) {
                b.ForeColor = (b.Tag.ToString() == id) ? colorAcento : colorTextoSecundario;
                b.BackColor = (b.Tag.ToString() == id) ? Color.FromArgb(35, 35, 35) : Color.Transparent;
            }

            switch (id) {
                case "Inventario": DibujarInventario(); break;
                case "Reportes": DibujarReportes(); break;
                case "Entregas": DibujarEntregas(); break;
            }
        }

        private void DibujarInventario()
        {
            Panel pnlLeft = new Panel { Dock = DockStyle.Left, Width = 320, BackColor = colorPanel, Padding = new Padding(20) };
            Label lblTit = new Label { Text = "DETALLES DEL PRODUCTO", ForeColor = colorAcento, Font = new Font("Segoe UI", 10, FontStyle.Bold), Dock = DockStyle.Top, Height = 30 };
            pnlLeft.Controls.Add(lblTit);

            txtCodigo = CrearInput("Código del Producto", pnlLeft);
            txtNombre = CrearInput("Nombre", pnlLeft);
            txtMarca = CrearInput("Marca/Serie", pnlLeft);
            txtPrecioC = CrearInput("Precio Costo (Q)", pnlLeft);
            txtPrecioV = CrearInput("Precio Venta (Q)", pnlLeft);
            txtStock = CrearInput("Unidades en Stock", pnlLeft);
            txtImagen = CrearInput("Nombre de Imagen", pnlLeft);

            Panel pnlBtns = new Panel { Dock = DockStyle.Bottom, Height = 130 };
            Button btnGuardar = CrearBotonAccion("GUARDAR NUEVO", colorAcento, Color.Black);
            btnGuardar.Click += BtnGuardar_Click;
            Button btnModificar = CrearBotonAccion("ACTUALIZAR SELECCIONADO", Color.FromArgb(50, 50, 50), colorTexto);
            btnModificar.Click += BtnModificar_Click;
            Button btnEliminar = CrearBotonAccion("ELIMINAR", Color.FromArgb(80, 30, 30), colorTexto);
            btnEliminar.Click += BtnEliminar_Click;

            pnlBtns.Controls.Add(btnEliminar);
            pnlBtns.Controls.Add(btnModificar);
            pnlBtns.Controls.Add(btnGuardar);
            pnlLeft.Controls.Add(pnlBtns);

            dgvInventario = CrearTabla();
            dgvInventario.CellClick += DgvInventario_CellClick;
            dgvInventario.DataSource = Datos.Productos;

            pnlContenido.Controls.Add(dgvInventario);
            pnlContenido.Controls.Add(new Panel { Dock = DockStyle.Left, Width = 25 });
            pnlContenido.Controls.Add(pnlLeft);
        }

        private void DibujarReportes()
        {
            Panel pnlTop = new Panel { Dock = DockStyle.Top, Height = 140, Padding = new Padding(0, 0, 0, 20) };
            
            Panel pnlFiltros = new Panel { Dock = DockStyle.Left, Width = 300, BackColor = colorPanel, Padding = new Padding(15) };
            Label lblRange = new Label { Text = "RANGO DE FECHAS", ForeColor = colorTextoSecundario, Font = new Font("Segoe UI", 8, FontStyle.Bold), Dock = DockStyle.Top, Height = 20 };
            dtpInicio = new DateTimePicker { Dock = DockStyle.Top, Margin = new Padding(0, 5, 0, 5) };
            dtpFin = new DateTimePicker { Dock = DockStyle.Top };
            dtpInicio.ValueChanged += (s, e) => ActualizarReportes();
            dtpFin.ValueChanged += (s, e) => ActualizarReportes();
            
            pnlFiltros.Controls.Add(dtpFin);
            pnlFiltros.Controls.Add(new Label { Dock = DockStyle.Top, Height = 5 });
            pnlFiltros.Controls.Add(dtpInicio);
            pnlFiltros.Controls.Add(lblRange);
            pnlTop.Controls.Add(pnlFiltros);

            FlowLayoutPanel pnlCards = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(20, 0, 0, 0) };
            lblTotalVentas = CrearCard(pnlCards, "TOTAL VENTAS", "Q 0.00", Color.FromArgb(3, 218, 198));
            lblGanancia = CrearCard(pnlCards, "UTILIDAD ESTIMADA", "Q 0.00", Color.FromArgb(187, 134, 252));
            pnlTop.Controls.Add(pnlCards);

            Label lblTable = new Label { Text = "RANKING DE PRODUCTOS MÁS VENDIDOS", ForeColor = colorTexto, Font = new Font("Segoe UI", 11, FontStyle.Bold), Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.BottomLeft };
            dgvMasVendidos = CrearTabla();
            
            pnlContenido.Controls.Add(dgvMasVendidos);
            pnlContenido.Controls.Add(lblTable);
            pnlContenido.Controls.Add(pnlTop);
            ActualizarReportes();
        }

        private void DibujarEntregas()
        {
            Panel pnlInfo = new Panel { Dock = DockStyle.Top, Height = 60 };
            Label lblTit = new Label { Text = "CONTROL DE ENTREGAS PENDIENTES", ForeColor = colorTexto, Font = new Font("Segoe UI", 12, FontStyle.Bold), Dock = DockStyle.Left, AutoSize = true };
            pnlInfo.Controls.Add(lblTit);

            Button btnCheck = new Button { 
                Text = "MARCAR COMO ENTREGADO", Dock = DockStyle.Right, Width = 250, Height = 45,
                BackColor = Color.FromArgb(0, 150, 136), ForeColor = Color.White, FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold), Cursor = Cursors.Hand
            };
            btnCheck.FlatAppearance.BorderSize = 0;
            btnCheck.Click += (s, e) => {
                if (dgvEntregas.SelectedRows.Count > 0) {
                    string num = dgvEntregas.SelectedRows[0].Cells["Numero"].Value.ToString();
                    Datos.MarcarComoEntregada(num);
                    CargarEntregas();
                }
            };
            pnlInfo.Controls.Add(btnCheck);

            dgvEntregas = CrearTabla();
            CargarEntregas();

            pnlContenido.Controls.Add(dgvEntregas);
            pnlContenido.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 20 });
            pnlContenido.Controls.Add(pnlInfo);
        }

        private TextBox CrearInput(string label, Panel p)
        {
            Label lbl = new Label { Text = label.ToUpper(), ForeColor = colorTextoSecundario, Font = new Font("Segoe UI", 7, FontStyle.Bold), Dock = DockStyle.Top, Height = 15, Margin = new Padding(0, 10, 0, 0) };
            Panel wrapper = new Panel { Dock = DockStyle.Top, Height = 35, BackColor = Color.FromArgb(40, 40, 40), Padding = new Padding(10, 5, 10, 5), Margin = new Padding(0, 0, 0, 15) };
            TextBox txt = new TextBox { Dock = DockStyle.Fill, BackColor = Color.FromArgb(40, 40, 40), ForeColor = Color.White, BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 10) };
            wrapper.Controls.Add(txt);
            p.Controls.Add(wrapper);
            p.Controls.Add(lbl);
            return txt;
        }

        private Button CrearBotonAccion(string texto, Color bg, Color fg)
        {
            Button btn = new Button {
                Text = texto, Dock = DockStyle.Top, Height = 40, Margin = new Padding(0, 5, 0, 0),
                FlatStyle = FlatStyle.Flat, BackColor = bg, ForeColor = fg,
                Font = new Font("Segoe UI", 8, FontStyle.Bold), Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private Label CrearCard(FlowLayoutPanel p, string titulo, string valor, Color color)
        {
            Panel card = new Panel { Width = 220, Height = 85, BackColor = colorPanel, Margin = new Padding(0, 0, 15, 0), Padding = new Padding(15) };
            Label lblTit = new Label { Text = titulo, ForeColor = colorTextoSecundario, Font = new Font("Segoe UI", 7, FontStyle.Bold), Dock = DockStyle.Top, Height = 15 };
            Label lblVal = new Label { Text = valor, ForeColor = color, Font = new Font("Segoe UI", 14, FontStyle.Bold), Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
            card.Controls.Add(lblVal);
            card.Controls.Add(lblTit);
            p.Controls.Add(card);
            return lblVal;
        }

        private DataGridView CrearTabla()
        {
            DataGridView dgv = new DataGridView {
                Dock = DockStyle.Fill, BackgroundColor = colorFondo,
                ForeColor = colorTexto, BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false, RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                EnableHeadersVisualStyles = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowTemplate = { Height = 35 }, GridColor = Color.FromArgb(40, 40, 40)
            };
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 32, 32);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = colorAcento;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;
            dgv.DefaultCellStyle.BackColor = colorFondo;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 45, 45);
            dgv.DefaultCellStyle.SelectionForeColor = colorAcento;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(22, 22, 22);
            return dgv;
        }

        private void ActualizarReportes()
        {
            if (dgvMasVendidos == null) return;
            dgvMasVendidos.DataSource = null;
            dgvMasVendidos.DataSource = Datos.ObtenerMasVendidos();
            
            decimal total = Datos.ObtenerTotalVentas(dtpInicio.Value, dtpFin.Value);
            decimal ganancia = Datos.ObtenerGanancia(dtpInicio.Value, dtpFin.Value);
            
            lblTotalVentas.Text = $"Q {total:N2}";
            lblGanancia.Text = $"Q {ganancia:N2}";
        }

        private void CargarEntregas()
        {
            if (dgvEntregas == null) return;
            dgvEntregas.DataSource = null;
            dgvEntregas.DataSource = Datos.ObtenerVentasPendientes();
        }

        private void DgvInventario_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (dgvInventario.SelectedRows.Count > 0)
            {
                Producto p = (Producto)dgvInventario.SelectedRows[0].DataBoundItem;
                txtCodigo.Text = p.codigo;
                txtNombre.Text = p.nombre;
                txtMarca.Text = p.marca;
                txtPrecioC.Text = p.precioCompra.ToString();
                txtPrecioV.Text = p.precioVenta.ToString();
                txtStock.Text = p.cantidad.ToString();
                txtImagen.Text = p.rutaImagen;
                txtCodigo.Enabled = false;
            }
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            try {
                Producto p = new Producto {
                    codigo = txtCodigo.Text, nombre = txtNombre.Text, marca = txtMarca.Text,
                    precioCompra = decimal.Parse(txtPrecioC.Text), precioVenta = decimal.Parse(txtPrecioV.Text),
                    cantidad = int.Parse(txtStock.Text), rutaImagen = txtImagen.Text
                };
                Datos.AgregarNuevoProducto(p);
                dgvInventario.DataSource = null;
                dgvInventario.DataSource = Datos.Productos;
                MessageBox.Show("Producto guardado correctamente.");
            } catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void BtnModificar_Click(object? sender, EventArgs e)
        {
            try {
                Producto p = new Producto {
                    codigo = txtCodigo.Text, nombre = txtNombre.Text, marca = txtMarca.Text,
                    precioCompra = decimal.Parse(txtPrecioC.Text), precioVenta = decimal.Parse(txtPrecioV.Text),
                    cantidad = int.Parse(txtStock.Text), rutaImagen = txtImagen.Text
                };
                Datos.ActualizarProducto(p);
                dgvInventario.DataSource = null;
                dgvInventario.DataSource = Datos.Productos;
                MessageBox.Show("Producto actualizado.");
            } catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (dgvInventario.SelectedRows.Count > 0)
            {
                string? codigo = dgvInventario.SelectedRows[0].Cells["codigo"].Value.ToString();
                if (codigo == null) return;
                if (MessageBox.Show("¿Eliminar este producto?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Datos.EliminarProducto(codigo);
                    dgvInventario.DataSource = null;
                    dgvInventario.DataSource = Datos.Productos;
                }
            }
        }
    }
}
