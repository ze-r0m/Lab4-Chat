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
        
        // ��������� ������ ����� ����� � ��������� ������
        DataContext = new MainWindowDataContext();
        
    }

    private async void ButtonLogin_OnClick(object? sender, RoutedEventArgs e)
    {
        // �������� ������ ���������
        if (DataContext is MainWindowDataContext data)
        {
            

            // ���������� ���� ������������
            await SupabaseClient.Client.Auth.SignIn(data.Login, data.Password);

            // ���������� ���� � �������
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
    //    // �������� ������ ���������
    //    if (DataContext is MainWindowDataContext data)
    //    {
    //        // ���������� ����������� ������������
    //        await SupabaseClient.Client.Auth.SignUp(data.Login, data.Password);
    //    }
    //}
}

// ����� ���������� ������ ����� �����
public class MainWindowDataContext
{
    public string Login { get; set; } = "";
    public string Password { get; set; } = "";
}