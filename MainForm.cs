using System;
using System.Drawing;
using System.Windows.Forms;

namespace MangaNya
{
    public partial class MainForm : Form
    {
        private Panel pnlContenedor;
        private Label btnAdmin;

        public MainForm()
        {
            InitializeComponent();
            CambiarVista(new UC_PuntoVenta());
        }

        private void InitializeComponent()
        {
            this.Text = "MangaNya - Enterprise Edition";
            this.Size = new Size(1200, 850);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(10, 10, 10);

            // Botón Admin (Esquina superior derecha)
            btnAdmin = new Label
            {
                Text = "⚙ Admin",
                Size = new Size(100, 30),
                Location = new Point(this.Width - 130, 10),
                ForeColor = Color.FromArgb(221, 160, 221),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnAdmin.Click += (s, e) => CambiarVista(new UC_Gerente());

            pnlContenedor = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            this.Controls.Add(btnAdmin);
            this.Controls.Add(pnlContenedor);

            // Asegurar que el botón esté al frente
            btnAdmin.BringToFront();
        }

        public void CambiarVista(UserControl vista)
        {
            pnlContenedor.Controls.Clear();
            vista.Dock = DockStyle.Fill;
            pnlContenedor.Controls.Add(vista);
        }
    }
}
