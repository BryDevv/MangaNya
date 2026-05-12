namespace MangaNya
{
    public static class Datos
    {
        public static List<Cliente> Clientes { get; set; } = new List<Cliente>();
        public static List<Producto> Productos { get; set; } = new List<Producto>();
        public static List<Factura> Facturas { get; set; } = new List<Factura>();

        public static List<DetalleFactura> CarritoTemporal { get; set; } = new List<DetalleFactura>();

        public static void GuardarProductoTxt()
        {

            using (StreamWriter escritor = new StreamWriter("productos.txt"))
            {
                foreach (Producto p in Productos)
                {

                    string linea = $"{p.codigo}|{p.nombre}|{p.marca}|{p.precioCompra}|{p.precioVenta}|{p.cantidad}|{p.rutaImagen}";

                    escritor.WriteLine(linea);

                }
            }
        }



        public static void CargarDatos()
        {


            if (File.Exists("productos.txt"))
            {

                Productos.Clear();

                using (StreamReader lector = new StreamReader("productos.txt"))
                {
                    string linea;

                    while ((linea = lector.ReadLine()) != null)
                    {

                        string[] datos = linea.Split('|');


                        if (datos.Length == 7)
                        {
                            Producto nuevoProducto = new Producto();
                            nuevoProducto.codigo = datos[0];
                            nuevoProducto.nombre = datos[1];
                            nuevoProducto.marca = datos[2];


                            nuevoProducto.precioCompra = decimal.Parse(datos[3]);
                            nuevoProducto.precioVenta = decimal.Parse(datos[4]);
                            nuevoProducto.cantidad = int.Parse(datos[5]);

                            nuevoProducto.rutaImagen = datos[6];


                            Productos.Add(nuevoProducto);
                        }
                    }
                }
            }



        }


        public static void AgregarNuevoProducto(Producto nuevoProducto)
        {
            Productos.Add(nuevoProducto);
            GuardarProductoTxt();
        }



        public static Cliente BuscarClientePorNIT(string nitBuscado)
        {
            foreach (Cliente c in Clientes)
            {

                if (c.nit == nitBuscado)
                {
                    return c;
                }
            }
            return null;
        }


        public static void DescontarStock(string codigoVendido, int cantidadVendida)
        {
            foreach (Producto p in Productos)
            {
                if (p.codigo == codigoVendido)
                {
                    p.cantidad -= cantidadVendida;
                    break;
                }
            }
            GuardarProductoTxt();
        }


        public static void AgregarAlCarrito(string codigoBuscado)
        {
            //Verificar si ya hay algo en el invetario

            Producto productoEncontrado = null;
            foreach (Producto p in Productos)
            {
                if (p.codigo == codigoBuscado)
                {
                    productoEncontrado = p;
                    break;
                }
            }


            if (productoEncontrado != null && productoEncontrado.cantidad > 0)
            {

                bool yaEstabaEnCarrito = false;
                foreach (DetalleFactura detalle in CarritoTemporal)
                {
                    if (detalle.codigoProducto == codigoBuscado)
                    {
                        detalle.cantidad++;
                        yaEstabaEnCarrito = true;
                        break;
                    }
                }


                if (!yaEstabaEnCarrito)
                {
                    DetalleFactura nuevoDetalle = new DetalleFactura();
                    nuevoDetalle.codigoProducto = productoEncontrado.codigo;
                    nuevoDetalle.cantidad = 1;

                    CarritoTemporal.Add(nuevoDetalle);
                }
            }
        }


        public static decimal CalcularSubtotal()
        {
            decimal subtotal = 0;

            foreach (DetalleFactura detalle in CarritoTemporal)
            {
                // Se busca el precio de venta productos con un foreach 
                foreach (Producto p in Productos)
                {
                    if (p.codigo == detalle.codigoProducto)
                    {
                        // Multiplicamos el precio por la cantidad que llevamos en el carrito
                        subtotal += (p.precioVenta * detalle.cantidad);
                        break;
                    }
                }
            }

            return subtotal;
        }

        public static void FinalizarVenta()
        {
            foreach (var detalle in CarritoTemporal)
            {
                DescontarStock(detalle.codigoProducto, detalle.cantidad);
            }
            CarritoTemporal.Clear();
        }










    }




}
