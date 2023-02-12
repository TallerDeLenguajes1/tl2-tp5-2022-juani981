namespace tp5.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //ReverseMap() va de Model a ViewModel
        CreateMap<Cadete, CadeteViewModel>().ReverseMap();
        CreateMap<Cliente, ClienteViewModel>().ReverseMap();
        CreateMap<Pedido, PedidoViewModel>().ReverseMap();
        CreateMap<Pedido, PedidoAltaViewModel>().ReverseMap();
        CreateMap<Pedido, PedidoModificadoViewModel>().ReverseMap();
        CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
    }
}