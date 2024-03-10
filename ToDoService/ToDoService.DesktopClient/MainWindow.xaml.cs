using System;
using System.Windows;
using Microsoft.Win32;
using ToDoService.Client.Models;
using ToDoService.DesktopClient.Common;

namespace ToDoService.DesktopClient;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void ButtonHttp_OnClick(object sender, RoutedEventArgs e)
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

    private async void SendFileButton_OnClick(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog(this) != true) return;
        using var toDoHttpClient = new ToDoHttpClient("http://localhost:5105");
        if (await toDoHttpClient.UploadFile(openFileDialog.FileName))
        {
            MessageBox.Show($"Файл {openFileDialog.FileName} отправлен");
        }
    }

    private async void ButtonRefit_OnClick(object sender, RoutedEventArgs e)
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
        var toDoRefitClient = new ToDoRefitClient("http://localhost:5105");
        await toDoRefitClient.Create(toDoTask);
        MessageBox.Show("Данные отправлены");
    }
}