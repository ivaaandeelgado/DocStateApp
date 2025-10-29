using System;

public class Item
{


    public int Id { get; set; } 
    public string Nombre { get; set; } = string.Empty;
    public Estado estado { get; set; } = "Desconocido";
    public string Descripcion { get; set; } = string.Empty;


}
