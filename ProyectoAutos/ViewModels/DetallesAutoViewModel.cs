using CommunityToolkit.Mvvm.ComponentModel;
using ProyectoAutos.Modelos;
using System.Web;

namespace ProyectoAutos.ViewModels
{
    [QueryProperty("identificador", "Id")]
    public partial class DetallesAutoViewModel : BaseViewModels, IQueryAttributable
    {
        [ObservableProperty]
        int identificador;
        [ObservableProperty]
        Auto carro;
        public DetallesAutoViewModel()
        {

        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Identificador = Convert.ToInt32(HttpUtility.UrlDecode(query["Id"].ToString()));
            Carro = App.AutoService.ObtenerAuto(Identificador);
        }
    }
}
