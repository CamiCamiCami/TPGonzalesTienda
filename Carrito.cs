using System.Collections.Generic;
using System;

namespace Tp2AAT {
  class Carrito {
    //El carrito esta conformado por un diccionario de productos(key) y sus cantidades
    public Dictionary<Producto, int> Items {get; private set; } = new Dictionary<Producto, int>();

    //Metodo para agregar productos al carrito, si ya esta presente setea la cantidad
    public void agregarProducto(Producto prod, int cantidad){
      
      if (cantidad == 0) {
        if (Items.ContainsKey(prod)) {
          Items.Remove(prod);
        }
      }
      if(Items.ContainsKey(prod)) {
        Items[prod] = cantidad;
      } else {
        Items.Add(prod, cantidad);
      }
    }

    //Metodo para eliminar productos del Carrito
    public void EliminarProducto(Producto prod){
      if(Items.ContainsKey(prod)) {
        Items.Remove(prod);
      }
    }

    //Metodo para obtener el total de productos en el carrito
    public double subtotal() {
      double subtotal = 0;
      foreach(Producto prod in Items.Keys) {
        subtotal += Items[prod] * prod.precio;
      }

      return subtotal;
    }

    public List<Producto> ProductosEnCarrito() {
      return new List<Producto>(this.Items.Keys);
}

    public int CantidadEnCarrito(Producto prod) {
      if(this.Items.ContainsKey(prod)) {
        return this.Items[prod];
      } 
      else {
        return 0;
      }
    }

  }
}
