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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Cone();
        }
        
        public static SqlCommand comando;

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



        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    DialogResult respuesta = MessageBox.Show("Esta seguro de eliminar?",
                                    "confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (respuesta == DialogResult.Yes)
                    {
                        comando.CommandText = "Delete From Productos Where id_producto = " + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "; ";
                        comando.Connection = Conexion.conexion;
                        Conexion.conexion.Open();
                        comando.ExecuteNonQuery();
                        Conexion.conexion.Close();
                        MessageBox.Show("Cliente Eliminado Exitosamente");
                        Cone();
                    }
                    else if (respuesta == DialogResult.No) Cone();
                }
                catch (Exception oo)
                {
                    Conexion.conexion.Close();
                    MessageBox.Show(oo.ToString());
                }
            }
            else MessageBox.Show("Seleccione algun dato de la tabla");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    DialogResult respuesta = MessageBox.Show("Esta seguro de eliminar?",
                                    "confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (respuesta == DialogResult.Yes)
                    {
                        comando.CommandText = "Delete From Productos Where id_producto = " + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "; ";
                        comando.Connection = Conexion.conexion;
                        Conexion.conexion.Open();
                        comando.ExecuteNonQuery();
                        Conexion.conexion.Close();
                        MessageBox.Show("Cliente Eliminado Exitosamente");
                        Cone();
                    }
                    else if (respuesta == DialogResult.No) Cone();
                }
                catch (Exception oo)
                {
                    Conexion.conexion.Close();
                    MessageBox.Show(oo.ToString());
                }
            }
            else MessageBox.Show("Seleccione algun dato de la tabla");

        }
    }
}
