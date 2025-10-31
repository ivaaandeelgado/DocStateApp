using System;



namespace DocStateApp.Core.Modelos
{
    public class Item
    {

        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public Estado estado { get; set; }
        public string Descripcion { get; set; } = string.Empty;


    }

}
