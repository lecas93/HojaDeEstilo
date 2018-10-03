using System;
using System.IO;

namespace HojaDeEstilo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Generador de informe";
            Console.WriteLine("Presione ENTER para comenzar..."); Console.ReadLine();
            try
            {
                if (File.Exists(args[0]) && File.Exists(args[1]))
                {
                    XMLTransformer transformer = XMLTransformer.getInstance();
                    transformer.transform(args);
                }
                else
                {
                    Console.Write("No se han encontrado todos los archivos necesarios para generar el informe."
                        + "\nRevise que los archivos <.xml> y <.pl> se encuentren en las misma"
                        + " carpeta que la aplicacion e intente de nuevo.\n");
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("No se han encontrado los argumentos necesarios.");
            }
            Console.WriteLine("\nPresione ENTER para finalizar...");
            Console.ReadLine();
        }
    }
}
