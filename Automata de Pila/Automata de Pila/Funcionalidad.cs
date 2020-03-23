using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automata_de_Pila
{
   public class Funcionalidad
    {
        
        public  Pila pda { get; set; }
        public int delay { get; set; }
        public List<string> stack { get; set; }
        public string lastState { get; set; } // estado en que ha quedado el automata

        public Funcionalidad(Pila pda)
        {
            // TODO: Complete member initialization
            this.pda = pda;
        }

        public Funcionalidad() { }

        public void putInStack(string data)
        {
            stack.Insert(stack.Count, data);
        }

        public string popStack()
        {
            if (stack.Count > 0)
            {
                string temp = stack.Last();
                stack.RemoveAt(stack.Count - 1);
                return temp;
            }
            else
            {
                return "&";
            }

        }

        public void inicializar()
        {
            stack = new List<string>();
            stack.Add(pda.simboloInicialPila);
            this.lastState = pda.estadoInicial;
        }

        public bool validateTransition(string entrada, string elementoPila)
        {
            return pda.funcTransicion.ContainsKey(lastState + entrada + elementoPila);
        }

        public string generateTransition(string entrada, string elementoPila)
        {
            string temp = pda.funcTransicion[lastState + entrada + elementoPila];
            lastState = temp;                                   // actualizo al ultimo estado
            return temp;
        }

        public string stackToString()
        {
            string stackString = "";

            foreach (string s in stack)
            {
                stackString = stackString + s + ",";
            }

            return stackString.Substring(0, stackString.Length - 1);
        }

        public bool esAceptado()
        {
            return pda.estadosFinales.Contains(lastState);
        }

        public List<string> obtenerListaEstados()
        {

            if (this.pda != null)// si se creo 
            {
                if (this.pda.estados.Count > 0) // si tiene estados
                {
                    return this.pda.estados;
                }
            }
            return new List<string>();
        }
    }
}
