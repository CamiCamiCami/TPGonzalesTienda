using System.Collections.Generic;
using System;

namespace Tp2AAT {
    class Tienda {
      
      //Inicializamos la lista de productos que estaran disponibles en la tienda
      private List<Producto> productos = new List<Producto>();
      //Propiedad para obtener y definir un dinero en la caja
      public double caja { get; private set; } = 0;

      //Metodo para agregar productos a la lista de productos de la tienda
      public void AgregarProducto(Producto producto){
        foreach(Producto p in productos){
          if (p.nombre == producto.nombre){ //Si el producto ya existe en la lista, no se agrega
            throw new Exception("El producto ya existe");
          }
        }
        productos.Add(producto); 
      }
      
      //Metodo para vender un producto
      public float VenderProducto(Dictionary<Producto, int> diccionario, int pago){
        float total = 0;
        foreach(var producto in diccionario){
          for(int i = 0; i < productos.Count; i++){
            if(productos[i].nombre == producto.Key.nombre){
              if(productos[i].stock >= producto.Value){
                productos[i].stock -= producto.Value;
                caja += productos[i].precio * producto.Value;
                total += productos[i].precio * producto.Value;
              }
              else{
                throw new Exception("No hay suficiente stock, la compra ha fallado");
                
              }
            }
          }
        }
        return (pago-total);
      }

      //Metodo para eliminar productos de la lista
      public void EliminarProducto(string nombre) {
        foreach(Producto p in productos) {
          if (p.nombre == nombre) {
            productos.Remove(p);
            return;
          }
        }
        throw new Exception("El producto no existe");
      }

      //Metodo para buscar por un producto en la lista de productos
      public Producto ConsultarProducto(string nombre) {
        foreach(Producto p in productos) {
          if (p.nombre == nombre) {
            return p;
          }
        }
        throw new Exception("El producto no existe");
      }

      //Metodo para obtener unicamente los nombres de los productos
      public List<string> ConsultarNombres() {
        List<string> nombres = new List<string>();
        foreach(Producto p in productos) {
          nombres.Add(p.nombre);
        }

        return nombres;
      }

      //Metodo para obtener toda la informacion de los productos 
      public string ObtenerStock() {
        string contenido = "";
        foreach(Producto p in productos) {
          contenido += p.InformacionProducto() + "\n";
        }
        return contenido;
      }
      
      //Metodo que agrega algunos productos basicos a la tienda
      public static Tienda AgregarProductosDefault(Tienda _tienda){
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
      
    }
}
