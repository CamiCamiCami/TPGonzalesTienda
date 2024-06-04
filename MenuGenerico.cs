using System.Collections.Generic;
using System;

namespace Tp2AAT {
    class MenuGenerico : MenuSeleccionable {
        private List<string> Opciones = new List<string>();
        private string Titulo;

        public MenuGenerico(string titulo_menu, List<string> opciones_menu) {
            foreach(string opc in opciones_menu) {
                this.Opciones.Add(opc);
            }
            this.CantOpciones = opciones_menu.Count;
            this.Titulo = titulo_menu;
        }
        public override void PrintOpciones() {
            Console.WriteLine(this.Titulo);
            foreach(string opc in this.Opciones) {
                Console.Write(" \t");
                Console.WriteLine(opc);
            }
        }

        public string EsperarEleccion() {
            this.Seleccionado = 0;
            this.PrintOpciones();
            this.PrintCambioSeleccion();

            while(true){
                ConsoleKey tecla = EsperarTecla();
        
                switch (tecla) {
                    case ConsoleKey.UpArrow:
                        this.Seleccionado = this.Seleccionado - 1 >= 0 ? this.Seleccionado - 1 : 0;
                        PrintCambioSeleccion();
                        break;
          
                    case ConsoleKey.DownArrow:
                        this.Seleccionado = this.Seleccionado + 1 < this.CantOpciones ? this.Seleccionado + 1 : this.CantOpciones - 1;
                        PrintCambioSeleccion();
                        break;
          
                    case ConsoleKey.Enter:
                        return this.Opciones[this.Seleccionado];
          
                    default:
                        break;
                }
            }
        }
    }
}