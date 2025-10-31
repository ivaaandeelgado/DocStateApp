using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocStateApp.Core.Interfaces;
using System.IO;



namespace DocStateApp.Worker.Escaner
{
    public class ScanDocuments : IScanDocs
    {
        public string[] ScanDocumentsInDirectory(string directoryPath)
        {

            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException($"El directorio '{directoryPath}' no existe.");
            }
            
            string[] supportedExtensions = new[] { ".pdf", ".docx", ".doc", ".txt", ".xlsx", ".pptx" };
         
            var documents = Directory.GetFiles(directoryPath)
                                     .Where(file => supportedExtensions.Contains(Path.GetExtension(file).ToLower()))
                                     .ToArray();
            return documents;

        }
    }


}
