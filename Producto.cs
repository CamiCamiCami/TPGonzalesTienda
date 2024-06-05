using System;

namespace Tp2AAT {
  
  class Producto {
    //Propiedades de los productos
    public string nombre { get; private set; }

    public float costo { get; private set; }

    public int stock { get; set; }

    public float precio{
      get {return costo*130/100;}
    }

    //Metodo para determinar las propiedades del Producto
    public Producto(string nombre_n, float costo_n, int stock_n){
      nombre = nombre_n;
      costo = costo_n;
      stock = stock_n;
    }
    
    //Metodo para imprimir las propiedades del Producto
    public string InformacionProducto(){
      return $"{nombre}\t| Precio: {precio}\t| Stock: {stock}";
    }

  }
}
