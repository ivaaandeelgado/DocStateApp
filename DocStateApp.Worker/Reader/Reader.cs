using DocStateApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStateApp.Worker.Reader
{
    public class Reader : IReadDocs
    {
        public string[] ReadDocuments(string directoryPath)
        {
            var content = new List<string>();   

            foreach (var file in Directory.EnumerateFiles(directoryPath))
            {
                if (File.Exists(file))
                {
                    if (File.ReadAllText(file).Length > 0)
                    {
                        content.Add(File.ReadAllText(file));
                    }   
                }
            

                // Aquí puedes agregar la lógica para leer el contenido del archivo
                // Por ejemplo, si es un archivo de texto, podrías usar File.ReadAllText(file)
            }   

            Debug.Write(content);
        }
    }
}
