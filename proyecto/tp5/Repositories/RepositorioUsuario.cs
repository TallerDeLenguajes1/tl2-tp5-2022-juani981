namespace tp5.Repositories;
    public class RepositorioUsuario :Repositorio<Usuario>, IRepositorioUsuario
    {   
        
        public RepositorioUsuario(IConfiguration configuration): base(configuration){
        }

        public override Usuario? BuscarPorId(int id){
            const string consulta = "select * from Usuario where id_usuario = @id";
            try{
                using var conexion = new SqliteConnection(CadenaConexion);
                var peticion = new SqliteCommand(consulta, conexion);
                peticion.Parameters.AddWithValue("@id", id);
                conexion.Open();

                var salida = new Usuario();
                using var reader = peticion.ExecuteReader();
                while (reader.Read()){
                    salida = new Usuario(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),reader.GetString(4));
                }
                conexion.Close();
                return salida;
            }
            catch (Exception e)
        {
            Logger.Debug("Error al buscar el Usuario {Id} - {Error}", id, e.Message);
        }

        return null;
        }
        public  Usuario? BuscarPorUsuario(string Nomusuario){
            const string consulta = "select * from Usuario where usuario = @user";
            try{
                using var conexion = new SqliteConnection(CadenaConexion);
                var peticion = new SqliteCommand(consulta, conexion);
                peticion.Parameters.AddWithValue("@user", Nomusuario);
                conexion.Open();

                var salida = new Usuario();
                using var reader = peticion.ExecuteReader();
                while (reader.Read())
                    salida = new Usuario(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),reader.GetString(4));
                
                conexion.Close();
                return salida;
            }
            catch (Exception e)
        {
            Logger.Debug("Error al buscar el Usuario {user} - {Error}", Nomusuario, e.Message);
        }

        return null;
        }

        public override IEnumerable<Usuario> BuscarTodos(){
            const string consulta = "select * from Usuario";
            try{
                using var conexion = new SqliteConnection(CadenaConexion);
                var peticion = new SqliteCommand(consulta, conexion);
                conexion.Open();

                var salida = new List<Usuario>();
                using var reader = peticion.ExecuteReader();
                while (reader.Read()){
                    var usuario = new Usuario(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),reader.GetString(4));
                    salida.Add(usuario);
                }

                conexion.Close();
                return salida;
            }
            catch (Exception e)
        {
            Logger.Debug("Error al buscar lista de Usuarios - {Error}", e.Message);
        }

        return null;
        }

        public override void Insertar(Usuario entidad){
            const string consulta =
            "insert into Usuario (nombre, usuario, contraseña, rol) values (@nombre,@usuario,@contraseña, @rol)";
        try
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            conexion.Open();
            
            peticion.Parameters.AddWithValue("@nombre", entidad.nombre);
            peticion.Parameters.AddWithValue("@usuario", entidad.usuario);
            peticion.Parameters.AddWithValue("@contraseña", entidad.contraseña);
            peticion.Parameters.AddWithValue("@rol", entidad.rol);

            peticion.ExecuteNonQuery();
            conexion.Close();
        }
        catch (Exception e)
        {
            Logger.Debug("Error al insertar el cliente {Id} - {Error}", entidad.id, e.Message);
        }
        }

        public override void Actualizar(Usuario entidad){
            const string consulta =
            "update Usuario set nombre=@nombre, usuario=@usuario, contraseña=@contraseña, rol=@rol where id_usuario=@id";
            try
            {
            using var conexion = new SqliteConnection(CadenaConexion);
            var peticion = new SqliteCommand(consulta, conexion);
            conexion.Open();
            
            peticion.Parameters.AddWithValue("@id", entidad.id);
            peticion.Parameters.AddWithValue("@nombre", entidad.nombre);
            peticion.Parameters.AddWithValue("@usuario", entidad.usuario);
            peticion.Parameters.AddWithValue("@contraseña", entidad.contraseña);
            peticion.Parameters.AddWithValue("@rol", entidad.rol);
            
            peticion.ExecuteReader();
            conexion.Close();
            }
            catch (Exception e)
            {
                Logger.Debug("Error al insertar el Uduario {Id} - {Error}", entidad.id, e.Message);
            }
        }

        public override void Eliminar(int id){
            const string consulta = "delete * from Usuario where id_usuario = {id}";
            try{
                using var conexion = new SqliteConnection(CadenaConexion);
                var peticion = new SqliteCommand(consulta, conexion);
                peticion.Parameters.AddWithValue("@id", id.ToString());
                conexion.Open();
                peticion.ExecuteReader();
                conexion.Close();
            }
            catch (Exception e)
        {
            Logger.Debug("Error al eliminar el Usuario {Id} - {Error}", id, e.Message);
        }


        }
        public Usuario? Verificar(Usuario usuario){
            /*var consulta =
            //"select usuario,contraseña from Usuario";
            //where usuario = @user and contraseña = @password";
            "select usuario,contraseña from Usuario where usuario = 'admin' and contraseña = 'admin'";


                using var conexion = new SqliteConnection(CadenaConexion);
                var peticion = new SqliteCommand(consulta, conexion);
                //peticion.Parameters.AddWithValue("@user", usuario.usuario);
                //peticion.Parameters.AddWithValue("@password", usuario.contraseña);
                conexion.Open();

                //var salida = new Usuario();
                using var reader = peticion.ExecuteReader();*/
                var usuarioDB = BuscarPorId(usuario.id);
                if (usuario.usuario==usuarioDB.usuario && usuario.contraseña==usuarioDB.contraseña)
                {
                    //conexion.Close();
                    return usuarioDB;
                }
                else{
                    //conexion.Close();
                    
                }

                return null;
            
        }
        public IEnumerable<Usuario> BuscarTodosPorRol(string rol){
            const string consulta = "select * from Usuario where rol=@rol";
            try{
                using var conexion = new SqliteConnection(CadenaConexion);
                var peticion = new SqliteCommand(consulta, conexion);
                peticion.Parameters.AddWithValue("@rol", rol);
                conexion.Open();

                var salida = new List<Usuario>();
                using var reader = peticion.ExecuteReader();
                while (reader.Read()){
                    var usuario = new Usuario(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),reader.GetString(4));
                    salida.Add(usuario);
                }

                conexion.Close();
                return salida;
            }
            catch (Exception e)
        {
            Logger.Debug("Error al buscar lista de Usuarios - {Error}", e.Message);
        }
        return null;
        }
    }
