using System;

namespace Tp2AAT {

  class Program {

    private const string SI_STR = "SI";
    private const string NO_STR = "NO";
    private static Tienda tienda = new Tienda();
    private static Carrito carrito = new Carrito();

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
        cambiarSeleccion(seleccionado, cant_opciones);
        ConsoleKey tecla = esperarTecla();
        switch (tecla) {
          case ConsoleKey.UpArrow:
            seleccionado = seleccionado - 1 >= 0 ? seleccionado - 1 : 0;
            break;
          case ConsoleKey.DownArrow:
            seleccionado = seleccionado + 1 < cant_opciones ? seleccionado + 1 : cant_opciones - 1;
            break;
          case ConsoleKey.Enter:
            ElegirAccion(seleccionado);
            seleccionado = 0;
            printMenuVendedor();
            break;
        }
      }
    }

    public static void Main() {
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