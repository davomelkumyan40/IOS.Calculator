using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace IOS.Calculator
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("SF-Pro-Text-Medium.otf", "SFPRO-Medium");
                    fonts.AddFont("SF-Pro-Text-Regular.otf", "SFPRO-Regular");
                    fonts.AddFont("SF-Pro-Text-Semibold.otf", "SFPRO-Semibold");
                    fonts.AddFont("SF-Pro-Text-Thin.otf", "SFPRO-Thin");
                }).ConfigureLifecycleEvents(events =>
                {
#if ANDROID
              
                events.AddAndroid(android => android.OnCreate((activity, bundle) => MakeStatusBarTranslucent(activity)));
      
                static void MakeStatusBarTranslucent(Android.App.Activity activity)
                {
             
                activity.Window.SetNavigationBarColor(Android.Graphics.Color.ParseColor("#000000"));

                }
#endif
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
