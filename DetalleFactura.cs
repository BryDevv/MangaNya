using System;
using System.Collections.Generic;
using System.Text;

namespace MangaNya
{
    public class DetalleFactura
    {
        public string nombreProducto { get; set; } = string.Empty;
        public int cantidad { get; set; }
        public string codigoProducto { get; set; } = string.Empty;
       
        public decimal precio { get; set; }


    }
}
