using System.Collections.Generic;
using System;

using System.Collections.Generic;
using System;


namespace Tp2AAT {

  class Program {
    
    //Contraseña para el modo vendedor
    private const string contraseña = "TpAAT";
  
    //Creamos los objetos de tienda y carrito para el uso de programa
    private static Tienda tienda = Tienda.AgregarProductosDefault(new Tienda());
    private static Carrito carrito = new Carrito();

    //Metodo para leer una tecla
    private static ConsoleKey esperarTecla() {
      return Console.ReadKey(true).Key;
    }

    //Metodo para pedir la clave al usuario
    private static bool PedirAcceso(){
      int intentos = 1;
      
      Console.Write("Ingrese la contraseña: ");
      string intento = "";
      
      while(intentos != 3){
        intento == Console.ReadLine();
        intentos++;
        if (intento == contraseña){
          return true;
        }
        else{
          Console.Write("Contraseña incorrecta, intente de nuevo: ");
        }
      }

      return false;
    }

    //Dentro del menu interactivo, esta funcion se ejecuta primero para preguntar al usuario por una contraseña
    private static bool esVendedor(){
      const string VENDEDOR_OPC = "Vendedor";
      const string CLIENTE_OPC = "Cliente";
      
      MenuSeleccionable menu = new MenuSeleccionable("Como quieres entrar al sistema?", new string[] {VENDEDOR_OPC, CLIENTE_OPC});

      string opc = menu.EsperarEleccion();

      if (opc == VENDEDOR_OPC) {
        if (PedirAcceso()){
          return true;
        } else {
          throw new Exception ("Se agotaron los intentos, saliendo.");
        }
      } else {
        return false;
      }
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

    //Llamar a la funcion que muestra el menu y maneja la respuesta del usuario con el teclado
    private static void MenuVendedor(){
      const string AGREGAR_OPC = "Agregar Producto"; 
      const string ELIMINAR_OPC = "Eliminar Producto"; 
      const string MOSTRAR_OPC = "Mostrar Stock"; 
      const string DINERO_OPC = "Mostrar Dinero en Caja"; 
      const string SALIR_OPC = "Salir"; 

      bool bandera_salida = false;
      MenuSeleccionable menu = new MenuSeleccionable("¿Que opcion quieres realizar?", new string[] {AGREGAR_OPC, ELIMINAR_OPC, MOSTRAR_OPC, DINERO_OPC, SALIR_OPC});

      while (!bandera_salida) {
        string opc = menu.EsperarEleccion();

        switch (opc) {
          case AGREGAR_OPC:
            AgregarProductoMenu();
            break;
          case ELIMINAR_OPC:
            EliminarProductoMenu();
            break;
          case MOSTRAR_OPC: 
            printStock();
            break;
          case DINERO_OPC:
            Console.WriteLine($"Hay {tienda.caja}$ en la caja");
            break;
          case SALIR_OPC:
            bandera_salida = true;
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

      const string SALIR_OPC = "Salir";
      const string CAJA_OPC = "Ir a la Caja";
      List<string> productos = tienda.ConsultarNombres();
      List<string> opciones = new List


      MenuSeleccionable menu = new MenuSeleccionable("Que quiere agregar al carrito", )

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
      int bandera_salida = 0;
      
      while (bandera_salida == 0){
        if (esVendedor()) {
          MenuVendedor();
        } else { 
          MenuCliente();
        }
        
        Console.WriteLine("¿Quieres seguir navegando? (s/n)");
        string salir = Console.ReadLine();
        
        if(salir.ToUpper() == "S" || salir.ToUpper() == "N"){
          if(salir.ToUpper() == "N"){
            bandera_salida += 1;
          }
        } else {
          throw new Exception("Opcion no valida");
        }    
      }   
    }
  }
}
