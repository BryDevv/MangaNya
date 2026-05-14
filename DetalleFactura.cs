namespace MangaNya
{
    public class DetalleFactura
    {
        public string codigoProducto { get; set; } = string.Empty;
        public string nombreProducto { get; set; } = string.Empty;
        public int cantidad { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal precioCompra { get; set; }
        public decimal subtotal => cantidad * precioUnitario;
    }
}
