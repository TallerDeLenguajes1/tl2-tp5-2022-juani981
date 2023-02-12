namespace tp5.Models;

public class Usuario : Persona {

    public int id{get;set;}
    public string nombre{get;set;}
    public string usuario {get;set;}
    public string contraseña{get;set;}
    public string rol {get;set;}

public Usuario(){

}
public Usuario(string Usuario, string Contraseña){
    usuario=Usuario;
    contraseña=Contraseña;
}
public Usuario(int Id, string Nombre,string Usuario, string Contraseña, string Rol){
    id=Id;
    nombre=Nombre;
    usuario=Usuario;
    contraseña=Contraseña;
    rol=Rol;

}

}