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

            using (StreamWriter escritor = new StreamWriter("productosv1.txt"))
            {
                foreach (Producto p in Productos)
                {

                    string linea = $"{p.codigo}|{p.nombre}|{p.marca}|{p.precioCompra}|{p.precioVenta}|{p.cantidad}|{p.informacionExtra}|{p.unidadesVendidas}";

                    escritor.WriteLine(linea);

                }
            }
        }



        public static void CargarDatos()
        {


            if (File.Exists("productosv1.txt"))
            {

                Productos.Clear();

                using (StreamReader lector = new StreamReader("productosv1.txt"))
                {
                    string linea;

                    while ((linea = lector.ReadLine()) != null)
                    {

                        string[] datos = linea.Split('|');


                        if (datos.Length >= 6)
                        {
                            Producto nuevoProducto = new Producto();
                            nuevoProducto.codigo = datos[0];
                            nuevoProducto.nombre = datos[1];
                            nuevoProducto.marca = datos[2];


                            nuevoProducto.precioCompra = decimal.Parse(datos[3]);
                            nuevoProducto.precioVenta = decimal.Parse(datos[4]);
                            nuevoProducto.cantidad = int.Parse(datos[5]);

                            if (datos.Length >= 8)
                            {
                                nuevoProducto.informacionExtra = datos[6];
                                nuevoProducto.unidadesVendidas = int.Parse(datos[7]);
                            }


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

        public static void ModificarProducto(string codigo, Producto productoModificado)
        {
            Producto p = Productos.Find(prod => prod.codigo == codigo);
            if (p != null)
            {
                p.nombre = productoModificado.nombre;
                p.marca = productoModificado.marca;
                p.precioCompra = productoModificado.precioCompra;
                p.precioVenta = productoModificado.precioVenta;
                p.cantidad = productoModificado.cantidad;
                p.informacionExtra = productoModificado.informacionExtra;
                GuardarProductoTxt();
            }
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
                    p.unidadesVendidas += cantidadVendida;
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
                    nuevoDetalle.nombreProducto = productoEncontrado.nombre;
                    nuevoDetalle.codigoProducto = productoEncontrado.codigo;
                    nuevoDetalle.precio = productoEncontrado.precioVenta;
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
              
                foreach (Producto p in Productos)
                {
                    if (p.codigo == detalle.codigoProducto)
                    {
                        
                        subtotal += (p.precioVenta * detalle.cantidad);
                        break;
                    }
                }
            }

            return subtotal;
        }




        public static void EliminarDelCarrito(string codigoBuscado)
        {
            DetalleFactura detalleAEliminar = null;

            foreach (DetalleFactura detalle in CarritoTemporal)
            {
                if (detalle.codigoProducto == codigoBuscado)
                {
                    detalleAEliminar = detalle;
                    break;
                }
            }

            if (detalleAEliminar != null)
            {
                CarritoTemporal.Remove(detalleAEliminar);
            }
        }





    }




}
