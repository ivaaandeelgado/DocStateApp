using DocStateApp.Core.Interfaces;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig;

namespace DocStateApp.Worker.Reader
{
    public class Reader : IReadDocs
    {


        public string[] ReadDocuments(string directoryPath)
        {
            var content = new List<string>();   


            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                throw new ArgumentException("El path del directorio no puede estar vacío.", nameof(directoryPath));
            }

            if (!Directory.Exists(directoryPath))
            {
                return content.ToArray();
                throw new DirectoryNotFoundException($"El directorio especificado no existe: {directoryPath}");
            }   

            foreach (var file in Directory.EnumerateFiles(directoryPath))
            {

                try
                {
                    var ext = Path.GetExtension(file).ToLowerInvariant();

                    if (ext == ".txt" || ext == ".docx" || ext == ".csv")
                    {
                        content.Add(File.ReadAllText(file));
                    }
                    else if(ext == ".pdf")
                    {
                        var sb = new StringBuilder();

                        using (var document = PdfDocument.Open(file))
                        {
                            foreach (var page in document.GetPages())
                            {
                                sb.AppendLine(page.Text);
                            }
                        }

                        content.Add(sb.ToString());
                    }
                    else
                    {
                        content.Add($"Formato de archivo no soportado: {ext}");
                    }   
                }
                catch(Exception ex)
                {
                    Debug.WriteLine($"Error leyendo '{file}': {ex.Message}");
                    
                }


                // Aquí puedes agregar la lógica para leer el contenido del archivo
                // Por ejemplo, si es un archivo de texto, podrías usar File.ReadAllText(file)
            }   

            return content.ToArray();
        }
    }
}
