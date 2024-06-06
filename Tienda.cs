using System.Collections.Generic;
using System;

namespace Tp2AAT {
    class Tienda {

      private List<Producto> Productos = new List<Producto>(); //Inicializamos la lista de productos que estaran disponibles en la tienda
      public double Caja {get; private set; } = 0; //Propiedad para obtener y definir un dinero en la caja

      //Metodo para agregar productos a la lista de productos de la tienda
      public void AgregarProducto(Producto producto){
        Productos.Add(producto); 
      }

      //Metodo para vender un producto
      public float VenderProducto(Dictionary<Producto, int> diccionario, float pago){
        float total = 0;
        foreach(var producto in diccionario){
          for(int i = 0; i < Productos.Count; i++){
            if(Productos[i].Nombre == producto.Key.Nombre){ //Si el producto existe en la lista de productos
              if(Productos[i].Stock >= producto.Value){ //Si hay stock suficiente
                Productos[i].Stock -= producto.Value;
                total += Productos[i].Precio * (float) producto.Value;
              } else {
                Console.WriteLine($"No se pueden comprar {producto.Value} {producto.Key.Nombre}/s. No hay suficiente stock.");
              }
            }
          }
        }

        if (pago < total) {
          throw new Exception($"Se quiso pagar con {pago}$ una compra de {total}$. Faltan {total-pago}$");
        }

        Caja += total;
        return (pago - total);
      }

      //Metodo para eliminar productos de la lista
      public void EliminarProducto(string nombre) {
        foreach(Producto p in Productos) {
          if (p.Nombre == nombre) {
            Productos.Remove(p);
            return;
          }
        }

        throw new Exception($"No existe un producto en la tienda llamado {nombre}");
      }

      //Metodo para buscar por un producto en la lista de productos
      public Producto ConsultarProducto(string nombre) {
        foreach(Producto p in Productos) {
          if (p.Nombre.ToUpper() == nombre.ToUpper()) {
            return p;
          }
        }
        
        throw new Exception($"No existe un producto en la tienda llamado {nombre}");
      }

      //Metodo para obtener unicamente los nombres de los productos
      public List<string> ConsultarNombres() {
        List<string> nombres = new List<string>();
        foreach(Producto p in Productos) {
          nombres.Add(p.Nombre);
        }

        return nombres;
      }

      //Metodo para obtener toda la informacion de los productos 
      public string ObtenerStock() {
        int mayor_largo_nombre = 0, mayor_largo_precio = 0, mayor_largo_stock = 0;
        foreach(Producto p in Productos) {
          mayor_largo_nombre = mayor_largo_nombre < p.Nombre.Length ? p.Nombre.Length : mayor_largo_nombre;
          mayor_largo_precio = mayor_largo_precio < p.Precio.ToString().Length ? p.Precio.ToString().Length : mayor_largo_precio;
          mayor_largo_stock = mayor_largo_stock < p.Stock.ToString().Length ? p.Stock.ToString().Length : mayor_largo_stock;
        }

        string acumulador = "";
        foreach(Producto p in Productos) {
          acumulador += p.Nombre;
          acumulador += new string(' ', mayor_largo_nombre - p.Nombre.Length);
          acumulador += "\t";
          acumulador += new string(' ', mayor_largo_precio - (p.Precio.ToString().Length));
          acumulador += p.Precio.ToString();
          acumulador += "$";
          acumulador += "\t";
          acumulador += p.Stock.ToString();
          acumulador += new string(' ', mayor_largo_stock - (p.Stock.ToString().Length));
          acumulador += " unidades\n";
        }

        return acumulador;
      }

      //Metodo que agrega algunos productos basicos a la tienda
      public static Tienda AgregarProductosDefault(Tienda _tienda){
        _tienda.AgregarProducto(new Producto("Desodorante", 5000, 50));
        _tienda.AgregarProducto(new Producto("Vaso", 1000, 13));
        _tienda.AgregarProducto(new Producto("Lampara", 30000, 55));
        _tienda.AgregarProducto(new Producto("Vino", 10000, 38));
        _tienda.AgregarProducto(new Producto("Celular", 1000000, 74));
        _tienda.AgregarProducto(new Producto("Monitor", 500000, 3));
        _tienda.AgregarProducto(new Producto("Panel de Vidro", 60000, 94));
        _tienda.AgregarProducto(new Producto("Cinturon", 8000, 27));
        _tienda.AgregarProducto(new Producto("Block de Notas", 3000, 43));
        _tienda.AgregarProducto(new Producto("Tablon", 10000, 85));
        _tienda.AgregarProducto(new Producto("Pajita", 700, 34));
        _tienda.AgregarProducto(new Producto("Coca-Cola", 1500));
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
    }
} 
