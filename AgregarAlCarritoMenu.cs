using System.Collections.Generic;
using System;

namespace Tp2AAT {
    class AgregarAlCarritoMenu {
        
        private int Unidades = 0; //Cantidad de Unidades que se van a agregar al carrito o que ya estaban en el
        private Producto ProductoCompra; //Producto que se quiere agregar al carrito
        public int Seleccionado = 0;
        
        private (int Left, int Top) CantidadPosCursor; //Posicion del cursor en la cantidad de unidades
        private (int Left, int Top) PrecioPosCursor; //Posicion del cursor en el precio del producto

        //Metodo para darle al objeto el producto que se esta queriendo agregar al carrito y la cantidad de este
        //La cantidad si el producto ya estaba en el carrito, se inicializa con la cantidad existente, sino 0
        public AgregarAlCarritoMenu(Producto prod, int cantidad_inicial = 0) {
            ProductoCompra = prod;
            Unidades = cantidad_inicial;
        }

        //Metodo para recibir una tecla del usuario
        private static ConsoleKey EsperarTecla() {
            return Console.ReadKey(true).Key;
        }

        private void PrintCambioSeleccion(){
           
            const int CantOpciones = 3;
            int col_guardado = Console.CursorLeft;
            int fila_guardado = Console.CursorTop;

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
            
            Console.WriteLine($"¿Cuantas unidades de {this.ProductoCompra.nombre} quiere llevar?"); //Titulo - Producto
            Console.Write(" \t< "); // Primera Opcion - Elegir Cantidad
            CantidadPosCursor.Left = Console.CursorLeft;
            Console.WriteLine(Unidades + " > ");
            
            Console.Write(" \tAgregar al Carrito (");  // Segunda Opcion - Agregar
            PrecioPosCursor.Left = Console.CursorLeft;
            Console.WriteLine((Unidades * this.ProductoCompra.precio) + "$)");
            
            Console.WriteLine(" \tCancelar"); // Tercera Opcion - Cancelar
            CantidadPosCursor.Top = Console.CursorTop - 3;
            PrecioPosCursor.Top = Console.CursorTop - 2;
        }

        //Metodo para visualizar el cambio de cantidad de unidades a comprar
        private void PrintCambioUnidades(){
            
            (int Left, int Top) cursor_guardado = (Console.CursorLeft, Console.CursorTop); //Guardar la posicion del cursor actual
            
            Console.SetCursorPosition(CantidadPosCursor.Left, CantidadPosCursor.Top); //Poner el cursor en la posicion de la cantidad
            Console.Write(Unidades + " > "); //Actualizar la cantidad 
            
            Console.SetCursorPosition(this.PrecioPosCursor.Left, this.PrecioPosCursor.Top); //Poner el cursor en la posicion del precio
            Console.Write((this.Unidades * this.ProductoCompra.precio) + "$) "); //Actualizar el precio en base a la cantidad seleccionada
            
            Console.Write(new string(' ', Console.WindowWidth - Console.CursorLeft));  //Rellena el resto de la linea con espacios
            Console.SetCursorPosition(cursor_guardado.Left, cursor_guardado.Top); //Devuelve el cursor al lugar donde estaba
        }

        //Metodo para el manejo del menu
        public int EsperarCantidad() {
            
            PrintOpciones(); //Se muestran las opciones por primera vez
            Seleccionado = 0;
            PrintCambioSeleccion(); //se ubica el cursor en la primera opcion

            while(true){
                ConsoleKey tecla = EsperarTecla(); //Se espera una tecla del usuario
        
                switch (tecla) {

                //Con las flechas de arriba y abajo se puede mover por el menu
                case ConsoleKey.UpArrow: 
                        Seleccionado = Seleccionado - 1 >= 0 ? Seleccionado - 1 : 0;
                        PrintCambioSeleccion();
                        break;
          
                    case ConsoleKey.DownArrow:
                        Seleccionado = Seleccionado + 1 < 3 ? Seleccionado + 1 : 3 - 1;
                        PrintCambioSeleccion();
                        break;

                    //Con las flechas de izquierda y derecha cambia la cantidad
                    case ConsoleKey.LeftArrow:
                        if (Seleccionado == 0) {
                            Unidades = Unidades - 1 >= 0 ? Unidades - 1 : 0;
                            PrintCambioUnidades();
                        }
                        break;
                  
                    case ConsoleKey.RightArrow:
                        if (Seleccionado == 0) {
                            Unidades = Unidades + 1;
                            PrintCambioUnidades();
                        }
                        break;
                    //Cuando se presiona Enter se devuelve la cantidad que se selecciono o se sale del menu y se devuelve 0
                    case ConsoleKey.Enter:
                        if (Seleccionado == 1) {
                            return Unidades;
                        } 
                        else if (Seleccionado == 2) {
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
