using System;
using System.Drawing;
using System.Windows.Forms;

namespace MangaNya
{
    public partial class UC_Gerente : UserControl
    {
        private Button btnVolver;
        private Panel pnlFormulario;
        private DataGridView dgvInventario;
        private TextBox txtCodigo, txtNombre, txtMarca, txtPrecioC, txtPrecioV, txtStock, txtImagen;

        public UC_Gerente()
        {
            InitializeComponent();
            CargarInventario();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(15, 15, 15);
            this.Padding = new Padding(20);

            // Botón Volver
            btnVolver = new Button
            {
                Text = "← VOLVER A COMPRAS", Height = 50, Dock = DockStyle.Top,
                FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(221, 160, 221),
                ForeColor = Color.Black, Font = new Font("Segoe UI", 12, FontStyle.Bold), Cursor = Cursors.Hand
            };
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.Click += (s, e) => {
                if (this.ParentForm is MainForm main) main.CambiarVista(new UC_PuntoVenta());
            };

            // Contenedor principal
            Panel pnlMain = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 20, 0, 0) };

            // Formulario Izquierda
            pnlFormulario = new Panel { Dock = DockStyle.Left, Width = 300, BackColor = Color.FromArgb(25, 25, 25), Padding = new Padding(20) };
            
            Label lblTitulo = new Label { Text = "NUEVO PRODUCTO", ForeColor = Color.White, Font = new Font("Segoe UI", 12, FontStyle.Bold), Dock = DockStyle.Top, Height = 40 };
            pnlFormulario.Controls.Add(lblTitulo);

            txtCodigo = CrearCampo("Código");
            txtNombre = CrearCampo("Nombre");
            txtMarca = CrearCampo("Marca");
            txtPrecioC = CrearCampo("Precio Compra");
            txtPrecioV = CrearCampo("Precio Venta");
            txtStock = CrearCampo("Stock");
            txtImagen = CrearCampo("Nombre Imagen (ej: m1.jpg)");

            Button btnGuardar = new Button
            {
                Text = "GUARDAR PRODUCTO", Dock = DockStyle.Top, Height = 45, Margin = new Padding(0, 20, 0, 0),
                FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(147, 112, 219), // MediumPurple
                ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold), Cursor = Cursors.Hand
            };
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Click += BtnGuardar_Click;
            pnlFormulario.Controls.Add(btnGuardar);

            Button btnModificar = new Button
            {
                Text = "MODIFICAR SELECCIONADO", Dock = DockStyle.Top, Height = 45, Margin = new Padding(0, 10, 0, 0),
                FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(70, 70, 70), 
                ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold), Cursor = Cursors.Hand
            };
            btnModificar.FlatAppearance.BorderSize = 0;
            btnModificar.Click += BtnModificar_Click;
            pnlFormulario.Controls.Add(btnModificar);

            Button btnEliminar = new Button
            {
                Text = "ELIMINAR SELECCIONADO", Dock = DockStyle.Top, Height = 45, Margin = new Padding(0, 10, 0, 0),
                FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(200, 50, 50), 
                ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold), Cursor = Cursors.Hand
            };
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.Click += BtnEliminar_Click;
            pnlFormulario.Controls.Add(btnEliminar);

            // DataGridView Derecha
            dgvInventario = new DataGridView
            {
                Dock = DockStyle.Fill, BackgroundColor = Color.FromArgb(20, 20, 20),
                ForeColor = Color.White, BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false, RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                EnableHeadersVisualStyles = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Margin = new Padding(20, 0, 0, 0)
            };
            dgvInventario.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            dgvInventario.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvInventario.DefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
            dgvInventario.CellClick += DgvInventario_CellClick;

            pnlMain.Controls.Add(dgvInventario);
            pnlMain.Controls.Add(new Panel { Dock = DockStyle.Left, Width = 20 }); // Espaciador
            pnlMain.Controls.Add(pnlFormulario);

            this.Controls.Add(pnlMain);
            this.Controls.Add(btnVolver);
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
                txtCodigo.Enabled = false; // El código no debe cambiarse
            }
        }

        private TextBox CrearCampo(string placeholder)
        {
            Label lbl = new Label { Text = placeholder, ForeColor = Color.Gray, Dock = DockStyle.Top, Height = 20, Margin = new Padding(0, 10, 0, 0) };
            TextBox txt = new TextBox { Dock = DockStyle.Top, BackColor = Color.FromArgb(40, 40, 40), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 10) };
            pnlFormulario.Controls.Add(txt);
            pnlFormulario.Controls.Add(lbl);
            return txt;
        }

        private void CargarInventario()
        {
            dgvInventario.DataSource = null;
            dgvInventario.DataSource = Datos.Productos;
            txtCodigo.Enabled = true;
            txtCodigo.Clear(); txtNombre.Clear(); txtMarca.Clear(); txtPrecioC.Clear(); txtPrecioV.Clear(); txtStock.Clear(); txtImagen.Clear();
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
                CargarInventario();
                MessageBox.Show("Producto agregado.");
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
                CargarInventario();
                MessageBox.Show("Producto modificado.");
            } catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (dgvInventario.SelectedRows.Count > 0)
            {
                string codigo = dgvInventario.SelectedRows[0].Cells["codigo"].Value.ToString();
                DialogResult res = MessageBox.Show("¿Está seguro de eliminar este producto?", "Confirmar", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    Datos.EliminarProducto(codigo);
                    CargarInventario();
                }
            }
        }
    }
}
