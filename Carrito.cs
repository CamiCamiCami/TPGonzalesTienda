using System.Collections.Generic;

namespace Tp2AAT {
  class Carrito {
    private Dictionary<Producto, int> items = new Dictionary<Producto, int>();


    public void agregarProducto(Producto prod, int cantidad){
      if(items.ContainsKey(prod)) {
        items[prod] += cantidad;
      } else {
        items.Add(prod, cantidad);
      }
    }
    
    public void restarProducto(Producto prod, int cantidad){
      if(items.ContainsKey(prod)) {
        items[prod] = cantidad > items[prod] ? 0 : items[prod] - cantidad;
      }
    }

    public void eliminarProducto(string prod){
      if(items.ContainsKey(prod)) {
        items.Remove(prod);
      }
    }

    public long subtotal() {
      long subtotal = 0;
      foreach(Producto prod in items.Keys) {
        subtotal += items[prod] * prod.precio;
      }

      return subtotal;
    } 
  }
}