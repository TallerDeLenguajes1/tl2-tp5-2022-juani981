using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace tp5.Controllers;
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;

        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IMapper _mapper;

        public UsuarioController(ILogger<UsuarioController> logger,  IRepositorioUsuario repositorioUsuario,  IMapper mapper)
        {
            _logger = logger;
            _repositorioUsuario = repositorioUsuario;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var usuarios = _repositorioUsuario.BuscarTodos();
            var usuariosViewModel = _mapper.Map<List<UsuarioViewModel>>(usuarios);
            return View(usuariosViewModel);
        }
    [HttpGet]
    public IActionResult AltaUsuario()
    {
        return View("AltaUsuario");
    }

    [HttpPost]
    public IActionResult AltaUsuario(UsuarioViewModel usuarioViewModel)
    {
        if (ModelState.IsValid)
        {
            var Usuario = _mapper.Map<Usuario>(usuarioViewModel);
            _repositorioUsuario.Insertar(Usuario);
        }
        else
        {
            var errores = ModelState.Values.SelectMany(x => x.Errors);
            Console.WriteLine(errores);
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarUsuario(int id)
    {
        var Usuario = _repositorioUsuario.BuscarPorId(id);
        if (Usuario is null) return RedirectToAction("Index");
        var UsuarioViewModel = _mapper.Map<UsuarioViewModel>(Usuario);
        return View(UsuarioViewModel);
    }

    [HttpPost]
    public IActionResult ModificarUsuario(UsuarioViewModel usuarioViewModel)
    {
        if (ModelState.IsValid)
        {
            Usuario Usuario = _mapper.Map<Usuario>(usuarioViewModel);
            _repositorioUsuario.Actualizar(Usuario);
        }
        else
        {
            var errores = ModelState.Values.SelectMany(x => x.Errors);
            Console.WriteLine(errores);
        }

        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult BuscarTodosPorRol(string rol){
        if (rol == "sinFiltro")
        {
            return RedirectToAction("Index");
        }
        else{
        var usuariosPorRol = _repositorioUsuario.BuscarTodosPorRol(rol);
        //Ojo al mappear objetos, y listas de objetos.
        List<UsuarioViewModel> usuariosViewModel = _mapper.Map<List<UsuarioViewModel>>(usuariosPorRol);
        return View(usuariosViewModel);
        }
    }

    [HttpGet]
    public IActionResult BajaUsuario(int id)
    {
        
        _repositorioUsuario.Eliminar(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult LoginScreen(){

        return View();
    }

    [HttpPost]
    public IActionResult Login(UsuarioViewModel usuarioViewModel){
        try{
            //var temp = _repositorioUsuario.BuscarPorUsuario(usuarioViewModel.Usuario);
            var temp = _mapper.Map<Usuario>(usuarioViewModel);
            var usuario = _repositorioUsuario.Verificar(temp);
            //var usuario = _mapper.Map<Usuario>(usuarioViewModel);
           //var usuarioOK = _repositorioUsuario.Verificar(usuario);

            if (usuario is null || usuario.rol is null )
            {
                return RedirectToAction("Index","Home");
            }
                HttpContext.Session.SetString(SessionRol,usuario.rol);
                HttpContext.Session.SetString(SessionUser,usuario.usuario);
                HttpContext.Session.SetString(SessionNombre,usuario.nombre);        
                HttpContext.Session.SetInt32(SessionID,usuario.id);

                return RedirectToAction("Index","Home");
        }
        catch(SystemException e){
            _logger.LogError("Error al Iniciar Sesión - {Error}",e.Message);
            return View("Error");
        }
    }
    public IActionResult Logout(){
        try
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginScreen");
        }
        catch (Exception e)
        {
            _logger.LogError("Error al Cerrar Sesión {Error}", e.Message);
            return View("Error");
        }
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error");
    }

}