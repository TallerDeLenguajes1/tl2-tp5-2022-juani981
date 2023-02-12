namespace tp5.ViewModels.Cadete;

public class CadeteViewModel
{
    [Required]
    public int Id { get; set; }

    [Required][StringLength(100)]
    public string Nombre { get; set; }   

    [Required][StringLength(300)]
    public string Direccion { get; set; }

    [Required][Phone]
    public string Telefono { get; set; }

    public CadeteViewModel (string nombre, string direccion, string telefono){
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
    }
}