using System.Collections.Generic;
using System;


namespace Tp2AAT {

  class Program {
    
    private const string contraseña = "TpAAT"; //Contraseña para el modo vendedor
    private static Tienda tienda = Tienda.AgregarProductosDefault(new Tienda()); //Creamos los objetos de tienda y carrito para el uso de programa
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

      string opc = menu.EsperarEleccion(); //Nos devuelve la opcion que eligio el usuario

      if (opc == VENDEDOR_OPC) {
        if (PedirAcceso()){
          return true; //Si la clave es ingresada con exito, el usuario entra en modo vendedor
        } 
        else {
          throw new Exception ("Se agotaron los intentos, saliendo."); //Si se acaban los intentos, el programa se rompe
        }
      } 
      else {
        return false; //Si el usuario no pidio el modo vendedor, entra en modo cliente
      }
    }

    //Agregar productos al sistema
    private static void AgregarProductoMenu(){
      //Se piden los datos del producto
      string nombre ="";
      string costo = "";
      string stock = "";
      int fallo = 0;

      while(nombre == "" || costo == "" || stock =="" || costo == "0" || stock =="0"){ //Revisar errores
        if(fallo == 1){
        Console.WriteLine("Algunos de los campos es vacio o igual a 0. Ingrese los datos nuevamente");
        }
        else {
          Console.WriteLine("Ingrese los campos del producto");
        }
        Console.Write("(Obligatorio)\tNombre: ");
        nombre = Console.ReadLine();
        Console.Write("(Obligatorio)\tCosto: ");
        costo = Console.ReadLine();
        Console.Write("\t\tStock: ");
        stock = Console.ReadLine();
        fallo=1;
      }

      if(tienda.ConsultarProducto(nombre) != null){
        Console.WriteLine("El producto ya existe dentro del sistema");
      }
      else {
        Producto prod = new Producto(nombre, float.Parse(costo), int.Parse(stock));
        tienda.AgregarProducto(prod);
      }
    }

    //Eliminar productos del sistema
    private static void EliminarProductoMenu(){
      
      Console.Write("Nombre del producto a eliminar:"); //Se pide el nombre de producto a eliminar
      string nombre = Console.ReadLine();

      //Manejo de error
      if(tienda.ConsultarProducto(nombre)==null){
        Console.WriteLine("El producto que quiere eliminar no existe en el sistema");
      }
      else{
      tienda.EliminarProducto(nombre);
      Console.WriteLine($"El producto {nombre} ha sido eliminado del sistema");
      }
    }

    //Esta funcion muestra el stock de productos del sistema
    private static void printStock(){
      string stock = tienda.ObtenerStock();
      Console.Write(stock);
    }

    //Metodo para mostrar el menu de Vendedor 
    private static void MenuVendedor(){
      //Campos para el objeto Menu
      const string AGREGAR_OPC = "Agregar Producto"; 
      const string ELIMINAR_OPC = "Eliminar Producto"; 
      const string MOSTRAR_OPC = "Mostrar Stock"; 
      const string DINERO_OPC = "Mostrar Dinero en Caja"; 
      const string SALIR_OPC = "Salir"; 
      
      List<string> opciones = new List<string>(){AGREGAR_OPC, ELIMINAR_OPC, MOSTRAR_OPC, DINERO_OPC, SALIR_OPC};
      MenuSeleccionable menu = new MenuSeleccionable("¿Que opcion quieres realizar?", opciones); //Se crea el objeto Menu

      bool bandera_salida = false;
      
      while (!bandera_salida) { //El Menu se ejecuta hasta que el usuario elija la opcion de salir
        string opc = menu.EsperarEleccion(); //Nos devuelve la opcion que eligio el usuario

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

      int cantidad_compra = menu.EsperarCantidad(); //Nos devuelve la cantidad de productos a comprar
      while((prod.stock < cantidad_compra)){
        Console.WriteLine("No hay suficiente stock para este producto, vuelva a ingresar");
        cantidad_compra = menu.EsperarCantidad();
    }
      carrito.agregarProducto(prod, cantidad_compra); //Se agrega al carrito el producto y la cantidad
    }

    //Metodo para el menu de la caja del cliente
    private static void CajaMenu(){
      
      const string PROCEDER_OPC = "Proceder con el Pago"; //Opcion del menu 
      const string CANCELAR_OPC = "Cancelar la Compra"; //Opcion del menu
      List<string> opciones = new List<string>();
      
      List<Producto> contenidos_carrito = carrito.ProductosEnCarrito(); //Se obtienen los productos del carrito
      
      foreach (Producto prod in contenidos_carrito) {
        opciones.Add("X " + prod.nombre + "\t\t" + carrito.CantidadEnCarrito(prod)); //Guarda los productos del carrito y la cantidad
      }
      //Guarda las opciones restantes
      opciones.Add(PROCEDER_OPC); 
      opciones.Add(CANCELAR_OPC);

      bool bandera_salida = false;

      while(!bandera_salida) { //Mientras el usuario no elija la opcion de salir

        //Se crea el objeto Menu 
        MenuSeleccionable menu = new MenuSeleccionable("Va a Comprar\t\tSubtotal: " + carrito.subtotal(), opciones); //Titulo
        string opc = menu.EsperarEleccion(); //Nos devuelve la opcion que eligio el usuario

        if(opc == PROCEDER_OPC) { //Opcion de pagar directamente
          float vuelto = 0;
          bool bandera_salida_pago = false;
          
          Console.WriteLine("Con cuanto va a pagar?");
          
          while(!bandera_salida_pago){
            Console.Write("-> "); 
            int pago = int.Parse(Console.ReadLine()); //Se le pide al usuario que ingrese con cuanto va a pagar
            
            if (pago < carrito.subtotal()) { //Caso en que el pago del cliente sea menor al monto a abonar
              Console.WriteLine($"No ingreso el monto suficiente, le falta {carrito.subtotal() - pago}");
            } 
            else {
              vuelto = tienda.VenderProducto(carrito.Items, pago); //Nos devuelve el vuelto
              carrito = new Carrito(); //Crea un nuevo carrito por si el cliente quiere seguir navegando
              Console.WriteLine($"Muchas gracias por su compra, su vuelto es {vuelto}$");
              bandera_salida_pago = true;
            }
          }
          bandera_salida = true; 
        }
        
        else if(opc == CANCELAR_OPC) { //Caso de cancelar la compra 
          MenuSeleccionable menu_cancelar = new MenuSeleccionable("Está seguro de que quiere cancelar la operacion?", new List<string>(){"SI", "NO"}); //Se crea otro menu para confirmar la accion

          string opc_cancelar = menu_cancelar.EsperarEleccion();

          if (opc_cancelar == "SI") { 
            carrito = new Carrito(); //Se vacia el Carrito 
            bandera_salida = true; 
          }
        } 
        else { // Eliminar un Producto del carrito
          int index = opciones.IndexOf(opc); //Se obtiene el indice del producto a eliminar
          Producto prod = contenidos_carrito[index]; 
          
          MenuSeleccionable menu_eliminar = new MenuSeleccionable($"Seguro que quiere eliminar {prod.nombre} del carrito?", new List<string>(){"SI", "NO"}); //Se crea un nuevo menu para confirmar la accion
          
          string opc_eliminar = menu_eliminar.EsperarEleccion();
          
          if (opc_eliminar == "SI") { //Elimina el producto del carrito
            contenidos_carrito.Remove(prod);
            opciones.Remove(opc);
            carrito.EliminarProducto(prod);
          }
        }
      }
    }

    //Metodo para mostrar el menu del cliente
    private static void MenuCliente() {

      List<string> productos = tienda.ConsultarNombres(); //Se obtienen los nombres de los productos de la tienda
      const string SALIR_OPC = "Salir";
      const string CAJA_OPC = "Ir a la Caja";

      //La primeras opciones seran los productos de la tienda y luego ira la opcion de salir y de ir a la caja
      List<string> opciones = new List<string>(productos);
      opciones.Add(CAJA_OPC);
      opciones.Add(SALIR_OPC);
      MenuSeleccionable menu = new MenuSeleccionable("Que quiere agregar al carrito", opciones); //Se crea el menu para el cliente

      bool bandera_salida = false;
      
      while (!bandera_salida) {
        string opc = menu.EsperarEleccion();

        if(opc == SALIR_OPC) { //Opcion para salir del sistema
          bandera_salida = true;
        } 
        else if (opc == CAJA_OPC) { //Opcion para ir a la caja
          CajaMenu();
          bandera_salida = true;
        } 
        else { //Opcion para agregar al carrito
          AgregarAlCarrito(opc);
        }
      }
    }

    public static void Main() {
      
      Console.WriteLine("Bienvenido a Mauri Shop");
      MenuSeleccionable menu_salida = new MenuSeleccionable("¿Quieres seguir navegando?", new List<string> {"Si", "No"});
      bool bandera_salida = false;

      while (!bandera_salida){
        if (esVendedor()) {
          MenuVendedor();
        } 
        else { 
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
