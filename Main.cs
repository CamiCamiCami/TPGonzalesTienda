using System;
using System.Console;
using System.Console.ConsoleKey;

namespace Tp2AAT
{

    class Program
    {

        private const string SI_STR = "SI";
        private const string NO_STR = "NO";
        Tienda tienda = new Tienda();
        Carrito carrito = new Carrito();

        private static bool esVendedor(){
          while (true){
            Console.WriteLine("Quiere entrar al sistema como vendedor?");
            string respuesta = Console.ReadLine();

            respuesta = respuesta.ToUpper();  
            if (respuesta == SI_STR || respuesta == NO_STR){
              return respuesta == SI_STR;
            }

            Console.WriteLine("Respuesta no valida, intente de nuevo");
          }
          
          

          return respuesta.ToUpper() == "COMPRADOR";
        }

        private static void esperarTecla() {
          return Console.ReadKey(false).Key;
        }

        private static void menuVendedor(){
          while(true){
            ConsoleKey tecla = esperarTecla();
            switch(tecla){
              case ConsoleKey.UpArrow:
                
            }
          }
        }
        public static void Main()
        {
          Console.WriteLine("Bienvenido a la tienda Mauricio Shop");
          string respuesta = "";
          if (esVendedor()) {
            while(respuesta.ToUpper() != "SALIR"){
              Console.WriteLine("Que opcion quieres realizar?");
              Console.WriteLine("1: Agregar producto");
              Console.WriteLine("2: Eliminar producto");
              Console.WriteLine("3: Ver listado de productos y stock");
              Console.WriteLine("4: Ver dinero en caja");
              Console.WriteLine("Escriba SALIR si quiere salir");
              respuesta = Console.ReadLine();
            
              if(respuesta == "1"){
                Console.WriteLine("Ingrese el nombre del producto");
                string nombreProd = Console.ReadLine();
              }
              if(respuesta == "2");
              if(respuesta == "3");
              if(respuesta == "4");
            }       
          } else { 
            
          }
        }
      }
    }