using System;
using System.Collections.Generic;
using System.Text;

namespace MangaNya
{
    internal class Factura
    {
        string numeroFactura { get; set; } = string.Empty;
        string fecha { get; set; } = string.Empty;
        string cliente { get; set; } = string.Empty;
        string nit { get; set; } = string.Empty;
        string producto { get; set; } = string.Empty;
        string cantidad { get; set; } = string.Empty;
        string precioUnitario { get; set; } = string.Empty;
        string total { get; set; } = string.Empty;
    }
}
