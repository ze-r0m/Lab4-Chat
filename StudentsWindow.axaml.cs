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

        // Связываем данные окна студентов и экземпляр класса контекста данных
        DataContext = new StudentsWindowDataContext();
    }
}

public class StudentsWindowDataContext : INotifyPropertyChanged
{
    // Событие для уведомления интерфейса об изменениях
    public event PropertyChangedEventHandler? PropertyChanged;

    // Список студентов
    public IEnumerable<StudentsRow?> StudentsList { get; set; } = new List<StudentsRow>();

    public StudentsWindowDataContext()
    {
        // Вызываем асинхронную функцию подписки на добавление данных
        Subscribe();

        // Вызываем асинхронную функцию загрузки данных
        LoadData();
    }

    public async void Subscribe()
    {
        // Подписываемся на событие добавления данных в таблицу
        await SupabaseClient.Client.From<StudentsRow>().On(
            Client.ChannelEventType.Insert,
            OnStudentsInserted
        );
    }

    private void OnStudentsInserted(object sender, PostgresChangesEventArgs e)
    {
        // Получаем новую строку студента
        var newStudent = e.Response?.Model<StudentsRow>();

        // Добавляем в список студентов
        StudentsList = StudentsList.Append(newStudent);

        // Вызываем событие изменения данных
        OnPropertyChanged(nameof(StudentsList));
    }

    public async void LoadData()
    {
        // Отправляем запрос к базе данных на чтение списка студентов
        var data = await SupabaseClient.Client.From<StudentsRow>().Get();

        // Сохраняем полученные данные в список студентов
        StudentsList = data.Models;

        // Вызываем событие изменения данных
        OnPropertyChanged(nameof(StudentsList));
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}