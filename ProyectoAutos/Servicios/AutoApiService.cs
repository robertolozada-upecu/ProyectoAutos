using Newtonsoft.Json;
using ProyectoAutos.Modelos;
using System.Net.Http.Json;

namespace ProyectoAutos.Servicios
{
    public class AutoApiService
    {
        HttpClient _httpClient;
        public static string DireccionBase = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:6060" : "http://localhost:6060";
        public string MensajeEstado { get; private set; }

        public AutoApiService()
        {
            _httpClient = new() { BaseAddress = new Uri(DireccionBase) };
        }

        public async Task<List<Auto>> ObtenerAutos()
        {
            try
            {
                var respuesta = await _httpClient.GetStringAsync("/autos");
                return JsonConvert.DeserializeObject<List<Auto>>(respuesta);
            }
            catch (Exception)
            {

                MensajeEstado = "No se ha podido recuperar la información";
            }

            return null;
        }

        public async Task AgregarAuto(Auto auto)
        {
            try
            {
                var respuesta = await _httpClient.PostAsJsonAsync("/autos", auto);
                respuesta.EnsureSuccessStatusCode();
                MensajeEstado = "Ingreso exitoso";
            }
            catch (Exception)
            {
                MensajeEstado = "No se ha podido insertar el item";
            }
        }

        public async Task EliminarAuto(int id)
        {
            try
            {
                var respuesta = await _httpClient.DeleteAsync($"/autos/{id}");
                respuesta.EnsureSuccessStatusCode();
                MensajeEstado = "Eliminación exitosa";
            }
            catch (Exception)
            {
                MensajeEstado = "No se ha podido eliminar el item";
            }
        }

        public async Task<Auto> ObtenerAuto(int id)
        {
            try
            {
                var respuesta = await _httpClient.GetStringAsync($"/autos/{id}");
                return JsonConvert.DeserializeObject<Auto>(respuesta);
            }
            catch (Exception)
            {

                MensajeEstado = "No se ha podido recuperar la información";
            }

            return null;
        }

        public async Task ActualizarAuto(int id, Auto automodificado)
        {
            try
            {
                if (automodificado == null)
                    throw new Exception("Auto no válido");
                var resultado = await _httpClient.PutAsJsonAsync($"/autos/{id}", automodificado);
                resultado.EnsureSuccessStatusCode();
                MensajeEstado = "Actualización exitosa";
            }
            catch (Exception)
            {
                MensajeEstado = "No se ha podido actualizar el item";
            }
        }
    }
}
