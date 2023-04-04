using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using Supabase.Interfaces;
using Postgrest.Attributes;
using Postgrest.Models;
using System.Threading;
using System.Windows.Forms;
using System;

namespace Lab4;

[Table("Students")]
public class Student : BaseModel
{
    [PrimaryKey("id")] public int Id { get; set; }
    [Column("name")] public string Name { get; set; } = string.Empty;
    [Column("surname")] public string Surname { get; set; } = string.Empty;
    [Column("email")] public string Email { get; set; } = string.Empty;
    [Column("password")] public string Password { get; set; } = string.Empty;
}

public partial class RegisterWindow : Window
{
    public RegisterWindow()
    {
        InitializeComponent();

        // ��������� ������ ����� ����� � ��������� ������
        DataContext = new RegisterWindowDataContext();
    }

    private async void ButtonRegister_OnClick(object? sender, RoutedEventArgs e)
    {
        // �������� ������ ���������
        if (DataContext is RegisterWindowDataContext regdata)
        {
            var student = new Student
            {
                Name = regdata.Name,
                Surname = regdata.Surname,
                Email = regdata.Login,
                Password = regdata.Password
            };


            try
            {
                await SupabaseClient.Client.Auth.SignUp(regdata.Login, regdata.Password);
                await SupabaseClient.Client.From<Student>().Insert(student);

                MessageBox.Show("����������� ������ �������!", "�����",  MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������ ��� �����������: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
    }
}


public class RegisterWindowDataContext
{
    public string Name { get; set; } = "";
    public string Surname { get; set; } = "";
    public string Login { get; set; } = "";
    public string Password { get; set; } = "";
}