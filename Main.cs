using System;

namespace Tp2AAT {

  class Program {

    private const string SI_STR = "SI";
    private const string NO_STR = "NO";
    private static Tienda tienda = new Tienda();
    private static Carrito carrito = new Carrito();


    private static void AgregarProductosDefault(){
      tienda.agregarProducto(new Producto("Desodorante", 5000, 50));
      tienda.agregarProducto(new Producto("Vaso", 1000, 13));
      tienda.agregarProducto(new Producto("Lampara", 30000, 55));
      tienda.agregarProducto(new Producto("Vino", 10000, 38));
      tienda.agregarProducto(new Producto("Celular", 1000000, 74));
      tienda.agregarProducto(new Producto("Monitor", 500000, 67));
      tienda.agregarProducto(new Producto("Panel de Vidro", 60000, 94));
      tienda.agregarProducto(new Producto("Cinturon", 8000, 27));
      tienda.agregarProducto(new Producto("Block de Notas", 3000, 43));
      tienda.agregarProducto(new Producto("Tablon", 10000, 85));
      tienda.agregarProducto(new Producto("Pajita", 700, 34));
      tienda.agregarProducto(new Producto("Coca-Cola", 1500, 52));
      tienda.agregarProducto(new Producto("Lapicera", 900, 91));
      tienda.agregarProducto(new Producto("Cinta Adhesiva", 800, 16));
      tienda.agregarProducto(new Producto("Zapatilla", 7500, 1));
      tienda.agregarProducto(new Producto("Paraguas", 9200, 7));
      tienda.agregarProducto(new Producto("Polo", 35000, 12));
      tienda.agregarProducto(new Producto("Ventilador", 40000, 73));
      tienda.agregarProducto(new Producto("Almohada", 6800, 29));
      tienda.agregarProducto(new Producto("Sabana", 7200, 23));
      tienda.agregarProducto(new Producto("Mochila", 6000, 89));
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
      string[] lineas = new string[] {"¿Que opcion quieres realizar?\n", " \tAgregar producto\n", " \tEliminar producto\n", " \tVer listado de productos y stock\n", " \tVer dinero en caja\n", " \tSalir\n"};
      int chars_escritos = 0;

      foreach (string linea in lineas) {
        chars_escritos += linea.Length;
        Console.Write(linea);
      }
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
      tienda.agregarProducto(prod);
    }

    private static void EliminarProductoMenu(){
      Console.Write("Nombre del producto a eliminar:");
      string nombre = Console.ReadLine();

      tienda.eliminarProducto(nombre);
    }

    private static void printStock(){
      string stock = tienda.contenidosComoString();
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

    private static void menuVendedor(){
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
        }
      }
    }

    private static void printMenuCliente(List<string> productos){

      foreach (string producto in productos) {
        Console.WriteLine(" \t" + producto);
      }
      Console.WriteLine(" \tCarrito");
      Console.WriteLine(" \tComprar");
      Console.WriteLine(" \tSalir");
    }

    private static void menuCliente() {
      const int max_pagina = 10;
      int seleccionado = 0;
      int pagina = 0;

      List<string> productos = tienda.ConsultarNombres();
      
      Console.WriteLine("¿Que desea comprar?");



    }

    public static void Main() {
      AgregarProductosDefault();
      Console.WriteLine("Bienvenido a la tienda Mauricio Shop");
      string respuesta = "";
      if (esVendedor()) {
        while(respuesta.ToUpper() != "SALIR") {  
          menuVendedor();
            
          if(respuesta == "1"){
            Console.WriteLine("Ingrese el nombre del producto");
            string nombreProd = Console.ReadLine();
          }
          if(respuesta == "2");
          if(respuesta == "3");
          if(respuesta == "4");
        }       
      } else { 
            
      }
    }
  }
}