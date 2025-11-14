using DocStateApp.Core.Interfaces;
using DocStateApp.Core.Modelos;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig;

namespace DocStateApp.Worker.Reader
{
    public class Reader : IReadDocs
    {


        public List<Item> ReadDocuments(string directoryPath)
        {
            var listaItems = new List<Item>();


            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                throw new ArgumentException("El path del directorio está vacío.", nameof(directoryPath));
            }

            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException($"El directorio no existe: {directoryPath}");
            }

            foreach (var file in Directory.EnumerateFiles(directoryPath))
            {

                try
                {
                    string contenido = string.Empty;
                    var ext = Path.GetExtension(file).ToLowerInvariant();

                    if (ext == ".txt" || ext == ".csv")
                    {
                        contenido = File.ReadAllText(file);
                    }
                    else if (ext == ".pdf")
                    {
                        var sb = new StringBuilder();
                        using (var document = PdfDocument.Open(file))
                        {
                            foreach (var page in document.GetPages())
                                sb.AppendLine(page.Text);
                        }

                        contenido = sb.ToString();
                    }
                    else
                    {
                        continue;
                    }

                    var item = sacarItem(contenido);

                    item.NombreArchivo = Path.GetFileName(file);

                    if (!string.IsNullOrWhiteSpace(item.Id))
                        listaItems.Add(item);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error leyendo '{file}': {ex.Message}");

                }



            }

            return listaItems;
        }

  


        public Item sacarItem(string contenido)
        {
            Item item = new Item();

            var lines = contenido.Split(
            new[] { "\r\n", "\n" },
            StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var partes = line.Split(new[] { ':' }, 2);
                if (partes.Length < 2)
                    continue;

                var key = partes[0].Trim();
                var value = partes[1].Trim();

                switch (key)
                {
                    case "Ticket":
                        item.Id = value;
                        break;

                    case "Usuario":
                        item.EmailUsuario = value;
                        break;

                    case "Estado":
                        item.estado = compararEstado(value);
                        break;

                    case "Descripción":
                        item.Descripcion = value;
                        break;
                }
            }

            return item;
        }

         
        public Estado compararEstado(string estadoStr)
        {
            return estadoStr.ToLower() switch
            {
                "todo" => Estado.ToDo,
                "doing" => Estado.Doing,
                "done" => Estado.Done,
                "historial" => Estado.Historial,
                _ => throw new ArgumentException($"Estado desconocido: {estadoStr}"),
            };
        }
    }
}
