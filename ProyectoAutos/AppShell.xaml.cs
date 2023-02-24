using ProyectoAutos.Vistas;

namespace ProyectoAutos;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(DetallesAutoPage), typeof(DetallesAutoPage));
	}
}
