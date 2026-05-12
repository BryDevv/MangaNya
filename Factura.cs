using System;
using System.Collections.Generic;

namespace MangaNya
{
    public class Factura
    {
        public string Numero { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string ClienteNombre { get; set; } = "Consumidor Final";
        public string ClienteNIT { get; set; } = "CF";
        public List<DetalleFactura> Detalles { get; set; } = new List<DetalleFactura>();
        public decimal Total { get; set; }
    }
}
