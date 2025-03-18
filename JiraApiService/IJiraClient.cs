namespace JiraApi;

internal interface IJiraClient
{
    Task<T?> Get<T>(string path) where T : class;
    Task<T?> Post<T>(string path, object body) where T : class;
}
