using System;
using System.Windows;
using ToDoService.DesktopClient.Common;
using ToDoService.DesktopClient.Models;

namespace ToDoService.DesktopClient;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var toDoTask = new ToDoTask
        {
            Id = Convert.ToInt32(IdTextBox.Text),
            Name = NameTextBox.Text,
            Description = DescriptionTextBox.Text,
            CreatedDate = DateTime.Now,
            DeadlineDate = Convert.ToDateTime(DeadlineDatePicker.Text),
            IsCompleted = false
        };
        using var toDoHttpClient = new ToDoHttpClient("http://localhost:5105");
        if (await toDoHttpClient.Create(toDoTask))
        {
            MessageBox.Show("Данные отправлены");
        }
    }
}