using System.Collections.Generic;
using System;

namespace Tp2AAT {
    
    class MenuSeleccionable {
        
        private List<string> Opciones = new List<string>();
        private string Titulo;
        public int CantOpciones {get; private set; }
        public int Seleccionado = 0;

        //Metodo para determinar el titulo y las opciones
        public MenuSeleccionable(string titulo_menu, List<string> opciones_menu) {
            foreach(string opc in opciones_menu) {
                Opciones.Add(opc);
            }
            CantOpciones = opciones_menu.Count;
            Titulo = titulo_menu;
        }

        //Metodo para recibir una tecla del usuario
        private static ConsoleKey EsperarTecla() {
            return Console.ReadKey(true).Key;
        }

        //Metodo para imprimir el cursor del sistema ">"
        private void PrintCambioSeleccion(){
            
            int col_guardado = Console.CursorLeft; //Guardar la columna actual del cursor, antes de cambiarla
            int fila_guardado = Console.CursorTop; //Guardar la fila actual del cursor, antes de cambiarla

            for (int i = 0; i < CantOpciones; i++) { //Buscamos la opcion que requiere el usuario y se imprime el cursor
                int fila_a_cambiar = fila_guardado - CantOpciones + i;
                Console.SetCursorPosition(0, fila_a_cambiar);
                if (Seleccionado == i) {
                    Console.Write(">");
                } else {
                    Console.Write(" ");
                }
            }
            
            Console.SetCursorPosition(col_guardado, fila_guardado); //Volvemos el Cursor a la posicion original
        }    

        //Metodo para imprimir las opciones del sistema, dependiendo del estado del sistema
        private void PrintOpciones() {
            Console.WriteLine(Titulo);
            foreach(string opc in Opciones) {
                Console.Write(" \t");
                Console.WriteLine(opc);
            }
        }

        //Metodo para manejarse por el menu en el que estemos
        public string EsperarEleccion() {
            
            Seleccionado = 0;
            PrintOpciones(); //Se muestran las opciones del sistema
            PrintCambioSeleccion(); //Se muestra el cursor del sistema por primera vez

            while(true){
                ConsoleKey tecla = EsperarTecla(); //Se obtiene la tecla del usuario
        
                switch (tecla) {
                case ConsoleKey.UpArrow: //Si se presiona la flecha hacia arriba, se llama otra vez al cursor del sistema
                        Seleccionado = Seleccionado - 1 >= 0 ? Seleccionado - 1 : 0;
                        PrintCambioSeleccion();
                        break;
          
                case ConsoleKey.DownArrow: //Si se presiona la flecha hacia abajo, se llama otra vez al cursor del sistema
                        Seleccionado = Seleccionado + 1 < CantOpciones ? Seleccionado + 1 : CantOpciones - 1;
                        PrintCambioSeleccion();
                        break;
          
                case ConsoleKey.Enter: //Si se presiona Enter, se retorna la opcion seleccionada
                        return Opciones[Seleccionado];
          
                    default:
                        break;
                }
            }
        }
    }
}
