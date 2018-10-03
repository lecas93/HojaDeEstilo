using System;
using System.Xml;
using System.IO;

namespace HojaDeEstilo
{
    class HtmlBuilder
    {
        private const string fileHTML = "informe.html";
        private static string filePL;
        private static string titulo;
        private static string nombreAsignatura;
        private static string curso;
        private XmlNodeList listaNotas;

        public HtmlBuilder(string archivoPL, string titulo, string nombreAsignatura, string curso, XmlNodeList listaNotas)
        {
            filePL = archivoPL;
            HtmlBuilder.titulo = titulo;
            HtmlBuilder.nombreAsignatura = nombreAsignatura;
            HtmlBuilder.curso = curso;
            this.listaNotas = listaNotas;
        }

        public void crearHTML()
        {
            StreamWriter writer = new StreamWriter(fileHTML); //Se crea un nuevo .html
            writer.Close();
            File.AppendAllText(fileHTML, "<!-- Fichero presentacion_listado.html -->" + "\r\n\r"); //Agregamos el encabezado al archivo html recien creado
            FileStream streamPL = new FileStream(filePL, FileMode.Open, FileAccess.Read);
            StreamReader readerPL = new StreamReader(streamPL);
            while (!readerPL.EndOfStream) //Ciclo donde se realizara la lectura del archivo .pl y escritura del .html
            {
                string lineaActual = readerPL.ReadLine();
                if (!(lineaActual.Contains("LISTADO") || lineaActual.Contains("ASIGNATURA") || lineaActual.Contains("NOTAS") || lineaActual.Contains("ALUMNO") || lineaActual.Contains("hojaestilo.pl")))
                {
                    //Las siguientes sentencias cambian las etiquetas encontradas en el .pl por la informacion correspondiente extraida del XML
                    if (lineaActual.Contains("@NOMBRE@")) lineaActual = lineaActual.Replace("@NOMBRE@", nombreAsignatura);
                    if (lineaActual.Contains("@CURSO@")) lineaActual = lineaActual.Replace("@CURSO@", curso);
                    if (lineaActual.Contains("@@")) lineaActual = lineaActual.Replace("@@", titulo);
                    File.AppendAllText(fileHTML, lineaActual + "\r\n\r");
                }
                else
                {
                    //Cuando se llega al nodo de ALUMNO...
                    if (lineaActual.Contains("ALUMNO"))
                    {
                        string nodoAlumno = "";
                        while (true) //... se lee y almacena temporalmente la estructura de dicho nodo...
                        {
                            string linea = readerPL.ReadLine();
                            if (linea.Contains("ALUMNO")) break;
                            nodoAlumno += linea + "\r\n\r";
                        }
                        foreach (XmlElement nodo in listaNotas) //... y se implanta dicho nodo en el archivo .html por cada alumno encontrado en el archivo XML
                        {
                            string dni = nodo.GetAttribute("dni");
                            string nota = nodo.GetAttribute("nota");
                            string nombre = nodo.GetAttribute("nombre");
                            string temp = nodoAlumno;
                            if (temp.Contains("@NOMBRE@")) temp = temp.Replace("@NOMBRE@", nombre);
                            if (temp.Contains("@CURSO@")) temp = temp.Replace("@CURSO@", curso);
                            temp = temp.Replace("@DNI@", dni);
                            temp = temp.Replace("@NOTA@", nota);
                            File.AppendAllText(fileHTML, temp);
                        }
                    }
                }
            }
            Console.WriteLine("El archivo HTML ha sido creado con exito!");
        }
    }
}
