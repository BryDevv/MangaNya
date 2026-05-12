using System;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace MangaNya
{
    public static class ImpresoraFactura
    {
        public static string GenerarTextoFactura(Factura f)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************************");
            sb.AppendLine("           MANGANYA STORE               ");
            sb.AppendLine("       Expertos en Manga y Anime        ");
            sb.AppendLine("****************************************");
            sb.AppendLine($"Factura No: {f.Numero}");
            sb.AppendLine($"Fecha:      {f.Fecha:dd/MM/yyyy HH:mm}");
            sb.AppendLine($"Cliente:    {f.ClienteNombre}");
            sb.AppendLine($"NIT:        {f.ClienteNIT}");
            sb.AppendLine("----------------------------------------");
            sb.AppendLine(string.Format("{0,-20} {1,5} {2,12}", "Producto", "Cant", "Subtotal"));
            sb.AppendLine("----------------------------------------");

            foreach (var d in f.Detalles)
            {
                string nombreCorte = d.nombreProducto.Length > 18 ? d.nombreProducto.Substring(0, 18) : d.nombreProducto;
                sb.AppendLine(string.Format("{0,-20} {1,5} {2,12:N2}", nombreCorte, d.cantidad, d.subtotal));
            }

            sb.AppendLine("----------------------------------------");
            sb.AppendLine(string.Format("{0,26} {1,12:N2}", "TOTAL:", f.Total));
            sb.AppendLine("****************************************");
            sb.AppendLine("      ¡Gracias por su compra!           ");
            sb.AppendLine("       Siguenos en MangaNya.com         ");
            sb.AppendLine("****************************************");

            return sb.ToString();
        }

        public static void Imprimir(string texto)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (sender, ev) => {
                // Usamos Courier New para asegurar que las columnas se alineen perfectamente
                Font font = new Font("Courier New", 10);
                float yPos = 20;
                float leftMargin = 20;
                
                string[] lineas = texto.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                foreach (string linea in lineas)
                {
                    ev.Graphics.DrawString(linea, font, Brushes.Black, leftMargin, yPos);
                    yPos += font.GetHeight(ev.Graphics);
                }
            };

            try
            {
                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir: " + ex.Message);
            }
        }
    }
}
