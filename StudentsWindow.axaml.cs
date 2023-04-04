using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using Supabase;
using Supabase.Realtime.PostgresChanges;

namespace Lab4;

public partial class StudentsWindow : Window
{
    public StudentsWindow()
    {
        InitializeComponent();

        // ��������� ������ ���� ��������� � ��������� ������ ��������� ������
        DataContext = new StudentsWindowDataContext();
    }
}

public class StudentsWindowDataContext : INotifyPropertyChanged
{
    // ������� ��� ����������� ���������� �� ����������
    public event PropertyChangedEventHandler? PropertyChanged;

    // ������ ���������
    public IEnumerable<StudentsRow?> StudentsList { get; set; } = new List<StudentsRow>();

    public StudentsWindowDataContext()
    {
        // �������� ����������� ������� �������� �� ���������� ������
        Subscribe();

        // �������� ����������� ������� �������� ������
        LoadData();
    }

    public async void Subscribe()
    {
        // ������������� �� ������� ���������� ������ � �������
        await SupabaseClient.Client.From<StudentsRow>().On(
            Client.ChannelEventType.Insert,
            OnStudentsInserted
        );
    }

    private void OnStudentsInserted(object sender, PostgresChangesEventArgs e)
    {
        // �������� ����� ������ ��������
        var newStudent = e.Response?.Model<StudentsRow>();

        // ��������� � ������ ���������
        StudentsList = StudentsList.Append(newStudent);

        // �������� ������� ��������� ������
        OnPropertyChanged(nameof(StudentsList));
    }

    public async void LoadData()
    {
        // ���������� ������ � ���� ������ �� ������ ������ ���������
        var data = await SupabaseClient.Client.From<StudentsRow>().Get();

        // ��������� ���������� ������ � ������ ���������
        StudentsList = data.Models;

        // �������� ������� ��������� ������
        OnPropertyChanged(nameof(StudentsList));
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}