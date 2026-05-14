using System.Data;

namespace MangaNya
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Datos.CargarDatos();
            ActualizarTablaProductos();


        }


        private void ActualizarTablaProductos()
        {

            DataP.DataSource = null;
            DataP_admin.DataSource = null;

            DataP.DataSource = Datos.Productos;
            DataP_admin.DataSource = Datos.Productos;
        }




        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (DataP.CurrentRow != null)
            {

                string codigoSeleccionado = DataP.CurrentRow.Cells["codigo"].Value.ToString();
                

                Datos.AgregarAlCarrito(codigoSeleccionado);


                Data2.DataSource = null!;
                Data2.DataSource = Datos.CarritoTemporal;
                decimal total = Datos.CalcularSubtotal();
                label9.Text = "Total: Q " + total.ToString("00.0");

            }

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (Data2.CurrentRow != null)
            {
               
                DialogResult respuesta = MessageBox.Show("¿Estás seguro de que deseas eliminar este producto del carrito?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    decimal total = Datos.CalcularSubtotal()-(Convert.ToDecimal(Data2.CurrentRow.Cells["precio"].Value)*Convert.ToInt32(Data2.CurrentRow.Cells["cantidad"].Value));
                    string codigoSeleccionado = Data2.CurrentRow.Cells["codigoProducto"].Value.ToString();
                    
                    Datos.EliminarDelCarrito(codigoSeleccionado);
                    label9.Text = "Total: Q "+total.ToString("00.0");

                    Data2.DataSource = null!;
                    Data2.DataSource = Datos.CarritoTemporal;

                    
                }
            }
            

        }
    }
}
