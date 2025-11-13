using System;



namespace DocStateApp.Core.Modelos
{
    public class Item
    {

        public int Id { get; set; }
        private string Usuario { get; set; } = string.Empty;
        private string Pioridad { get; set; } = string.Empty;

        //public string NombreArchivo { get; set; } = string.Empty;
        public Estado estado { get; set; }
        public string Descripcion { get; set; } = string.Empty;


    }

}
