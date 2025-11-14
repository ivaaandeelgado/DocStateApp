using System;



namespace DocStateApp.Core.Modelos
{
    public class Item
    {

        public string Id { get; set; }
        public string EmailUsuario{ get; set; }
        public Prioridad prioridad { get; set; }
        public string NombreArchivo { get; set; } = string.Empty;
        public Estado estado { get; set; }
        public string Descripcion { get; set; } = string.Empty;


    }

}
