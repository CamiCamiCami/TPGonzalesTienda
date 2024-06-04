using System.Collections.Generic;
using System;

namespace Tp2AAT {
    class AgregarAlCarritoMenu : MenuSeleccionable {
        private int Unidades = 0;
        private Producto ProductoCompra;
        private (int Left, int Top) CantidadPosCursor;
        private (int Left, int Top) PrecioPosCursor;

        public AgregarAlCarritoMenu(Producto prod) {
            this.ProductoCompra = prod;
            this.CantOpciones = 3;
        }

        public override void PrintOpciones() {
            // Titulo
            Console.WriteLine($"¿Cuantas unidades de {this.ProductoCompra.nombre} quiere llevar?");
            // Primera Opcion - Elegir Cantidad
            Console.Write(" \t< ");
            this.CantidadPosCursor.Left = Console.CursorLeft;
            Console.WriteLine("0 > ");
            // Segunda Opcion - Agregar 
            Console.Write(" \tAgregar al Carrito (");
            this.PrecioPosCursor.Left = Console.CursorLeft;
            Console.WriteLine("0$)");
            // Tercera Opcion - Cancelar
            Console.WriteLine(" \tCancelar");
        }

        public void PrintCambioUnidades(){
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
                        this.PrintCambioSeleccion();
                        break;
          
                    case ConsoleKey.DownArrow:
                        this.Seleccionado = this.Seleccionado + 1 < 3 ? this.Seleccionado + 1 : 3 - 1;
                        this.PrintCambioSeleccion();
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