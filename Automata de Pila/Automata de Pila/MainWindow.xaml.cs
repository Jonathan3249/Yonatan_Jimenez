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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Automata_de_Pila
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Funcionalidad funcionalidad;
        public MainWindow()
        {
            InitializeComponent();
        }


        private void cerrar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void moverVentana(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Generador_de_Gramatica utilizar = new Generador_de_Gramatica();
            this.Close();
            utilizar.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string[] cintaEntradas = this.txtentrada.Text.Split(',');
            this.txtPila.Text = "";          
            this.txtResultado2.Text = "";

            string temp;

            if (funcionalidad != null)
            {
                bool terminaPDA = true;
                this.label3.Content = "Estado: Realizando transiciones";
                funcionalidad.inicializar();
                string[] data;

                this.txtResultado2.Text = this.txtResultado2.Text
                        + "\n\t\tStack\t\t|\t\tFunción de transición\n";

                foreach (string entrada in cintaEntradas)
                {
                    esperar1Seg();
                    this.txtResultado2.Text = this.txtResultado2.Text
                       + "\t\t-----------------------------------------------------------------------\n";
                    string tempStack = funcionalidad.popStack();
                    if (funcionalidad.validateTransition(entrada, tempStack))
                    {

                        String eAnt = funcionalidad.lastState;

                        temp = funcionalidad.generateTransition(entrada, tempStack);
                        data = temp.Split(',');

                        this.txtResultado2.Text = this.txtResultado2.Text
                            + "\t\t"
                            + tempStack
                            + " -> "
                            + data[1]
                            + "\t\t|\t\t";

                        this.txtResultado2.Text = this.txtResultado2.Text
                            + "\t\tδ("
                            + eAnt + "," + entrada
                            + ") := "
                            + data[0]
                            + "\n";

                        funcionalidad.lastState = data[0];
                        funcionalidad.putInStack(data[1]);

                        this.txtPila.Text = funcionalidad.stackToString();
                        
                    }
                    else
                    {
                        funcionalidad.putInStack(tempStack);
                        MessageBox.Show("El autómata no esta definido correctamente\n"
                       + "Falta la definición para δ(" + funcionalidad.lastState + "," + entrada + "," + tempStack + ")",
                       "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        terminaPDA = false;
                        break;
                    }
                }


                bool aceptado = funcionalidad.esAceptado();

                if (terminaPDA && aceptado)
                {
                    
                    this.txtResultado2.Text = this.txtResultado2.Text
                        + "\n\t\tLa palabra: "
                        + this.txtentrada.Text.Replace(",", String.Empty)
                        + " Es aceptada por el PDA";

                }
                else
                {
                    
                    this.txtResultado2.Text = this.txtResultado2.Text
                        + "\n\t\tLa palabra: "
                        + this.txtentrada.Text.Replace(",", String.Empty)
                        + " Es rechazada por el PDA";
                }
                this.label3.Content = "Estado: listo";
            }
            




        }

        private void esperar1Seg()
        {
            long t0 = CurrentMillis.Millis;
            long t1;
            long t;
            do
            {
                t1 = CurrentMillis.Millis;
                t = t1 - t0;
            } while (t < 1000);

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            funcionalidad = null;
            limpiar();
        }

        private void limpiar()
        {

            this.txtentrada.Text = "";
            this.txtPila.Text = "";

            this.txtResultado2.Text = "";
            
        }

        
    }

    static class CurrentMillis
    {
        private static readonly DateTime Jan1St1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        /// <summary>Get extra long current timestamp</summary>
        public static long Millis { get { return (long)((DateTime.UtcNow - Jan1St1970).TotalMilliseconds); } }
    }

}
