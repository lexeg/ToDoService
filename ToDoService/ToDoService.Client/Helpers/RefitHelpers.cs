using Refit;

namespace ToDoService.Client.Helpers;

public static class RefitHelpers
{
    public static ITasksApi CreateTasksClient(string uri) => RestService.For<ITasksApi>(uri);
}