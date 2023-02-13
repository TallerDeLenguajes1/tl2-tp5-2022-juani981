namespace tp5.Controllers;

public class PedidoController : Controller
{
    private readonly ILogger<PedidoController> _logger;
    private readonly IRepositorioPedido _repositorioPedido;
    private readonly IRepositorio<Cadete> _repositorioCadete;
    private readonly IRepositorio<Cliente> _repositorioCliente;
    private readonly IMapper _mapper;

    public PedidoController(ILogger<PedidoController> logger, IRepositorioPedido repositorioPedido,
        IRepositorio<Cadete> repositorioCadete, IRepositorio<Cliente> repositorioCliente, IMapper mapper)
    {
        _logger = logger;
        _repositorioPedido = repositorioPedido;
        _repositorioCadete = repositorioCadete;
        _repositorioCliente = repositorioCliente;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Index(){
    var idCadete = HttpContext.Session.GetInt32(SessionID);
    
        if (HttpContext.Session.GetString(SessionRol)=="admin")
        {
            var pedidos = _repositorioPedido.BuscarTodos();
            var pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidos);
            return View(pedidosViewModel);
        }
        if(HttpContext.Session.GetString(SessionRol)=="cadete")
        {
            var pedidos = _repositorioPedido.BuscarTodosPorCadete((int)idCadete);
            var pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidos);
            return View("Index",pedidosViewModel);
        }
        
        return View();
    }

    [HttpGet]
    public IActionResult BuscarTodosPorCliente(int cliente)
    {
        var pedidos = _repositorioPedido.BuscarTodosPorCliente(cliente);
        var pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidos);
        return View(pedidosViewModel);
    }

    [HttpGet]
    public IActionResult BuscarTodosPorCadete(int cadete)
    {
        var pedidos = _repositorioPedido.BuscarTodosPorCadete(cadete);
        var pedidosViewModel = _mapper.Map<List<PedidoViewModel>>(pedidos);
        return View(pedidosViewModel);
    }

    [HttpGet]
    public IActionResult AltaPedido()
    {
        var cadetes = _repositorioCadete.BuscarTodos();
        var clientes = _repositorioCliente.BuscarTodos();
        var cadetesViewModel = _mapper.Map<List<CadeteViewModel>>(cadetes);
        var clientesViewModel = _mapper.Map<List<ClienteViewModel>>(clientes);
        var pedidoAltaViewModel = new PedidoAltaViewModel(cadetesViewModel, clientesViewModel);
        return View(pedidoAltaViewModel);
    }

    [HttpPost]
    public IActionResult AltaPedido(PedidoViewModel pedidoViewModel)
    {
        if (ModelState.IsValid)
        {
            var pedido = _mapper.Map<Pedido>(pedidoViewModel);
            _repositorioPedido.Insertar(pedido);
        }
        else
        {
            var errores = ModelState.Values.SelectMany(x => x.Errors);
            Console.WriteLine(errores);
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarPedido(int id)
    {
        var pedido = _repositorioPedido.BuscarPorId(id);
        
        var cadetes = _repositorioCadete.BuscarTodos();
        var cadetesViewModel = _mapper.Map<List<CadeteViewModel>>(cadetes);

        var clientes = _repositorioCliente.BuscarTodos();
        var clientesViewModel = _mapper.Map<List<ClienteViewModel>>(clientes);
        
        var pedidoModificadoViewModel = _mapper.Map<PedidoModificadoViewModel>(pedido);
        pedidoModificadoViewModel.Cadetes = cadetesViewModel;
        pedidoModificadoViewModel.Clientes = clientesViewModel;
        
        return View(pedidoModificadoViewModel);
    }

    [HttpPost]
    public IActionResult ModificarPedido(PedidoViewModel pedidoViewModel)
    {
        Console.WriteLine(pedidoViewModel.Cadete);
        if (ModelState.IsValid)
        {
            var pedido = _mapper.Map<Pedido>(pedidoViewModel);
            _repositorioPedido.Actualizar(pedido);
        }
        else
        {
            var errores = ModelState.Values.SelectMany(x => x.Errors);
            Console.WriteLine(errores);
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult BajaPedido(int id)
    {
        _repositorioPedido.Eliminar(id);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}