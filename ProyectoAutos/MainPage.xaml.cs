using ProyectoAutos.ViewModels;

namespace ProyectoAutos;

public partial class MainPage : ContentPage
{

	public MainPage(ListadoAutosViewModel listadoAutosViewModel)
	{
		InitializeComponent();
		BindingContext= listadoAutosViewModel;
	}
}



