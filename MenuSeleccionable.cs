using System.Collections.Generic;
using System;

namespace Tp2AAT {
    class MenuSeleccionable {
        private List<string> Opciones = new List<string>();
        public int CantOpciones { get; private set; }
        private string Titulo;
        private int Seleccionado = 0;

        private static ConsoleKey EsperarTecla() {
            return Console.ReadKey(true).Key;
        }

        public MenuSeleccionable(string titulo_menu, List<string> opciones_menu) {
            foreach(string opc in opciones_menu) {
                this.Opciones.Add(opc);
            }
            this.CantOpciones = opciones_menu.Count;
            this.Titulo = titulo_menu;
        }

        private void PrintCambioSeleccion(){
            int col_guardado = Console.CursorLeft;
            int fila_guardado = Console.CursorTop;

            for (int i = 0; i < this.CantOpciones; i++) {
                int fila_a_cambiar = fila_guardado - this.CantOpciones + i;
                Console.SetCursorPosition(0, fila_a_cambiar);
                if (this.Seleccionado == i) {
                    Console.Write(">");
                } else {
                    Console.Write(" ");
                }
            }
            Console.SetCursorPosition(col_guardado, fila_guardado);
        }

        private void PrintOpciones() {
            Console.WriteLine(this.Titulo);
            foreach(string opc in this.Opciones) {
                Console.Write(" \t");
                Console.WriteLine(opc);
            }
        }

        public string EsperarEleccion() {
            this.Seleccionado = 0;
            this.PrintOpciones();

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