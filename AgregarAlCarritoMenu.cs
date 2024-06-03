using System.Collections.Generic;
using System;

namespace Tp2AAT {
    class AgregarAlCarritoMenu {
        private int Seleccionado = 0;
        private int Unidades = 0;
        private Producto ProductoCompra;
        private (int Left, int Top) CantidadPosCursor;
        private (int Left, int Top) PrecioPosCursor;

        private static ConsoleKey EsperarTecla() {
            return Console.ReadKey(true).Key;
        }

        public AgregarAlCarritoMenu(Producto prod) {
            this.ProductoCompra = prod;
        }

        private void PrintCambioSeleccion(){
            int col_guardado = Console.CursorLeft;
            int fila_guardado = Console.CursorTop;
            int CantOpciones = 3;

            for (int i = 0; i < CantOpciones; i++) {
                int fila_a_cambiar = fila_guardado - CantOpciones + i;
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
            Console.WriteLine($"¿Cuantas unidades de {this.ProductoCompra.nombre} quiere llevar?");
            Console.Write(" \t< ");
            this.CantidadPosCursor.Left = Console.CursorLeft;
            Console.WriteLine("0 > ");
            Console.Write(" \tAgregar al Carrito (0");
            this.PrecioPosCursor.Left = Console.CursorLeft;
            Console.WriteLine("$)");
            Console.WriteLine(" \tCancelar");
        }

        private void PrintCambioUnidades(){
            this.CantidadPosCursor.Top = Console.CursorTop - 3;
            this.PrecioPosCursor.Top = Console.CursorTop - 2;
            (int Left, int Top) cursor_guardado = (Console.CursorLeft, Console.CursorTop); //guardar la posicion del cursor actual

            Console.SetCursorPosition(this.CantidadPosCursor.Left, this.CantidadPosCursor.Top);
            Console.Write(this.Unidades + " > "); //actualizar la cantidad 
            Console.SetCursorPosition(this.PrecioPosCursor.Left, this.PrecioPosCursor.Top);
            Console.Write((this.Unidades * this.ProductoCompra.precio) + "$) "); //actualizar el precio 
            Console.Write(new string(' ', Console.WindowWidth - Console.CursorLeft));  // Rellena el resto de la linea con espacios
            Console.SetCursorPosition(cursor_guardado.Left, cursor_guardado.Top); // devuelve el cursor al lugar donde estaba
        }

        public int EsperarCantidad() {
            this.PrintOpciones();
            this.Seleccionado = 0;
            this.PrintCambioSeleccion();

            while(true){
                ConsoleKey tecla = EsperarTecla();
        
                switch (tecla) {
          
                    case ConsoleKey.UpArrow:
                        this.Seleccionado = this.Seleccionado - 1 >= 0 ? this.Seleccionado - 1 : 0;
                        cambiarSeleccion();
                        break;
          
                    case ConsoleKey.DownArrow:
                        this.Seleccionado = this.Seleccionado + 1 < 3 ? this.Seleccionado + 1 : 3 - 1;
                        cambiarSeleccion();
                        break;
          
                    case ConsoleKey.LeftArrow:
                        if (this.Seleccionado == 0) {
                            this.Unidades = this.Unidades - 1 >= 0 ? this.Unidades - 1 : 0;
                            this.PrintCambioUnidades();
                        }
                        break;
                  
                    case ConsoleKey.RightArrow:
                        if (this.Seleccionado == 0) {
                            this.Unidades = this.Unidades + 1;
                            this.PrintCambioUnidades();
                        }
                        break;
                  
                    case ConsoleKey.Enter:
                        if (this.Seleccionado == 1) {
                            return this.Unidades;
                        } else if (this.Seleccionado == 2) {
                            return 0;
                        }
                        break;
                    
                    default:
                        break;
                }
            }
        }          
    }
}          