using System;
using System.Xml;

namespace HojaDeEstilo
{
    class XMLTransformer
    {
        private static XMLTransformer instancia = null;
        XmlDocument doc;
        private static string fileXML;
        private static string filePL;
        private const string fileHTML = "informe.html";
        private static string titulo;
        XmlNodeList listaNotas;

        private XMLTransformer() { }

        public static XMLTransformer getInstance()
        {
            if (instancia == null)
            {
                return instancia = new XMLTransformer();
            }
            else
            {
                return instancia;
            }
        }

        public void transform(string[] args)
        {
            fileXML = args[0];
            filePL = args[1];

            doc = new XmlDocument();
            try
            {
                doc.Load(fileXML);
                foundTitle(fileXML);
            }
            catch (XmlException e)
            {
                Console.WriteLine("No se ha podido cargar correctamente el archivo XML!\nError: " + e.Message);
                return;
            }

            //Metodo para lectura de los demas datos que se encuentran en los nodos del archivo XML
            try
            {
                XmlNodeList asignatura = doc.GetElementsByTagName("asignatura"); //Raiz de asignatura
                XmlNodeList notas = doc.GetElementsByTagName("notas"); //Raiz de notas
                listaNotas = ((XmlElement)notas[0]).GetElementsByTagName("alumno"); //Hojas o nodos de notas

                string nombreAsignatura = ((XmlElement)asignatura[0]).GetAttribute("nombre");
                string curso = ((XmlElement)asignatura[0]).GetAttribute("curso");
                HtmlBuilder html = new HtmlBuilder(filePL, titulo, nombreAsignatura, curso, listaNotas);
                html.crearHTML();
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("No se ha podido leer correctamente la informacion del archivo XML");
                return;
            }
        }

        private void foundTitle(string fileXML)
        {
            //Metodo para leer los datos que unicamente son texto
            XmlTextReader reader = new XmlTextReader(fileXML);
            while (reader.Read()) //Leemos solo el primer texto que encontremos (porque es el titulo) y lo guardamos en la cadena
            {
                reader.MoveToContent();
                if (reader.NodeType == XmlNodeType.Text)
                {
                    titulo = reader.Value;
                    break;
                }
            }
        }

    }
}
