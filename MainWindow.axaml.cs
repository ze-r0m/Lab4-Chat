using Avalonia.Controls;
using Avalonia.Interactivity;
using Postgrest.Attributes;
using Supabase.Interfaces;


namespace Lab4;

public partial class MainWindow : Window
{
   
    public MainWindow()
    {
        InitializeComponent();
        
        // Связываем данные формы входа и экземпляр класса
        DataContext = new MainWindowDataContext();
        
    }

    private async void ButtonLogin_OnClick(object? sender, RoutedEventArgs e)
    {
        // Получаем данные контекста
        if (DataContext is MainWindowDataContext data)
        {
            

            // Производим вход пользователя
            await SupabaseClient.Client.Auth.SignIn(data.Login, data.Password);

            // Показываем окно с данными
            var dataWindow = new StudentsWindow();
            await dataWindow.ShowDialog(this);

            this.Close();
        }
    }

    private async void SignUp_OnClick(object? sender, RoutedEventArgs e)
    {
        var dataWindow = new RegisterWindow();
        await dataWindow.ShowDialog(this);
        
    }

    //private async void ButtonRegister_OnClick(object? sender, RoutedEventArgs e)
    //{
    //    // Получаем данные контекста
    //    if (DataContext is MainWindowDataContext data)
    //    {
    //        // Производим регистрацию пользователя
    //        await SupabaseClient.Client.Auth.SignUp(data.Login, data.Password);
    //    }
    //}
}

// Класс содержащий даныне формы входа
public class MainWindowDataContext
{
    public string Login { get; set; } = "";
    public string Password { get; set; } = "";
}