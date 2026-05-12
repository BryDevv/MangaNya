using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MangaNya
{
    public partial class ProductoCard : UserControl
    {
        private string _codigo = string.Empty;
        private PictureBox pbImagen = null!;
        private Label lblNombre = null!;
        private Label lblPrecio = null!;
        private Button btnAgregar = null!;

        public event EventHandler? OnProductoAgregado;

        public ProductoCard()
        {
            InitializeComponent();
        }

        public ProductoCard(string codigo, string nombre, decimal precio, string rutaImagen) : this()
        {
            this._codigo = codigo;
            ConfigurarDatos(nombre, precio, rutaImagen);
        }

        private void InitializeComponent()
        {
            pbImagen = new PictureBox();
            lblNombre = new Label();
            lblPrecio = new Label();
            btnAgregar = new Button();
            ((System.ComponentModel.ISupportInitialize)pbImagen).BeginInit();
            SuspendLayout();
            // 
            // pbImagen
            // 
            pbImagen.BackColor = Color.FromArgb(25, 25, 25);
            pbImagen.Location = new Point(10, 10);
            pbImagen.Name = "pbImagen";
            pbImagen.Size = new Size(180, 160);
            pbImagen.SizeMode = PictureBoxSizeMode.Zoom;
            pbImagen.TabIndex = 0;
            pbImagen.TabStop = false;
            // 
            // lblNombre
            // 
            lblNombre.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNombre.ForeColor = Color.White;
            lblNombre.Location = new Point(10, 175);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(180, 25);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre Producto";
            lblNombre.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblPrecio
            // 
            lblPrecio.Font = new Font("Segoe UI", 11F);
            lblPrecio.ForeColor = Color.FromArgb(221, 160, 221);
            lblPrecio.Location = new Point(10, 200);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(180, 25);
            lblPrecio.TabIndex = 2;
            lblPrecio.Text = "Q 0.00";
            lblPrecio.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.FromArgb(221, 160, 221);
            btnAgregar.Cursor = Cursors.Hand;
            btnAgregar.FlatAppearance.BorderSize = 0;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAgregar.ForeColor = Color.Black;
            btnAgregar.Location = new Point(20, 230);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(160, 35);
            btnAgregar.TabIndex = 3;
            btnAgregar.Text = "AGREGAR";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += BtnAgregar_Click;
            // 
            // ProductoCard
            // 
            BackColor = Color.FromArgb(30, 30, 30);
            Controls.Add(btnAgregar);
            Controls.Add(lblPrecio);
            Controls.Add(lblNombre);
            Controls.Add(pbImagen);
            Margin = new Padding(10);
            Name = "ProductoCard";
            Padding = new Padding(10);
            Size = new Size(200, 280);
            ((System.ComponentModel.ISupportInitialize)pbImagen).EndInit();
            ResumeLayout(false);
        }

        private void ConfigurarDatos(string nombre, decimal precio, string rutaImagen)
        {
            lblNombre.Text = nombre;
            lblPrecio.Text = "Q " + precio.ToString("N2");

            if (!string.IsNullOrEmpty(rutaImagen))
            {
                string fullPath = Path.Combine(Application.StartupPath, "Imagenes", rutaImagen);
                if (File.Exists(fullPath))
                {
                    try
                    {
                        // Usar FileStream para evitar bloquear el archivo
                        using (var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                        {
                            pbImagen.Image = Image.FromStream(stream);
                        }
                    }
                    catch { /* Ignorar errores de carga de imagen */ }
                }
            }
        }

        private void BtnAgregar_Click(object? sender, EventArgs e)
        {
            Datos.AgregarAlCarrito(this._codigo);
            OnProductoAgregado?.Invoke(this, EventArgs.Empty);
        }
    }
}
