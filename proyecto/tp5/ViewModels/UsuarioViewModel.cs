namespace tp5.ViewModels;

public class UsuarioViewModel
{
    [Required] 
    public int Id{get;set;}
    [Required] 
    [StringLength(20)]
    [Display(Name = "Nombre")]
    public string Nombre{get;set;}
    [Required] 
    [StringLength(20)]
    [Display(Name = "Usuario")]
    public string Usuario{get;set;}
    [Required] 
    [StringLength(20)]
    [Display(Name = "Contraseña")]
    public string Contraseña{get;set;}
    [Required] 
    [StringLength(20)]
    [Display(Name = "Rol")]
    public string Rol{get;set;}
    
    public UsuarioViewModel(){}
    public UsuarioViewModel(string usuario,string contraseña){
        Usuario=usuario;
        Contraseña=contraseña;
    }
    public UsuarioViewModel(int id,string nombre,string usuario,string contraseña,string rol){
        Id=id;
        Nombre=nombre;
        Usuario=usuario;
        Contraseña=contraseña;
        Rol=rol;
    }

}