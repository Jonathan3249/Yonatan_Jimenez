using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace Parcial1._0
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            Cone();
        }
        public static SqlCommand comando;

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtPrecio.Text != "" && txtProducto.Text != "")
            {
                try
                {
                    comando.CommandText = "INSERT INTO Productos(nombre_producto, Precio) VALUES ('" + txtProducto.Text + "', '" + txtPrecio.Text + "');";
                    comando.Connection = Conexion.conexion;
                    Conexion.conexion.Open();
                    comando.ExecuteNonQuery();
                    comando.CommandText = "SELECT @@Identity";
                    Conexion.conexion.Close();
                    MessageBox.Show("Has ingresado los datos correctamente");
                    Cone();
                    limpiar();


                }
                catch (Exception ex)
                {
                    MessageBox.Show($"se ha producido un error {ex}");
                }
            }
            else
            {
                MessageBox.Show("Los campos no deben quedar vacios");
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
             if (txtProducto.Text != "" && txtPrecio.Text != "")
            {
                try
                {
                    comando.CommandText = "UPDATE Productos SET nombre_producto = '" + txtProducto.Text + "', Precio = '" + txtPrecio.Text + "' WHERE id_producto = " + dataGridView1.CurrentRow.Cells[0].Value.ToString();

                    comando.Connection = Conexion.conexion;
                    Conexion.conexion.Open();
                    comando.ExecuteNonQuery();
                    Conexion.conexion.Close();
                    MessageBox.Show("El registro ha sido actualizado exitosamente");
                    limpiar();
                    Cone();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.ToString());
                }
            }
            else MessageBox.Show("Los campos no deben quedar vacios");
        }
        public void Cone()
        {

            comando = new SqlCommand(@"Select * From Productos", Conexion.conexion);
            Conexion.conexion.Open();
            DataTable tabla = new DataTable();
            SqlDataAdapter usar = new SqlDataAdapter(comando);
            usar.Fill(tabla);
            dataGridView1.DataSource = tabla;
            Conexion.conexion.Close();

        }

        public void limpiar()
        {
            txtPrecio.Text = "";
            txtProducto.Text = "";
        }

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtProducto.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtPrecio.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }
    }
}
