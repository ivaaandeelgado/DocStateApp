using DocStateApp.Core.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStateApp.Core.Interfaces
{
    public interface IReadDocs
    {
        List<Item> ReadDocuments(string directoryPath);
    }
}
