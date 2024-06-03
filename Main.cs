using System.Collections.Generic;
using System;


namespace Tp2AAT {

  class Program {

    //Creamos los objetos de tienda y carrito para el uso de programa
    private static Tienda tienda = Tienda.AgregarProductosDefault(new Tienda());
    private static Carrito carrito = new Carrito();
    
    //Dentro del menu interactivo, esta funcion se ejecuta primero para preguntar al usuario si es vendedor o comprador
    private static bool esVendedor(){
      while (true){
        Console.WriteLine("Quiere entrar al sistema como vendedor?");
        string respuesta = Console.ReadLine();
        respuesta = respuesta.ToUpper();

        if (respuesta == "SI" || respuesta == "NO"){
          return respuesta == "SI";
        }

        Console.WriteLine("Respuesta no valida, intente de nuevo");
      }
    }

    //Con esta funcion esperamos a que el usuario presione alguna tecla para continuar
    private static ConsoleKey esperarTecla() {
      return Console.ReadKey(true).Key;
    }

    //Con esta funcion unicamente mostramos el menu principal de vendedor
    private static void printMenuVendedor(){
      Console.WriteLine("¿Que opcion quieres realizar?");
      Console.WriteLine(" \tAgregar producto");
      Console.WriteLine(" \tEliminar producto");
      Console.WriteLine(" \tVer listado de productos y stock");
      Console.WriteLine(" \tVer dinero en caja");
      Console.WriteLine(" \tSalir");
    }

    //Cambiar el cursor (>) de lugar para poder manejarse por el menu
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

    //Agregar productos al sistema
    private static void AgregarProductoMenu(){
      Console.WriteLine("Ingrese los datos del Producto");
      Console.Write("(Obligatorio)\tNombre: ");
      string nombre = Console.ReadLine();
      Console.Write("(Obligatorio)\tCosto: ");
      string costo = Console.ReadLine();
      Console.Write("\t\tStock: ");
      string stock = Console.ReadLine();

      Producto prod = new Producto(nombre, float.Parse(costo), int.Parse(stock));
      tienda.AgregarProducto(prod);
    }

    //Eliminar productos del sistema
    private static void EliminarProductoMenu(){
      Console.Write("Nombre del producto a eliminar:");
      string nombre = Console.ReadLine();

      tienda.EliminarProducto(nombre);
      Console.WriteLine($"El producto {nombre} ha sido eliminado del sistema");
    }

    //Esta funcion muestra los productos y stock del sistema
    private static void printStock(){
      string stock = tienda.ObtenerStock();
      Console.Write(stock);
    }

    //Manejar los casos para el modo vendedor
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

    //Llamar a la funcion que muestra el menu y manejar la respuesta del usuario con el teclado
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


  
    //Mostrar el menu interactivo del cliente
    private static void printMenuCliente(List<string> productos){
      Console.WriteLine("¿Que quiere Agregar al carrito?");

      foreach (string producto in productos) {
        Console.WriteLine(" \t" + producto);
      }
      Console.WriteLine(" \tIr a caja");
      Console.WriteLine(" \tSalir");
    }

    //Metodo para cambiar la cantidad y el precio del producto para agregar al carrito
    private static void CambiarCantidad((int Left, int Top) locacion_cantidad, (int Left, int Top) locacion_precio, int cantidad, double precio){
      (int Left, int Top) cursor_guardado = (Console.CursorLeft, Console.CursorTop); //guardar la posicion del cursor actual 
      Console.SetCursorPosition(locacion_cantidad.Left, locacion_cantidad.Top);
      Console.Write(cantidad + " > "); //actualizar la cantidad 
      Console.SetCursorPosition(locacion_precio.Left, locacion_precio.Top);
      Console.Write(cantidad * precio + "$) "); //actualizar el precio 
      //Console.Write(new string(' ', Console.WindowWidth - Console.CursorLeft)); 
      Console.SetCursorPosition(cursor_guardado.Left, cursor_guardado.Top);
    }

    //Metodo para agregar productos al carrito
    private static void AgregarAlCarritoMenu(string nombre){
      Producto prod = tienda.ConsultarProducto(nombre); //Buscar el producto en la lista de productos de la tienda
      int cantidad_compra = 0;
      int seleccionado = 0;
      int pos_cantidad, pos_precio;
      
      Console.WriteLine("");
      Console.WriteLine($"{nombre}\t\t{prod.precio}$");
      Console.Write(" \t< "); 
      pos_cantidad= Console.CursorLeft ; //Guarda la posicion del cursor donde se ingreso la cantidad de compra 
      Console.WriteLine(cantidad_compra + " >");
      Console.Write(" \tAgregar al carrito ("); 
      pos_precio = Console.CursorLeft; //Guarda la posicion del cursor donde se ingresa el precio del total
      Console.WriteLine((prod.precio * cantidad_compra) + "$)");
      Console.WriteLine(" \tCancelar");

      cambiarSeleccion(seleccionado, 3);
      int bandera_salida = 0;
      
      while(bandera_salida == 0) {
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
              CambiarCantidad((pos_cantidad, Console.CursorTop - 3), (pos_precio, Console.CursorTop - 2), cantidad_compra, prod.precio);
            }
            break;
          
          case ConsoleKey.RightArrow:
            if (seleccionado == 0) {
              cantidad_compra = cantidad_compra + 1;
              CambiarCantidad((pos_cantidad, Console.CursorTop - 3), (pos_precio, Console.CursorTop - 2), cantidad_compra, prod.precio);
            }
            break;
          
          case ConsoleKey.Enter:
            if (seleccionado == 1){
              carrito.agregarProducto(prod, cantidad_compra);
              bandera_salida =+ 1;
              }
            if(seleccionado == 2){
              bandera_salida =+ 1;
            }
            break;
          
          default:
            break;
        }
      } 
    }

    private static void CajaMenu(Carrito carrito){
      float vuelto = 0;
      
      Console.WriteLine($"Subtotal: {carrito.subtotal()}$");
      Console.Write("Con cuanto va a pagar?");
      string pago = Console.ReadLine();
      
      vuelto = tienda.VenderProducto(carrito.items, int.Parse(pago));
      
      Console.WriteLine($"Muchas gracias por su compra, su vuelto es {vuelto}$");
    }
  
    //Manejar los casos para el modo cliente
    private static void MenuCliente() {
      List<string> productos = tienda.ConsultarNombres();
      int cant_opciones = productos.Count + 2;
      int seleccionado = 0;

      printMenuCliente(productos);
      cambiarSeleccion(0, cant_opciones);

      int bandera_salida = 0;
      while(bandera_salida == 0) {
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
            if (seleccionado < productos.Count){
                AgregarAlCarritoMenu(productos[seleccionado]);
                printMenuCliente(productos);
                cambiarSeleccion(0, cant_opciones);
            }
          
            if (seleccionado == cant_opciones - 2) {
                CajaMenu(carrito);
                bandera_salida += 1;
            }
            
            if(seleccionado == cant_opciones - 1) { 
                bandera_salida += 1;
            }
            break;
          
            
            
          default:
            break;
        }
      }
    }
  

    public static void Main() {
      Console.WriteLine("Bienvenido a la tienda Mauricio Shop");
      if (esVendedor()) {
        MenuVendedor();
      } else { 
        MenuCliente();
      }
    }
  }
}
