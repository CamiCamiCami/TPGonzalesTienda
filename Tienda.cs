using System.Collections.Generic;
using System;

namespace Tp2AAT {
    class Tienda {
      private List<Producto> productos = new List<Producto>();
      public double caja { get; private set; } = 0;

      public void AgregarProducto(Producto producto){
        foreach(Producto p in productos){
          if (p.nombre == producto.nombre){
            throw new Exception("El producto ya existe");
          }
        }
        productos.Add(producto);
      }

      public void VenderProducto(string nombre, int cantidad){
        foreach(Producto p in productos){
          if(p.nombre == nombre) {
            if (p.stock < cantidad) {
              throw new Exception("No hay suficiente stock para relaizar la venta");
            }
            p.stock -= cantidad;
            caja += p.precio * cantidad;
            if (p.stock == 0) {
              productos.Remove(p);
            }
            return;
          }
        }
        throw new Exception("El producto no existe");
      }

      public void EliminarProducto(string nombre) {
        foreach(Producto p in productos) {
          if (p.nombre == nombre) {
            productos.Remove(p);
            return;
          }
        }
        throw new Exception("El producto no existe");
      }

      public Producto ConsultarProducto(string nombre) {
        foreach(Producto p in productos) {
          if (p.nombre == nombre) {
            return p;
          }
        }
        throw new Exception("El producto no existe");
      }

      public List<string> ConsultarNombres() {
        List<string> nombres = new List<string>();
        foreach(Producto p in productos) {
          nombres.Add(p.nombre);
        }

        return nombres;
      }
      
      public string ContenidosComoString() {
        string contenido = "";
        foreach(Producto p in productos) {
          contenido += p.ComoString() + "\n";
        }
        return contenido;
      }
    }
}