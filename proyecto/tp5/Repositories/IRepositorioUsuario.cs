namespace tp5.Repositories;

public interface IRepositorioUsuario : IRepositorio<Usuario>
{
    Usuario? BuscarPorUsuario(string usuario);
    Usuario? Verificar(Usuario usuario);

    IEnumerable<Usuario> BuscarTodosPorRol(string rol);
}
    
