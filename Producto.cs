using System;
using System.Collections.Generic;
using System.Text;

namespace MangaNya
{
    public class Producto
    {
        public string codigo { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;
        public string marca { get; set; } = string.Empty;
        public decimal precioCompra { get; set; }
        public decimal precioVenta { get; set; }
        public int cantidad { get; set; }
        public string informacionExtra { get; set; } = string.Empty;
        public int unidadesVendidas { get; set; }
    }
}
