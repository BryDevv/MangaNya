using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MangaNya
{
    public static class Datos    
    {
        public static List<Cliente> Clientes { get; set; } = new List<Cliente>();
        public static List<Producto> Productos { get; set; } = new List<Producto>();
        public static List<Factura> Facturas { get; set; } = new List<Factura>();

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



    }
}
