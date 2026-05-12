using System;
using System.Text;

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
    }
}
