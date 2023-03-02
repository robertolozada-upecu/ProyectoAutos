using ProyectoAutos.Modelos;
using SQLite;

namespace ProyectoAutos.Servicios
{
    public class AutoService
    {
        private SQLiteConnection conexion;
        private readonly string _pathDB;
        public string MensajeEstado { get; private set; }

        public int accion = 0;

        public AutoService(string pathDB)
        {
            _pathDB = pathDB;
        }
        
        private void InicializarDB()
        {
            if (conexion != null) return;

            conexion = new SQLiteConnection(_pathDB);
            conexion.CreateTable<Auto>();
        }

        public List<Auto> ObtenerAutos()
        {
            try
            {
                InicializarDB();
                return conexion.Table<Auto>().ToList();
            }
            catch (Exception)
            {

                MensajeEstado = "No se ha podido recuperar la información";
            }

            return null;
        }

        public void AgregarAuto(Auto auto)
        {
            try
            {
                InicializarDB();

                if (auto == null)
                    throw new Exception("Auto no válido");

                var resultado = conexion.Insert(auto);
                MensajeEstado = resultado == 0 ? "La operación de inserción ha fallado" : "Ingreso exitoso";
            }
            catch (Exception)
            {
                MensajeEstado = "No se ha podido insertar el item";
            }
        }

        public void EliminarAuto(int id)
        {
            try
            {
                InicializarDB();

                var resultado = conexion.Table<Auto>().Delete(auto => auto.Id == id);
                MensajeEstado = resultado == 0 ? "La operación de borrado ha fallado" : "Eliminación exitosa";
            }
            catch (Exception)
            {
                MensajeEstado = "No se ha podido eliminar el item";
            }
        }

        public Auto ObtenerAuto(int id)
        {
            try
            {
                InicializarDB();
                return conexion.Table<Auto>().FirstOrDefault(auto => auto.Id == id);
            }
            catch (Exception)
            {

                MensajeEstado = "No se ha podido recuperar la información";
            }

            return null;
        }

        public void ActualizarAuto(Auto auto)
        {
            try
            {
                InicializarDB();

                if (auto == null)
                    throw new Exception("Auto no válido");
                var resultado = conexion.Update(auto);
                MensajeEstado = resultado == 0 ? "La operación de actualización ha fallado" : "Actualización exitosa";
            }
            catch (Exception)
            {
                MensajeEstado = "No se ha podido actualizar el item";
            }
        }
    }
}
