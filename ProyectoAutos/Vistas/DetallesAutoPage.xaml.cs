using ProyectoAutos.ViewModels;

namespace ProyectoAutos.Vistas;

public partial class DetallesAutoPage : ContentPage
{
	public DetallesAutoPage(DetallesAutoViewModel detallesAutoViewModel)
	{
		InitializeComponent();
		BindingContext = detallesAutoViewModel;
	}
}