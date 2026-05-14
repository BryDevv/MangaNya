using System;
using System.Drawing;
using System.Windows.Forms;

namespace MangaNya
{
    public partial class MainForm : Form
    {
        private Panel pnlContenedor;
        private Panel pnlAdminTab;
        private Label lblAdminIcon;
        private System.Windows.Forms.Timer timerSlide;
        private bool isExpanded = false;
        private int targetWidth = 120;
        private int collapsedWidth = 10;

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

            pnlContenedor = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            // Panel deslizable para Admin
            pnlAdminTab = new Panel
            {
                Size = new Size(collapsedWidth, 80),
                Location = new Point(this.Width - collapsedWidth - 15, 300),
                BackColor = Color.FromArgb(187, 134, 252), // Color Acento
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Right
            };

            lblAdminIcon = new Label
            {
                Text = "⚙  ADMIN",
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Visible = false
            };
            
            pnlAdminTab.Controls.Add(lblAdminIcon);
            
            // Eventos para el panel
            pnlAdminTab.MouseEnter += (s, e) => ExpandirAdmin(true);
            pnlAdminTab.MouseLeave += (s, e) => {
                // Solo colapsar si el mouse realmente salió del área combinada
                if (!pnlAdminTab.ClientRectangle.Contains(pnlAdminTab.PointToClient(Control.MousePosition)))
                    ExpandirAdmin(false);
            };

            // Eventos para el label (ya que ocupa todo el panel)
            lblAdminIcon.MouseEnter += (s, e) => ExpandirAdmin(true);
            lblAdminIcon.MouseLeave += (s, e) => {
                if (!pnlAdminTab.ClientRectangle.Contains(pnlAdminTab.PointToClient(Control.MousePosition)))
                    ExpandirAdmin(false);
            };
            
            lblAdminIcon.Click += (s, e) => CambiarVista(new UC_Gerente());

            timerSlide = new System.Windows.Forms.Timer { Interval = 10 };
            timerSlide.Tick += TimerSlide_Tick;

            this.Controls.Add(pnlAdminTab);
            this.Controls.Add(pnlContenedor);

            pnlAdminTab.BringToFront();
        }

        private void ExpandirAdmin(bool expandir)
        {
            isExpanded = expandir;
            if (expandir) lblAdminIcon.Visible = true;
            timerSlide.Start();
        }

        private void TimerSlide_Tick(object? sender, EventArgs e)
        {
            int step = 15;
            int rightEdge = this.ClientSize.Width - 15;

            if (isExpanded)
            {
                if (pnlAdminTab.Width + step < targetWidth)
                {
                    pnlAdminTab.Width += step;
                }
                else
                {
                    pnlAdminTab.Width = targetWidth;
                    timerSlide.Stop();
                }
            }
            else
            {
                if (pnlAdminTab.Width - step > collapsedWidth)
                {
                    pnlAdminTab.Width -= step;
                }
                else
                {
                    pnlAdminTab.Width = collapsedWidth;
                    lblAdminIcon.Visible = false;
                    timerSlide.Stop();
                }
            }
            // Reposicionar Left para que el borde derecho sea siempre constante
            pnlAdminTab.Left = rightEdge - pnlAdminTab.Width;
        }

        public void CambiarVista(UserControl vista)
        {
            pnlContenedor.Controls.Clear();
            vista.Dock = DockStyle.Fill;
            pnlContenedor.Controls.Add(vista);
            
            bool isGerente = vista is UC_Gerente;
            pnlAdminTab.Visible = !isGerente;

            if (!isGerente)
            {
                // Reset absoluto del estado al volver para evitar imprecisiones
                timerSlide.Stop();
                isExpanded = false;
                pnlAdminTab.Width = collapsedWidth;
                pnlAdminTab.Left = this.ClientSize.Width - collapsedWidth - 15;
                lblAdminIcon.Visible = false;
            }
        }
    }
}
