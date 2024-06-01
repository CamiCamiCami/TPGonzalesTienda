using System;
using System.Collections.Generic;

namespace Tp2AAT {

  class Program {

    private const string SI_STR = "SI";
    private const string NO_STR = "NO";
    private static Tienda tienda = AgregarProductosDefault(new Tienda());
    private static Carrito carrito = new Carrito();


    private static Tienda AgregarProductosDefault(Tienda _tienda){
      _tienda.AgregarProducto(new Producto("Desodorante", 5000, 50));
      _tienda.AgregarProducto(new Producto("Vaso", 1000, 13));
      _tienda.AgregarProducto(new Producto("Lampara", 30000, 55));
      _tienda.AgregarProducto(new Producto("Vino", 10000, 38));
      _tienda.AgregarProducto(new Producto("Celular", 1000000, 74));
      _tienda.AgregarProducto(new Producto("Monitor", 500000, 67));
      _tienda.AgregarProducto(new Producto("Panel de Vidro", 60000, 94));
      _tienda.AgregarProducto(new Producto("Cinturon", 8000, 27));
      _tienda.AgregarProducto(new Producto("Block de Notas", 3000, 43));
      _tienda.AgregarProducto(new Producto("Tablon", 10000, 85));
      _tienda.AgregarProducto(new Producto("Pajita", 700, 34));
      _tienda.AgregarProducto(new Producto("Coca-Cola", 1500, 52));
      _tienda.AgregarProducto(new Producto("Lapicera", 900, 91));
      _tienda.AgregarProducto(new Producto("Cinta Adhesiva", 800, 16));
      _tienda.AgregarProducto(new Producto("Zapatilla", 7500, 1));
      _tienda.AgregarProducto(new Producto("Paraguas", 9200, 7));
      _tienda.AgregarProducto(new Producto("Polo", 35000, 12));
      _tienda.AgregarProducto(new Producto("Ventilador", 40000, 73));
      _tienda.AgregarProducto(new Producto("Almohada", 6800, 29));
      _tienda.AgregarProducto(new Producto("Sabana", 7200, 23));
      _tienda.AgregarProducto(new Producto("Mochila", 6000, 89));
      return _tienda;
    }  

    private static bool esVendedor(){
      while (true){
        Console.WriteLine("Quiere entrar al sistema como vendedor?");
        string respuesta = Console.ReadLine();

        respuesta = respuesta.ToUpper();  
        if (respuesta == SI_STR || respuesta == NO_STR){
          return respuesta == SI_STR;
        }

        Console.WriteLine("Respuesta no valida, intente de nuevo");
      }
    }

    private static ConsoleKey esperarTecla() {
      return Console.ReadKey(true).Key;
    }

    private static void printMenuVendedor(){
      Console.WriteLine("¿Que opcion quieres realizar?");
      Console.WriteLine(" \tAgregar producto");
      Console.WriteLine(" \tEliminar producto");
      Console.WriteLine(" \tVer listado de productos y stock");
      Console.WriteLine(" \tVer dinero en caja");
      Console.WriteLine(" \tSalir");
    }

    private static void cambiarSeleccion(int seleccionado, int cant_opciones){
      int col = Console.CursorLeft;
      int fila = Console.CursorTop;
      for (int i = 0; i < cant_opciones; i++){
        int fila_a_cambiar = fila - cant_opciones + i;
        Console.SetCursorPosition(0, fila_a_cambiar);
        if (seleccionado == i) {
          Console.Write(">");
        } else {
          Console.Write(" ");
        }
      }
          
      Console.SetCursorPosition(col, fila);
    }

    private static void AgregarProductoMenu(){
      Console.WriteLine("Ingrese los datos del Producto");
      Console.Write("(Obligatorio)\tNombre: ");
      string nombre = Console.ReadLine();
      Console.Write("(Obligatorio)\tCosto: ");
      string costo = Console.ReadLine();
      Console.Write("\t\tStock: ");
      string stock = Console.ReadLine();

      Producto prod = new Producto(nombre, Double.Parse(costo), Int64.Parse(stock));
      tienda.AgregarProducto(prod);
    }

    private static void EliminarProductoMenu(){
      Console.Write("Nombre del producto a eliminar:");
      string nombre = Console.ReadLine();

      tienda.EliminarProducto(nombre);
    }

    private static void printStock(){
      string stock = tienda.ContenidosComoString();
      Console.Write(stock);
    }

    private static void ElegirAccion(int seleccionado) {
      switch(seleccionado) {
        case 0:
          AgregarProductoMenu();
          break;
        case 1:
          EliminarProductoMenu();
          break;
        case 2:
          printStock();
          break;
        case 3:
          Console.WriteLine($"Hay {tienda.caja}$ en la caja");
          break;
        case 4:
          Environment.Exit(0);
          break;
      }
    }

    private static void MenuVendedor(){
      const int cant_opciones = 5;
      int seleccionado = 0;
      printMenuVendedor();

      while(true){
        ConsoleKey tecla = esperarTecla();
        switch (tecla) {
          case ConsoleKey.UpArrow:
            seleccionado = seleccionado - 1 >= 0 ? seleccionado - 1 : 0;
            cambiarSeleccion(seleccionado, cant_opciones);
            break;
          case ConsoleKey.DownArrow:
            seleccionado = seleccionado + 1 < cant_opciones ? seleccionado + 1 : cant_opciones - 1;
            cambiarSeleccion(seleccionado, cant_opciones);
            break;
          case ConsoleKey.Enter:
            ElegirAccion(seleccionado);
            seleccionado = 0;
            printMenuVendedor();
            cambiarSeleccion(seleccionado, cant_opciones);
            break;
          default:
            break;
        }
      }
    }

    private static void printMenuCliente(List<string> productos){
      Console.WriteLine("¿Que quiere Agregar al carrito?");

      foreach (string producto in productos) {
        Console.WriteLine(" \t" + producto);
      }
      Console.WriteLine(" \tCarrito");
      Console.WriteLine(" \tComprar");
      Console.WriteLine(" \tSalir");
    }

    private static void CambiarCantidad((int Left, int Top) locacion_cantidad, (int Left, int Top) locacion_precio, int cantidad, double precio){
      (int Left, int Top) cursor_guardado = (Console.CursorLeft, Console.CursorTop);
      Console.SetCursorPosition(locacion_cantidad.Left, locacion_cantidad.Top);
      Console.Write(cantidad + " > ");
      Console.SetCursorPosition(locacion_precio.Left, locacion_precio.Top);
      Console.Write(cantidad * precio + "$) ");
      Console.Write(new string(' ', Console.WindowWidth - Console.CursorLeft)); 
      Console.SetCursorPosition(cursor_guardado.Left, cursor_guardado.Top);
    }

    private static void AgregarAlCarritoMenu(string nombre){
      Producto prod = tienda.ConsultarProducto(nombre);
      int cantidad_compra = 0;
      int seleccionado = 0;
      int cantidad_left_pos, precio_left_pos;
      Console.WriteLine("");
      Console.WriteLine($"{nombre}\t\t{prod.precio}$");

      Console.Write(" \t< "); cantidad_left_pos = Console.CursorLeft ; Console.WriteLine(cantidad_compra + " >");
      Console.Write(" \tAgregar al carrito ("); precio_left_pos = Console.CursorLeft; Console.WriteLine((prod.precio * cantidad_compra) + "$)");
      Console.WriteLine(" \tCancelar");

      cambiarSeleccion(seleccionado, 3);
      while(true) {
        ConsoleKey tecla = esperarTecla();
        switch (tecla) {
          case ConsoleKey.UpArrow:
            seleccionado = seleccionado - 1 >= 0 ? seleccionado - 1 : 0;
            cambiarSeleccion(seleccionado, 3);
            break;
          case ConsoleKey.DownArrow:
            seleccionado = seleccionado + 1 < 3 ? seleccionado + 1 : 3 - 1;
            cambiarSeleccion(seleccionado, 3);
            break;
          case ConsoleKey.LeftArrow:
            if (seleccionado == 0) {
              cantidad_compra = cantidad_compra - 1 >= 0 ? cantidad_compra - 1 : 0;
              CambiarCantidad((cantidad_left_pos, Console.CursorTop - 3), (precio_left_pos, Console.CursorTop - 2), cantidad_compra, prod.precio);
            }
            break;
          case ConsoleKey.RightArrow:
            if (seleccionado == 0) {
              cantidad_compra = cantidad_compra + 1;
              CambiarCantidad((cantidad_left_pos, Console.CursorTop - 3), (precio_left_pos, Console.CursorTop - 2), cantidad_compra, prod.precio);
            }
            break;
          case ConsoleKey.Enter:
            break;
          default:
            break;
        }
      } 
    }

    private static void MenuCliente() {
      const int max_pagina = 10;
      List<string> productos = tienda.ConsultarNombres();
      int cant_opciones = productos.Count + 3;
      int seleccionado = 0;
      int pagina = 0;

      printMenuCliente(productos);
      cambiarSeleccion(0, cant_opciones);

      while(true) {
        ConsoleKey tecla = esperarTecla();

        switch (tecla) {
          case ConsoleKey.UpArrow:
            seleccionado = seleccionado - 1 >= 0 ? seleccionado - 1 : 0;
            cambiarSeleccion(seleccionado, cant_opciones);
            break;
          case ConsoleKey.DownArrow:
            seleccionado = seleccionado + 1 < cant_opciones ? seleccionado + 1 : cant_opciones - 1;
            cambiarSeleccion(seleccionado, cant_opciones);
            break;
          case ConsoleKey.Enter:
            if (seleccionado < productos.Count) {
              AgregarAlCarritoMenu(productos[seleccionado]);
            } else if (seleccionado == cant_opciones - 2) {

            } else if (seleccionado == cant_opciones - 1) {

            } else {

            }
            break;
          default:
            break;
        }

      }


    }

    public static void Main() {
      Console.WriteLine("Bienvenido a la tienda Mauricio Shop");
      string respuesta = "";
      if (esVendedor()) {
        MenuVendedor();
      } else { 
        MenuCliente();
      }
    }
  }
}