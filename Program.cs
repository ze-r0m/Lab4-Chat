using Avalonia;
using System;


namespace Lab4;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();


}
public static class SupabaseClient
{
    // Свойство, в котором хранится сам клиент
    public static Supabase.Client Client { get; set; }

    static SupabaseClient()
    {
        // Аттрибуты подключения к Supabase
        const string url = "https://jjvorbeigbftxrbvxrlr.supabase.co";
        const string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Impqdm9yYmVpZ2JmdHhyYnZ4cmxyIiwicm9sZSI6ImFub24iLCJpYXQiOjE2Nzk5OTQ4MTIsImV4cCI6MTk5NTU3MDgxMn0.3vOlhHpGq2DWbs2WfAYFKw8pQzN6q1ZczoOn5s-VAYg";

        // Настройки подключения
        var options = new Supabase.SupabaseOptions
        {
            AutoConnectRealtime = true
        };

        // Создаем клиента
        Client = new Supabase.Client(url, key, options);

        // Подключаемся
        Connect();
    }

    private static async void Connect()
    {
        // Ассинхронно подключаемся
        await Client.InitializeAsync();
    }
}
