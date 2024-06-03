using System.Collections.Generic;
using System;

namespace Tp2AAT {
  class Carrito {
    //El carrito esta conformado por un diccionario de productos(key) y sus cantidades
    public Dictionary<Producto, int> items = new Dictionary<Producto, int>();

    //Metodo para agregar productos al carrito
    public void agregarProducto(Producto prod, int cantidad){
      if(items.ContainsKey(prod)) {
        items[prod] += cantidad;
      } else {
        items.Add(prod, cantidad);
      }
    }

    //Metodo para restar productos del Carrito 
    public void restarProducto(Producto prod, int cantidad){
      if(items.ContainsKey(prod)) {
        items[prod] = cantidad > items[prod] ? 0 : items[prod] - cantidad;
      }
      else{
        Console.WriteLine("No existe el producto");
      }
    }

    //Metodo para eliminar productos del Carrito
    public void eliminarProducto(Producto prod){
      if(items.ContainsKey(prod)) {
        items.Remove(prod);
      }
    }

    //Metodo para obtener el total de productos en el carrito
    public double subtotal() {
      double subtotal = 0;
      foreach(Producto prod in items.Keys) {
        subtotal += items[prod] * prod.precio;
      }

      return subtotal;
    }
    
  }
}
