using System;

namespace Tp2AAT {
  
  class Producto {
    //Propiedades de los productos
    public string Nombre { get; private set; }

    public float Costo { get; private set; }

    public int Stock { get; set; }

    public float Precio{
      get {return Costo*130/100;}
    }

    //Metodo para determinar las propiedades del Producto
    public Producto(string nombre_n, float costo_n, int stock_n = 0){
      if(nombre_n == "") {
        throw new Exception("No se puede crear un producto sin nombre");
      }
      if(costo_n <= 0) {
        throw new Exception($"No se puede crear un producto con costo menor o igual a cero ({costo_n})");
      }
      Nombre = nombre_n;
      Costo = costo_n;
      Stock = stock_n;
    }
  }
}
