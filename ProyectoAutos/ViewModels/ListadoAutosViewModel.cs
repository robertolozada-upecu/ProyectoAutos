using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoAutos.Modelos;
using ProyectoAutos.Servicios;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ProyectoAutos.ViewModels
{
    public partial class ListadoAutosViewModel : BaseViewModels
    {
        [ObservableProperty]
        bool estaRefrescando;
        [ObservableProperty]
        int identificador;
        [ObservableProperty]
        string marca;
        [ObservableProperty]
        string modelo;
        [ObservableProperty]
        string placa;

        public enum Conexion {Local, API};
        public Conexion tipoConexion = Conexion.Local;

        private readonly AutoApiService _autoApiService;

        public ObservableCollection<Auto> Autos { get; private set; } = new();

        public ListadoAutosViewModel(AutoApiService autoApiService)
        {
            Titulo = "Listado de autos";
            _autoApiService = autoApiService;
            if (tipoConexion == Conexion.Local)
            {
                ObtenerListaAutos().Wait();
            }
            else
            {
                _ = ObtenerListaAutos();
            }
        }

        [RelayCommand]
        async Task ObtenerListaAutos()
        {
            if (EstaCargando) return;
            try
            {
                EstaCargando= true;
                if(Autos.Any())
                    Autos.Clear();

                var autos = new List<Auto>();
                if (tipoConexion == Conexion.Local)
                    autos = App.AutoService.ObtenerAutos();
                else
                    autos = await _autoApiService.ObtenerAutos();
                //var autos = App.AutoService.ObtenerAutos();
                //var autos = await _autoApiService.ObtenerAutos();
                foreach (var auto in autos)
                {
                    Autos.Add(auto);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"No se pueden obtener los autos: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "No se pudo obtener la lista de autos", "Ok");
            }
            finally
            {
                EstaRefrescando = false;
                EstaCargando= false;
            }
        }

        [RelayCommand]
        async Task AgregarAuto()
        {
            if(string.IsNullOrEmpty(Marca) || string.IsNullOrEmpty(Modelo) || string.IsNullOrEmpty(Placa))
            {
                await Shell.Current.DisplayAlert("Error", "Por favor ingrese valores válidos", "Ok");
                return;
            }

            var auto = new Auto
            {
                Marca = Marca,
                Modelo = Modelo,
                Placa = Placa
            };

            if (Identificador==0)
            {
                if (tipoConexion == Conexion.Local)
                {
                    App.AutoService.AgregarAuto(auto);
                    await Shell.Current.DisplayAlert("Info", App.AutoService.MensajeEstado, "Ok");
                }
                else
                {
                    await _autoApiService.AgregarAuto(auto);
                    await Shell.Current.DisplayAlert("Info", _autoApiService.MensajeEstado, "Ok");
                }
            }
            else
            {
                if (tipoConexion == Conexion.Local)
                {
                    auto.Id = Identificador;
                    Identificador = 0;
                    App.AutoService.ActualizarAuto(auto);
                    await Shell.Current.DisplayAlert("Info", App.AutoService.MensajeEstado, "Ok");
                }
                else
                {
                    await _autoApiService.ActualizarAuto(Identificador, auto);
                    Identificador = 0;
                    await Shell.Current.DisplayAlert("Info", _autoApiService.MensajeEstado, "Ok");
                } 
            }
            await LimpiarFormulario();
        }

        [RelayCommand]
        async Task EditarAuto(int id)
        {
            if (id == 0)
            {
                await Shell.Current.DisplayAlert("Error", "Por favor, intente otra vez", "Ok");
                return;
            }

            Auto auto;
            if (tipoConexion == Conexion.Local)
            {
                auto = App.AutoService.ObtenerAuto(id);
            }
            else
            {
                auto = await _autoApiService.ObtenerAuto(id);
            }
            Identificador = id;
            Marca = auto.Marca;
            Modelo = auto.Modelo;
            Placa = auto.Placa;
        }

        [RelayCommand]
        async Task LimpiarFormulario()
        {
            Marca = string.Empty;
            Modelo = string.Empty;
            Placa = string.Empty;
            Identificador = 0;
            await ObtenerListaAutos();
        }

        [RelayCommand]
        async Task EliminarAuto(int id)
        {
            if(id == 0)
            {
                await Shell.Current.DisplayAlert("Error", "Por favor, intente otra vez", "Ok");
                return;
            }
            if (tipoConexion == Conexion.Local)
            {
                App.AutoService.EliminarAuto(id);
                await Shell.Current.DisplayAlert("Info", App.AutoService.MensajeEstado, "Ok");
            }
            else
            {
                await _autoApiService.EliminarAuto(id);
                await Shell.Current.DisplayAlert("Info", _autoApiService.MensajeEstado, "Ok");
            }
            await ObtenerListaAutos();
        }

        [RelayCommand]
        async Task IrADetalleAuto(int id)
        {
            if (id == 0) return;

            if (tipoConexion == Conexion.Local)
            {
                await Shell.Current.GoToAsync($"DetallesAutoPage?id={id}", true);
            }
            else
            {
                await Shell.Current.GoToAsync($"DetallesAutoPage?id={id}", true);
            }
        }
    }
}
