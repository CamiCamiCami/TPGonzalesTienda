using System;

namespace Tp2AAT {
  class Producto {
    public string nombre {
      get {return nombre;}
      private set {nombre = value;}
    }
  
    public double costo {
      get {return costo;}
      private set {costo = value;}
    }

    public long stock {
      get {return stock;}
      set {stock = value;}
    }

    public double precio{
      get {return costo*130/100;}
    }

    public Producto(string nombre, double costo, long stock = (long)0){
      if (nombre == ""){
        throw new Exception("El nombre del producto no puede ser vacío");
      }
      if (costo <= 0){
        throw new Exception("El precio del producto no puede ser menor o igual a cero");
      }
      this.nombre = nombre;
      this.costo = costo;
      this.stock = stock;
    }

    public string ComoString(){
      return $"{this.nombre}\t\tPrecio: {this.costo}\tStock: {this.stock}";
    }
  
  }
}