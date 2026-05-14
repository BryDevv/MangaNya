using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MangaNya
{
    public static class Datos
    {
        public static List<Cliente> Clientes { get; set; } = new List<Cliente>();
        public static List<Producto> Productos { get; set; } = new List<Producto>();
        public static List<Factura> Facturas { get; set; } = new List<Factura>();

        public static List<DetalleFactura> CarritoTemporal { get; set; } = new List<DetalleFactura>();
        private static int contadorFacturas = 1;

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
            CargarFacturas();
        }

        public static void CargarFacturas()
        {
            if (File.Exists("facturas.txt"))
            {
                Facturas.Clear();
                using (StreamReader sr = new StreamReader("facturas.txt"))
                {
                    string linea;
                    Factura facturaActual = null;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        if (linea.StartsWith("FECHA:"))
                        {
                            string[] partes = linea.Split('|');
                            facturaActual = new Factura
                            {
                                Fecha = DateTime.Parse(partes[0].Replace("FECHA:", "")),
                                Numero = partes[1].Replace("NUM:", ""),
                                ClienteNIT = partes[2].Replace("NIT:", ""),
                                ClienteNombre = partes[3].Replace("CLIENTE:", ""),
                                Total = decimal.Parse(partes[4].Replace("TOTAL:", "")),
                                Entregada = bool.Parse(partes[5].Replace("ENTREGADA:", ""))
                            };
                            Facturas.Add(facturaActual);
                        }
                        else if (linea.StartsWith("  ITEM:") && facturaActual != null)
                        {
                            string[] partes = linea.Replace("  ITEM:", "").Split('|');
                            facturaActual.Detalles.Add(new DetalleFactura
                            {
                                codigoProducto = partes[0],
                                nombreProducto = partes[1],
                                cantidad = int.Parse(partes[2]),
                                precioUnitario = decimal.Parse(partes[3]),
                                precioCompra = decimal.Parse(partes[4])
                            });
                        }
                    }
                }
                if (Facturas.Count > 0)
                {
                    string ultimoNum = Facturas.Last().Numero.Replace("FAC-", "");
                    if (int.TryParse(ultimoNum, out int num)) contadorFacturas = num + 1;
                }
            }
        }

        public static void GuardarFacturas()
        {
            string path = "facturas.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (var f in Facturas)
                {
                    sw.WriteLine($"FECHA:{f.Fecha}|NUM:{f.Numero}|NIT:{f.ClienteNIT}|CLIENTE:{f.ClienteNombre}|TOTAL:{f.Total}|ENTREGADA:{f.Entregada}");
                    foreach (var d in f.Detalles)
                    {
                        sw.WriteLine($"  ITEM:{d.codigoProducto}|{d.nombreProducto}|{d.cantidad}|{d.precioUnitario}|{d.precioCompra}");
                    }
                    sw.WriteLine("--------------------------------------------------");
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
                if (c.nit == nitBuscado) return c;
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
            Producto productoEncontrado = Productos.Find(p => p.codigo == codigoBuscado);
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
                    CarritoTemporal.Add(new DetalleFactura { 
                        codigoProducto = productoEncontrado.codigo, 
                        cantidad = 1 
                    });
                }
            }
        }

        public static decimal CalcularSubtotal()
        {
            decimal subtotal = 0;
            foreach (DetalleFactura detalle in CarritoTemporal)
            {
                Producto p = Productos.Find(prod => prod.codigo == detalle.codigoProducto);
                if (p != null) subtotal += (p.precioVenta * detalle.cantidad);
            }
            return subtotal;
        }

        public static Factura FinalizarVenta(string nombreCliente = "Consumidor Final", string nitCliente = "CF")
        {
            Factura nuevaFactura = new Factura
            {
                Numero = "FAC-" + contadorFacturas++.ToString("D5"),
                Fecha = DateTime.Now,
                ClienteNombre = nombreCliente,
                ClienteNIT = nitCliente,
                Total = CalcularSubtotal(),
                Entregada = false
            };

            foreach (var detalle in CarritoTemporal)
            {
                Producto p = Productos.Find(prod => prod.codigo == detalle.codigoProducto);
                if (p != null)
                {
                    detalle.nombreProducto = p.nombre;
                    detalle.precioUnitario = p.precioVenta;
                    detalle.precioCompra = p.precioCompra;
                }
                
                nuevaFactura.Detalles.Add(new DetalleFactura 
                { 
                    codigoProducto = detalle.codigoProducto,
                    nombreProducto = detalle.nombreProducto,
                    cantidad = detalle.cantidad,
                    precioUnitario = detalle.precioUnitario,
                    precioCompra = detalle.precioCompra
                });

                DescontarStock(detalle.codigoProducto, detalle.cantidad);
            }

            Facturas.Add(nuevaFactura);
            GuardarFacturas();
            CarritoTemporal.Clear();
            return nuevaFactura;
        }

        public static void MarcarComoEntregada(string numeroFactura)
        {
            var f = Facturas.Find(fac => fac.Numero == numeroFactura);
            if (f != null)
            {
                f.Entregada = true;
                GuardarFacturas();
            }
        }

        public static void QuitarDelCarrito(string codigoBuscado)
        {
            CarritoTemporal.RemoveAll(d => d.codigoProducto == codigoBuscado);
        }

        public static void EliminarProducto(string codigo)
        {
            Productos.RemoveAll(p => p.codigo == codigo);
            GuardarProductoTxt();
        }

        public static void ActualizarProducto(Producto pActualizado)
        {
            for (int i = 0; i < Productos.Count; i++)
            {
                if (Productos[i].codigo == pActualizado.codigo)
                {
                    Productos[i] = pActualizado;
                    break;
                }
            }
            GuardarProductoTxt();
        }

        public static List<dynamic> ObtenerMasVendidos()
        {
            var ventasPorProducto = new Dictionary<string, (string nombre, int cantidad)>();
            foreach (var f in Facturas)
            {
                foreach (var d in f.Detalles)
                {
                    if (ventasPorProducto.ContainsKey(d.codigoProducto))
                    {
                        var info = ventasPorProducto[d.codigoProducto];
                        info.cantidad += d.cantidad;
                        ventasPorProducto[d.codigoProducto] = info;
                    }
                    else
                    {
                        ventasPorProducto[d.codigoProducto] = (d.nombreProducto, d.cantidad);
                    }
                }
            }
            return ventasPorProducto.Select(kvp => new { Codigo = kvp.Key, Nombre = kvp.Value.nombre, Cantidad = kvp.Value.cantidad })
                                    .OrderByDescending(x => x.Cantidad)
                                    .Cast<dynamic>()
                                    .ToList();
        }

        public static decimal ObtenerTotalVentas(DateTime inicio, DateTime fin)
        {
            return Facturas.Where(f => f.Fecha.Date >= inicio.Date && f.Fecha.Date <= fin.Date)
                           .Sum(f => f.Total);
        }

        public static decimal ObtenerGanancia(DateTime inicio, DateTime fin)
        {
            decimal ganancia = 0;
            var facturasRango = Facturas.Where(f => f.Fecha.Date >= inicio.Date && f.Fecha.Date <= fin.Date);
            foreach (var f in facturasRango)
            {
                foreach (var d in f.Detalles)
                {
                    ganancia += (d.precioUnitario - d.precioCompra) * d.cantidad;
                }
            }
            return ganancia;
        }

        public static List<Factura> ObtenerVentasPendientes()
        {
            return Facturas.Where(f => !f.Entregada).ToList();
        }
    }
}
