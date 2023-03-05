using Microsoft.Extensions.Logging;
using ProyectoAutos.Servicios;
using ProyectoAutos.ViewModels;
using ProyectoAutos.Vistas;

namespace ProyectoAutos;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		var pathDB = Path.Combine(FileSystem.AppDataDirectory, "auto.db");
		
		builder.Services.AddSingleton<AutoService>(servicios => ActivatorUtilities.CreateInstance<AutoService>(servicios, pathDB));
		builder.Services.AddTransient<AutoApiService>();

        builder.Services.AddSingleton<ListadoAutosViewModel>();
        builder.Services.AddTransient<DetallesAutoViewModel>();

        builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<DetallesAutoPage>();

        return builder.Build();
	}
}
