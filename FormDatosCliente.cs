using System;
using System.Drawing;
using System.Windows.Forms;

namespace MangaNya
{
    public class FormDatosCliente : Form
    {
        public string NombreCliente { get; private set; } = "Consumidor Final";
        public string NitCliente { get; private set; } = "CF";
        public bool Confirmado { get; private set; } = false;

        private TextBox txtNombre;
        private TextBox txtNit;
        private Button btnAceptar;

        public FormDatosCliente()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Datos de Facturación";
            this.Size = new Size(300, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ForeColor = Color.White;

            Label lblNit = new Label { Text = "NIT:", Location = new Point(20, 20), AutoSize = true };
            txtNit = new TextBox { Location = new Point(20, 40), Width = 240, BackColor = Color.FromArgb(50, 50, 50), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            txtNit.Text = "CF";

            Label lblNombre = new Label { Text = "Nombre:", Location = new Point(20, 80), AutoSize = true };
            txtNombre = new TextBox { Location = new Point(20, 100), Width = 240, BackColor = Color.FromArgb(50, 50, 50), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            txtNombre.Text = "Consumidor Final";

            btnAceptar = new Button { 
                Text = "CONFIRMAR", 
                Location = new Point(20, 150), 
                Width = 240, 
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(221, 160, 221),
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnAceptar.Click += (s, e) => {
                this.NombreCliente = txtNombre.Text;
                this.NitCliente = txtNit.Text;
                this.Confirmado = true;
                this.Close();
            };

            this.Controls.Add(lblNit);
            this.Controls.Add(txtNit);
            this.Controls.Add(lblNombre);
            this.Controls.Add(txtNombre);
            this.Controls.Add(btnAceptar);
        }
    }
}
