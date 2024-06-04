using System.Collections.Generic;
using System;


namespace Tp2AAT {

  class Program {
    private const string contraseña = "TpAAT";  //Contraseña para el modo vendedor
    private static Tienda tienda = Tienda.AgregarProductosDefault(new Tienda());  //Creamos los objetos de tienda y carrito para el uso de programa
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
      MenuSeleccionable menu = new MenuSeleccionable("Como quieres entrar al sistema?", opciones);
      
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
      List<string> opciones = new List<string>(){AGREGAR_OPC, ELIMINAR_OPC, MOSTRAR_OPC, DINERO_OPC, SALIR_OPC};
      MenuSeleccionable menu = new MenuSeleccionable("¿Que opcion quieres realizar?", opciones);

      bool bandera_salida = false;
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
    

    //Metodo para agregar productos al carrito
    private static void AgregarAlCarrito(string nombre){
      Producto prod = tienda.ConsultarProducto(nombre); //Buscar el producto en la lista de productos de la tienda
      AgregarAlCarritoMenu menu;
      if (carrito.Items.ContainsKey(prod)) {
        menu = new AgregarAlCarritoMenu(prod, carrito.Items[prod]);
      } else {
        menu = new AgregarAlCarritoMenu(prod);
      }

      int cantidad_compra = menu.EsperarCantidad();

      carrito.agregarProducto(prod, cantidad_compra);
    }

    private static void CajaMenu(){
      const string PROCEDER_OPC = "Proceder con el Pago";
      const string CANCELAR_OPC = "Cancelar la Compra";
      List<Producto> contenidos_carrito = carrito.ProductosEnCarrito();
      List<string> opciones = new List<string>();
      foreach (Producto prod in contenidos_carrito) {
        opciones.Add("X " + prod.nombre + "\t\t" + carrito.CantidadEnCarrito(prod)); // Da formato a las opciones
      }
      opciones.Add(PROCEDER_OPC);
      opciones.Add(CANCELAR_OPC);
      
      bool bandera_salida = false;

      while(!bandera_salida) {
        MenuSeleccionable menu = new MenuSeleccionable("Va a Comprar\t\tSubtotal: " + carrito.subtotal(), opciones);
        string opc = menu.EsperarEleccion();

        if(opc == PROCEDER_OPC) {
          float vuelto = 0;
          bool bandera_salida_pago = false;
          Console.WriteLine("Con cuanto va a pagar?");
          while(!bandera_salida_pago){
            Console.Write("-> ");
            int pago = int.Parse(Console.ReadLine());
            if (pago < carrito.subtotal()) {
              Console.WriteLine($"Ingreso {carrito.subtotal()-pago} menos del monto a pagar ({carrito.subtotal()})");
            } else {
              vuelto = tienda.VenderProducto(carrito.Items, pago);
              carrito = new Carrito();
              Console.WriteLine($"Muchas gracias por su compra, su vuelto es {vuelto}$");
              bandera_salida_pago = true;
            }
          }
          bandera_salida = true;
        } else if(opc == CANCELAR_OPC) {
          MenuSeleccionable menu_cancelar = new MenuSeleccionable("Está seguro de que quiere cancelar la operacion?", new List<string>(){"SI", "NO"});

          string opc_cancelar = menu_cancelar.EsperarEleccion();

          if (opc_cancelar == "SI") {
            carrito = new Carrito();
            bandera_salida = true;
          }
        } else {
          // Eliminar un Producto del carrito
          int index = opciones.IndexOf(opc);
          Producto prod = contenidos_carrito[index];
          MenuSeleccionable menu_eliminar = new MenuSeleccionable($"Seguro que quiere eliminar {prod.nombre} del carrito?", new List<string>(){"SI", "NO"});
          string opc_eliminar = menu_eliminar.EsperarEleccion();
          if (opc_eliminar == "SI") {
            contenidos_carrito.Remove(prod);
            opciones.Remove(opc);
            carrito.EliminarProducto(prod);
          }
        }
      }
    }
  
    //Manejar los casos para el modo cliente
    private static void MenuCliente() {

      const string SALIR_OPC = "Salir";
      const string CAJA_OPC = "Ir a la Caja";
      List<string> productos = tienda.ConsultarNombres();
      

      List<string> opciones = new List<string>(productos);
      opciones.Add(CAJA_OPC);
      opciones.Add(SALIR_OPC);
      MenuSeleccionable menu = new MenuSeleccionable("Que quiere agregar al carrito", opciones);

      bool bandera_salida = false;
      while (!bandera_salida) {
        string opc = menu.EsperarEleccion();

        if(opc == SALIR_OPC) {
          bandera_salida = true;
        } else if (opc == CAJA_OPC) {
          CajaMenu();
          bandera_salida = true;
        } else {
          AgregarAlCarrito(opc);
        }
      }
    }
  
    public static void Main() {
      Console.WriteLine("Bienvenido a la tienda Mauricio Shop");
      MenuSeleccionable menu_salida = new MenuSeleccionable("¿Quieres seguir navegando?", new List<string> {"Si", "No"});
      bool bandera_salida = false;
      
      while (!bandera_salida){
        if (esVendedor()) {
          MenuVendedor();
        } else { 
          MenuCliente();
        }
        
        string opc = menu_salida.EsperarEleccion();
        if(opc == "No"){
          bandera_salida = true;
        }
      }   
    }
  }
}
