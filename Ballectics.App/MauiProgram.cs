using Ballectics.App.Helper;
using Ballectics.App.Pages;
using Ballectics.App.Services;
using Ballectics.App.ViewModels;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ZXing.Net.Maui.Controls;

namespace Ballectics.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseBarcodeReader()
                .UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    fonts.AddFont("Lato-Bold.ttf", "latoB");
                    fonts.AddFont("Lato-Regular.ttf", "latoR");

                    fonts.AddFont("BeautyHandwriting-Regular.ttf", "beautyR");

                    fonts.AddFont("Cormorant-Italic-VariableFont_wght.ttf", "corIV");
                    fonts.AddFont("Cormorant-VariableFont_wght.ttf", "corVW");

                    fonts.AddFont("Hamlin-Bold.ttf", "homB");
                    fonts.AddFont("Hamlin-ExtraBold.ttf", "homXB");
                    fonts.AddFont("Hamlin-Light.ttf", "homL");
                    fonts.AddFont("Hamlin-Regular.ttf", "homR");

                    fonts.AddFont("Roboto_Italic_Variable.ttf", "robotoI");
                    fonts.AddFont("Roboto_Variable.ttf", "robotoR");

                    fonts.AddFont("FontAwesomeSolid.otf", "fa");
                    fonts.AddFont("FontAwesomeBrandsRegular.otf", "fab");
                });

            builder.Services.AddTransient<AuthHeaderHandler>();

            builder.Services.AddHttpClient("BaseClient", client =>
            {
                client.BaseAddress = new Uri("https://balleticsregisterproject.onrender.com/api/");
                client.Timeout = TimeSpan.FromSeconds(180);
                client.DefaultRequestHeaders.AcceptLanguage.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("de-DE"));

            }).AddHttpMessageHandler<AuthHeaderHandler>();

            //Services

            builder.Services.Scan(scan => scan
                .FromAssemblyOf<BaseServices>()
                .AddClasses(c => c.Where(type => type.Name.EndsWith("Service")))
                .AsSelf()
                .WithSingletonLifetime()
            );

            //ViewModels

            builder.Services.Scan(scan => scan
                .FromAssemblyOf<BasePageViewModel>()
                .AddClasses(c => c.Where(type => type.Name.EndsWith("ViewModel")))
                .AsSelf()
                .WithSingletonLifetime()
            );

            //Pages
            builder.Services.Scan(scan => scan
                .FromAssemblyOf<LoginPage>()
                .AddClasses(c => c.Where(type => type.Name.EndsWith("Page")))
                .AsSelf()
                .WithSingletonLifetime()
            );

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
