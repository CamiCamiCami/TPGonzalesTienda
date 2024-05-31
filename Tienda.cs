using System.Collections.Generic;
using System;

namespace Tp2AAT {
    class Tienda {
        private List<Producto> productos = new List<Producto>();
        private double dinero = 0;

        public void agregarProducto(Producto producto){
          foreach(Producto p in productos){
            if (p.nombre == producto.nombre){
              throw new Exception("El producto ya existe");
            }
          }
          productos.Add(producto);
        }

        public void venderProducto(string nombre, int cantidad){
          foreach(Producto p in productos){
            if(p.nombre == nombre) {
              if (p.stock < cantidad) {
                throw new Exception("No hay suficiente stock para relaizar la venta");
              }
              p.stock -= cantidad;
              dinero += p.precio * cantidad;
              return;
            }
          }
          throw new Exception("El producto no existe");
        }

        public void eliminarProducto(string nombre) {
          foreach(Producto p in productos) {
            if (p.nombre == nombre) {
              productos.Remove(p);
              return;
            }
          }
          throw new Exception("El producto no existe");
        }
      
      public string contenidosComoString() {
        string contenido = "";
        foreach(Producto p in productos) {
          contenido += p.ComoString() + "\n";
        }
        return contenido;
      }
    }
}