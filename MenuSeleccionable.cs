using System;
using System.Collections.Generic;

namespace Tp2AAT {
    abstract class MenuSeleccionable {

        public int CantOpciones;

        public int Seleccionado = 0;

        public static ConsoleKey EsperarTecla() {
            return Console.ReadKey(true).Key;
        }

        public void PrintCambioSeleccion(){
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

        public abstract void PrintOpciones();

    }
}