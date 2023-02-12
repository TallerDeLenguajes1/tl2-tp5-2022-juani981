namespace tp5.Models{

public abstract class Persona{
    
    public int Id { get; set; }

    public string Nombre { get; set; }   

    public string Direccion { get; set; }

    public string Telefono { get; set; }

    public Persona() { }

    public Persona (string nombre, string direccion, string telefono){
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
    }
}
}
