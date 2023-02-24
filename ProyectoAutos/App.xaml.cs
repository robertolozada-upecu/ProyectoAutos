using ProyectoAutos.Servicios;

namespace ProyectoAutos;

public partial class App : Application
{
	public static AutoService AutoService { get; private set; }
	public App(AutoService autoService)
	{
		InitializeComponent();

		MainPage = new AppShell();
		AutoService= autoService;
	}
}
