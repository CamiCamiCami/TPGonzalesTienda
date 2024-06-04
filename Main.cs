using System.Collections.Generic;
using System;


namespace Tp2AAT {

  class Program {
    
    //Contraseña para el modo vendedor
    private const string contraseña = "TpAAT";
  
    //Creamos los objetos de tienda y carrito para el uso de programa
    private static Tienda tienda = Tienda.AgregarProductosDefault(new Tienda());
    private static Carrito carrito = new Carrito();

    //Metodo para pedir la clave al usuario
    private static bool PedirAcceso(){
      int intentos = 1;
      
      Console.Write("Ingrese la contraseña: ");
      string intento = "";
      
      while(intentos != 3){
        intento = Console.ReadLine();
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

      List<string> opciones = new List<string>(){VENDEDOR_OPC, CLIENTE_OPC};
      MenuGenerico menu = new MenuGenerico("Como quieres entrar al sistema?", opciones);
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
      List<string> opciones = new List<string>(){AGREGAR_OPC, ELIMINAR_OPC, MOSTRAR_OPC, DINERO_OPC, SALIR_OPC};
      MenuGenerico menu = new MenuGenerico("¿Que opcion quieres realizar?", opciones);

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
    

    //Metodo para agregar productos al carrito
    private static void AgregarAlCarrito(string nombre){
      Producto prod = tienda.ConsultarProducto(nombre); //Buscar el producto en la lista de productos de la tienda
      
      AgregarAlCarritoMenu menu = new AgregarAlCarritoMenu(prod);

      int cantidad_compra = menu.EsperarCantidad();

      carrito.agregarProducto(prod, cantidad_compra);
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
      

      List<string> opciones = new List<string>(productos);
      opciones.Add(SALIR_OPC);
      opciones.Add(CAJA_OPC);
      MenuGenerico menu = new MenuGenerico("Que quiere agregar al carrito", opciones);

      bool bandera_salida = false;
      while (!bandera_salida) {
        string opc = menu.EsperarEleccion();

        if(opc == SALIR_OPC) {
          bandera_salida = true;
        } else if (opc == CAJA_OPC) {
          CajaMenu(carrito);
          bandera_salida = true;
        } else {
          AgregarAlCarrito(opc);
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
