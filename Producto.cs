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
        while(nombre_n == ""){
          Console.WriteLine("El nombre no puede ser vacio. Ingrese un nombre valido");
          nombre_n = Console.ReadLine(); 
        }
        while(costo_n <= 0){
          Console.WriteLine("El costo no puede ser 0. Ingrese un costo valido");
          string costo_n1 = Console.ReadLine(); 
          costo_n = float.Parse(costo_n1);
        }
      nombre = nombre_n;
      costo = costo_n;
      stock = stock_n;
    }

    public string InformacionProducto(){
      return $"{nombre}\t| Precio: {precio}\t| Stock: {stock}";
    }
  
  }
}
