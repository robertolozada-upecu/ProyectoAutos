using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoAutos.Modelos;
using ProyectoAutos.Servicios;
using ProyectoAutos.Vistas;
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


        public ObservableCollection<Auto> Autos { get; private set; } = new();

        public ListadoAutosViewModel()
        {
            Titulo = "Listado de autos";
            ObtenerListaAutos().Wait();
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

                var autos= App.AutoService.ObtenerAutos();
                foreach(var auto in autos)
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
                App.AutoService.AgregarAuto(auto);
            }
            else
            {
                App.AutoService.ActualizarAuto(Identificador, auto);
            }
            
            await Shell.Current.DisplayAlert("Info", App.AutoService.MensajeEstado, "Ok");
            await LimpiarFormulario();
            //await ObtenerListaAutos();
        }

        [RelayCommand]
        async Task EditarAuto(int id)
        {
            if (id == 0)
            {
                await Shell.Current.DisplayAlert("Error", "Por favor, intente otra vez", "Ok");
                return;
            }
            var auto = App.AutoService.ObtenerAuto(id);
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
            App.AutoService.EliminarAuto(id);
            await Shell.Current.DisplayAlert("Info", App.AutoService.MensajeEstado, "Ok");
            await ObtenerListaAutos();
        }

        [RelayCommand]
        async Task IrADetalleAuto(int id)
        {
            if (id == 0) return;
            
            await Shell.Current.GoToAsync($"DetallesAutoPage?id={id}", true);
        }
    }
}
