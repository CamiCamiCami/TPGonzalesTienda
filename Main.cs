using System.Collections.Generic;
using System;


namespace Tp2AAT {

  class Program {
    
    private const string Contraseña = "TpAAT"; //Contraseña para el modo vendedor
    private static Tienda MiTienda = Tienda.AgregarProductosDefault(new Tienda()); //Creamos los objetos de tienda y carrito para el uso de programa
    private static Carrito MiCarrito = new Carrito();

    //Metodo para pedir la clave al usuario
    private static bool PedirAcceso(){
      Console.CursorVisible = true;
      const int max_intentos = 3;

      Console.Write("Ingrese la contraseña: ");
      string intento = "";

      for (int intentos = 0; intentos < max_intentos; intentos++){ 
        intento = Console.ReadLine();
        if (intento == Contraseña){
          Console.CursorVisible = false;
          return true;
        }
        else{
          Console.Write("Contraseña incorrecta, intente de nuevo: ");
        }
      }
      Console.CursorVisible = false;
      return false;
    }

    //Dentro del menu interactivo, esta funcion se ejecuta primero para preguntar al usuario por una contraseña
    private static bool EsVendedor(){
      
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
          Console.WriteLine("Se agotaron los intentos, saliendo."); //Si se acaban los intentos, el programa sale
          Environment.Exit(1);
          Console.CursorVisible = true;
          return false;
        }
      } else {
        return false; //Si el usuario no pidio el modo vendedor, entra en modo cliente
      }
    }

    //Agregar productos al sistema
    private static void AgregarProductoMenu(){
      //Se piden los datos del producto
      Console.CursorVisible = true;
      // chequeos para el nombre
      string nombre = "";
      bool bandera_nombre = false;
      while (!bandera_nombre) {
        Console.Write("(Obligatorio)\tNombre: ");
        nombre = Console.ReadLine();
        // Chequea que el nombre no esté vacio
        if (nombre == "") {
          Console.WriteLine("El campo de nombre no puede estar vacio. Por Favor ingreselo de nuevo");
          continue;
        }
        // Chequea que el nombre no esté repetido
        try {
          MiTienda.ConsultarProducto(nombre);
        } catch (Exception) {
          // Fallo la consulta, por lo tanto no está repetido
          bandera_nombre = true;
        } finally {
          if (!bandera_nombre) { // El mensaje solo aparece si no paso por el catch
            Console.WriteLine($"El nombre del producto debe ser unico pera ya hay en la tienda un producto llamado {nombre}. Por Favor ingreselo de nuevo");
          }
        }
      }
      
      // chequeos para el costo
      float costo = 0.0f;
      bool bandera_costo = false;
      while (!bandera_costo) {
        Console.Write("(Obligatorio)\tCosto: ");
        string costo_str = Console.ReadLine();
        // chequea que el campo no esté vacio
        if (costo_str == "") {
          Console.WriteLine($"El campo de costo no puede estar vacio. Por Favor ingreselo de nuevo");
          continue;
        }
        // chequea que sea parseable
        try {
          costo = float.Parse(costo_str);
        } catch (FormatException) {
          Console.WriteLine($"El costo debe ser ingresado como un numero de punto flotante pero recibio {costo_str}. Por Favor ingreselo de nuevo");
          continue;
        }
        // chequea que no sea cero
        if (costo == 0.0f) {
          Console.WriteLine($"El campo de costo no puede ser cero. Por Favor ingreselo de nuevo");
          continue;
        }

        bandera_costo = true;
      }

      // chequeos para el stock
      bool bandera_stock = false;
      int stock = 0;
      while (!bandera_stock) {
        Console.Write("\t\tStock: ");
        string stock_str = Console.ReadLine();
        // Si el stock es salteado, lo pone como 0
        if (stock_str == "") {
          stock = 0;
          bandera_stock = true;
          continue;
        }
        // chequea que el stock sea parseable
        try {
          stock = int.Parse(stock_str);
        } catch (FormatException) {
          Console.WriteLine($"El stock debe ser ingresado como un entero pero recibio {stock_str}. Por Favor ingreselo de nuevo");
          continue;
        }
        // chequea que el stock sea mayor o igual a cero
        if (stock < 0) {
          Console.WriteLine($"El campo de stock no puede ser menor a cero. Por Favor ingreselo de nuevo");
          continue;
        }
        
        bandera_stock = true;
      }

      Producto prod = new Producto(nombre, costo, stock);
      MiTienda.AgregarProducto(prod);
      Console.CursorVisible = false;
    }

    //Eliminar productos del sistema
    private static void EliminarProductoMenu(){
      
      Console.Write("Nombre del producto a eliminar:"); //Se pide el nombre de producto a eliminar
      string nombre = Console.ReadLine();

      //Manejo de error
      try {
        MiTienda.EliminarProducto(nombre);
        Console.WriteLine($"El producto {nombre} ha sido eliminado del sistema");
      } catch (Exception) {
        Console.WriteLine("El producto que quiere eliminar no existe en el sistema");
      }
    }

    //Esta funcion muestra el stock de productos del sistema
    private static void PrintStock(){
      string stock = MiTienda.ObtenerStock();
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
            PrintStock();
            break;
          case DINERO_OPC:
            Console.WriteLine($"Hay {MiTienda.Caja}$ en la caja");
            break;
          case SALIR_OPC:
            bandera_salida = true;
            break;
        }
      }
    }

    //Metodo para agregar productos al carrito
    private static void AgregarAlCarrito(string nombre){
      
      Producto prod = MiTienda.ConsultarProducto(nombre); //Buscar el producto en la lista de productos de la tienda

      int cant_en_carrito = MiCarrito.CantidadEnCarrito(prod);
      AgregarAlCarritoMenu menu = new AgregarAlCarritoMenu(prod, cant_en_carrito);

      int cantidad_compra = menu.EsperarCantidad(); //Nos devuelve la cantidad de productos a comprar
      while((prod.Stock < cantidad_compra)){
        Console.WriteLine($"No hay suficiente stock para este producto (Actualmente tenemos {prod.Stock}), vuelva a ingresar");
        cantidad_compra = menu.EsperarCantidad();
      }
      MiCarrito.AgregarProducto(prod, cantidad_compra); //Se agrega al carrito el producto y la cantidad
    }

    // Convierte una lista de Productos a una lista de strings a usarse como opciones en el menu de la caja
    private static List<string> FormatearProductosParaCajaMenu(List<Producto> productos) {
      int mayor_largo_nombre = 0, mayor_largo_precio = 0, mayor_largo_cant = 0;
      // Encuentra el largo maximo (como string) de los valores a monoespaciar
        foreach(Producto p in productos) {
          string cant = MiCarrito.CantidadEnCarrito(p).ToString(); // consigue la cantidad en carrito como string
          mayor_largo_nombre = mayor_largo_nombre < p.Nombre.Length ? p.Nombre.Length : mayor_largo_nombre;
          mayor_largo_precio = mayor_largo_precio < p.Precio.ToString().Length ? p.Precio.ToString().Length : mayor_largo_precio;
          mayor_largo_cant = mayor_largo_cant < cant.Length ? cant.Length : mayor_largo_cant;
        }

        List<string> como_strings = new List<string>();
        // genera los strings monoespaciados
        foreach(Producto p in productos) {
          string cant = MiCarrito.CantidadEnCarrito(p).ToString(); // consigue la cantidad en carrito como string
          string str = "X ";
          str += p.Nombre;
          str += new string(' ', mayor_largo_nombre - p.Nombre.Length);
          str += "\t";
          str += new string(' ', mayor_largo_cant - (cant.Length));
          str += cant;
          str += " x ";
          str += new string(' ', mayor_largo_precio - (p.Precio.ToString().Length));
          str += p.Precio.ToString();
          str += "$";
          como_strings.Add(str);
        }

        return como_strings;
    }

    //Metodo para el menu de la caja del cliente
    private static void CajaMenu(){
      
      const string PROCEDER_OPC = "Proceder con el Pago"; //Opcion del menu 
      const string CANCELAR_OPC = "Cancelar la Compra"; //Opcion del menu
      
      List<Producto> contenidos_carrito = MiCarrito.ProductosEnCarrito(); //Se obtienen los productos del carrito
      
      List<string> opciones = FormatearProductosParaCajaMenu(contenidos_carrito);
      //Guarda las opciones restantes
      opciones.Add(PROCEDER_OPC); 
      opciones.Add(CANCELAR_OPC);

      bool bandera_salida = false;

      while(!bandera_salida) { //Mientras el usuario no elija la opcion de salir

        //Se crea el objeto Menu 
        MenuSeleccionable menu = new MenuSeleccionable("Va a Comprar\t\tSubtotal: " + MiCarrito.Subtotal(), opciones); //Titulo
        string opc = menu.EsperarEleccion(); //Nos devuelve la opcion que eligio el usuario

        if(opc == PROCEDER_OPC) { //Opcion de pagar directamente
          float vuelto = 0;
          bool bandera_salida_pago = false;
          
          Console.WriteLine("Con cuanto va a pagar?");
          
          while(!bandera_salida_pago){
            Console.Write("-> "); 
            Console.CursorVisible = true;
            string pago_str = Console.ReadLine();
            Console.CursorVisible = false;
            float pago;
            try {
              pago = float.Parse(pago_str); //Se le pide al usuario que ingrese con cuanto va a pagar
            } catch (FormatException) {
              Console.WriteLine($"El monto de pago debe ser ingresado como un numero de punto flotante pero recibio {pago_str}. Por Favor ingreselo de nuevo");
              continue;
            }
            
            if (pago < MiCarrito.Subtotal()) { //Caso en que el pago del cliente sea menor al monto a abonar
              Console.WriteLine($"No ingreso el monto suficiente, le falta {MiCarrito.Subtotal() - pago}. Por Favor ingreselo de nuevo");
            } 
            else {
              vuelto = MiTienda.VenderProducto(MiCarrito.Items, pago); //Nos devuelve el vuelto
              MiCarrito = new Carrito(); //Crea un nuevo carrito por si el cliente quiere seguir navegando
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
            MiCarrito = new Carrito(); //Se vacia el Carrito 
            bandera_salida = true; 
          }
        } 
        else { // Eliminar un Producto del carrito
          int index = opciones.IndexOf(opc); //Se obtiene el indice del producto a eliminar
          Producto prod = contenidos_carrito[index]; 
          
          MenuSeleccionable menu_eliminar = new MenuSeleccionable($"Seguro que quiere eliminar {prod.Nombre} del carrito?", new List<string>(){"SI", "NO"}); //Se crea un nuevo menu para confirmar la accion
          
          string opc_eliminar = menu_eliminar.EsperarEleccion();
          
          if (opc_eliminar == "SI") { //Elimina el producto del carrito
            contenidos_carrito.Remove(prod);
            opciones.Remove(opc);
            MiCarrito.EliminarProducto(prod);
          }
        }
      }
    }

    //Metodo para mostrar el menu del cliente
    private static void MenuCliente() {

      List<string> productos = MiTienda.ConsultarNombres(); //Se obtienen los nombres de los productos de la tienda
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
          if (!MiCarrito.EstaVacio()){
            CajaMenu();
            bandera_salida = true;
          } else {
            Console.WriteLine("Todavia no agrego ningun producto al carrito");
          }
        } 
        else { //Opcion para agregar al carrito
          AgregarAlCarrito(opc);
        }
      }
    }

    public static void Main() {
      bool visivilidad_cursor = Console.CursorVisible;
      Console.CursorVisible = false;
      Console.WriteLine("Bienvenido a Mauri Shop");
      MenuSeleccionable menu_salida = new MenuSeleccionable("¿Quieres seguir navegando?", new List<string> {"Si", "No"});
      bool bandera_salida = false;

      while (!bandera_salida){
        if (EsVendedor()) {
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
      Console.CursorVisible = visivilidad_cursor; 
    }
  }
}
