using System;

namespace Tp2AAT {
  class Producto {
    public string nombre { get; private set; }
  
    public double costo { get; private set; }

    public long stock { get; set; }

    public double precio{
      get {return costo*130/100;}
    }

    public Producto(string nombre_n, double costo, long stock = (long)0){
      if (nombre_n == ""){
        throw new Exception("El nombre del producto no puede ser vacío");
      }
      if (costo <= 0){
        throw new Exception("El precio del producto no puede ser menor o igual a cero");
      }
      this.nombre = nombre_n;
      this.costo = costo;
      this.stock = stock;
    }

    public string ComoString(){
      return $"Precio: {this.precio}$\t| Stock: {this.stock}\t| {this.nombre}";
    }
  
  }
}