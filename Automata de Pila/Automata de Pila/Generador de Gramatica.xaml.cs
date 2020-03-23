using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Automata_de_Pila
{
    /// <summary>
    /// Lógica de interacción para Generador_de_Gramatica.xaml
    /// </summary>
    public partial class Generador_de_Gramatica : Window
    {
        public Pila pda { get; set; }
        private List<string> estados = new List<string>();
        private List<string> alfaEntrada = new List<string>();
        private List<string> alfaPila = new List<string>();
        private List<string> estadosFinales = new List<string>();
        private List<string> estadosFinalesDisp = new List<string>();
        private Dictionary<string, string> funcT = new Dictionary<string, string>();

        public Generador_de_Gramatica()
        {
            InitializeComponent();
            List<string> temp = new List<string>();
            this.cantEstados.ItemsSource = temp;

            for (int i = 2; i <= 15; i++)
            {
                temp.Add("" + i);
            }

            this.cantEstados.ItemsSource = temp;
            this.cantEstados.SelectedIndex = 0;
            if (validarEntradas()) { llenarListas(); }

        }

        private bool validarEntradas()
        {
            string pattern = @"^\w[,\w]{0,}$";
            string pattern2 = @",{2,}";  // las , no coinciden con # elementos

            Regex rgx = new Regex(pattern);
            Regex comaReg = new Regex(pattern2);

            string valor = (string)this.cantEstados.SelectedValue;
            estados = new List<string>();
            int cantiEstados = Convert.ToInt32(valor);
            estadosFinalesDisp = new List<string>();
            for (int i = 0; i < cantiEstados; i++)
            {
                estados.Add("q" + i);
                estadosFinalesDisp.Add("q" + i);
            }

            if (this.txtAlfabeto.Text.Length > 0)  // alfabeto de entrada
            {
                if (rgx.IsMatch(this.txtAlfabeto.Text) && !comaReg.IsMatch(this.txtAlfabeto.Text))
                {
                    this.txtAlfabeto.Text = this.txtAlfabeto.Text.Replace(" ", String.Empty).Trim();
                    if (this.txtAlfabeto.Text.EndsWith(",")) { this.txtAlfabeto.Text = this.txtAlfabeto.Text.Substring(0, this.txtAlfabeto.Text.Length - 1); }
                    string[] tempEstados = this.txtAlfabeto.Text.Split(',');
                    alfaEntrada = new List<string>();
                    foreach (string t in tempEstados)
                    {
                        if (!alfaEntrada.Contains(t)) { alfaEntrada.Add(t); }
                    }
                    if (alfaEntrada.Count == 0) { return false; }
                }
                else
                {
                    MessageBox.Show("El formato de " + this.txtAlfabeto.Text + " NO es valido.\n"
                        + "Favor revise el campo de estados.", "Formato Invalido",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }

            if (this.txtPila.Text.Length > 0)  // alfabeto de pila
            {
                if (rgx.IsMatch(this.txtPila.Text) && !comaReg.IsMatch(this.txtPila.Text))
                {
                    this.txtPila.Text = this.txtPila.Text.Replace(" ", String.Empty).Trim();
                    if (this.txtPila.Text.EndsWith(",")) { this.txtPila.Text = this.txtPila.Text.Substring(0, this.txtPila.Text.Length - 1); }
                    string[] tempEstados = this.txtPila.Text.Split(',');
                    alfaPila = new List<string>();
                    foreach (string t in tempEstados)
                    {
                        if (!alfaPila.Contains(t)) { alfaPila.Add(t); }
                    }
                    if (alfaPila.Count == 0) { return false; }
                }
                else
                {
                    MessageBox.Show("El formato de " + this.txtPila.Text + " NO es valido.\n"
                        + "Favor revise el campo de estados.", "Formato Invalido",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }


            return true;
        }

        private void llenarListas()
        {
            this.estaInicial.ItemsSource = estados;
            this.primEstado.ItemsSource = estados;
            this.cuartaTransicion.ItemsSource = estados;
            if (estados.Count > 0) { this.estaInicial.SelectedIndex = 0; }

            this.estFinales.Items.Clear();
            foreach (string temp in estadosFinalesDisp)
            {
                if (!estadosFinales.Contains(temp)) { this.estFinales.Items.Add(temp); }
            }

            this.segundoGrama.ItemsSource = alfaEntrada;

            if (!alfaPila.Contains("&")) { alfaPila.Add("&"); }
            this.inicialPila.ItemsSource = alfaPila;
            this.inicialPila.SelectedIndex = 0;
            this.tercerGrama.ItemsSource = alfaPila;
            this.quintaGrama.ItemsSource = alfaPila;

        }

        private void cerrar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
     

        private void txtAlfabeto_LostFocus(object sender, RoutedEventArgs e)
        {
            if (validarEntradas()) { llenarListas(); }
        }

        private void txtPila_LostFocus(object sender, RoutedEventArgs e)
        {
            if (validarEntradas()) { llenarListas(); }
        }

        

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            if (this.estFinales.SelectedIndex >= 0)
            {
                string temp = (string)this.estFinales.SelectedItem;

                if (!estadosFinales.Contains(temp)) { estadosFinales.Add(temp); }
                this.listView2.Items.Add(temp);
                this.estFinales.Items.RemoveAt(this.estFinales.SelectedIndex);
                estadosFinalesDisp.Remove(temp);

            }


        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            if (this.listView2.SelectedIndex >= 0)
            {
                string temp = (string)this.listView2.SelectedItem;

                if (!estadosFinalesDisp.Contains(temp)) { estadosFinalesDisp.Add(temp); }
                this.estFinales.Items.Add(temp);
                this.listView2.Items.RemoveAt(this.listView2.SelectedIndex);
                estadosFinales.Remove(temp);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            string temp = (string)this.listView1.SelectedItem;

            funcT.Remove(temp);
            funcT.Remove(temp.Substring(2, temp.LastIndexOf(")") - 2));
            renderizarListaFuncT();
        }

        private void renderizarListaFuncT()
        {
            List<string> listTemp = new List<string>();

            foreach (KeyValuePair<string, string> entry in funcT)
            {
                listTemp.Add("δ(" + entry.Key + ") = " + entry.Value);
            }
            this.listView1.ItemsSource = listTemp;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (this.primEstado.SelectedIndex >= 0 &&
                this.segundoGrama.SelectedIndex >= 0 &&
                this.cuartaTransicion.SelectedIndex >= 0 &&
                this.tercerGrama.SelectedIndex >= 0 &&
                this.quintaGrama.SelectedIndex >= 0)
            {
                string key = (string)this.primEstado.SelectedItem
                            + (string)this.segundoGrama.SelectedItem
                            + (string)this.tercerGrama.SelectedItem;

                funcT[key] = (string)this.cuartaTransicion.SelectedItem + "," + (string)this.quintaGrama.SelectedItem;
            }
            renderizarListaFuncT();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (estados.Count > 0 && estadosFinales.Count > 0
                && alfaEntrada.Count > 0 && alfaPila.Count > 0
                && funcT.Count > 0)
            {
                llenarListas();

                pda = new Pila();
                pda.alfaEntrada = alfaEntrada;
                pda.alfaPila = alfaPila;
                pda.estadoInicial = (string)this.estaInicial.SelectedItem;
                pda.estados = estados;
                pda.estadosFinales = estadosFinales;
                pda.funcTransicion = funcT;
                pda.simboloInicialPila = (string)this.inicialPila.SelectedItem;
                MainWindow utilizar = new MainWindow();
                
                this.Close();
                utilizar.Show();
            }
            else
            {
                MessageBox.Show("Aún faltan campos por definir", "Aviso", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void cantEstados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (validarEntradas()) { llenarListas(); }
        }

        private void mover(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
