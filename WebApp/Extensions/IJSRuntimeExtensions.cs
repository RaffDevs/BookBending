using Microsoft.JSInterop;

namespace WebApp.Extensions;

public static class IJSRuntimeExtensions
{
    public static ValueTask<object> SetInLocalStorage(this IJSRuntime js, string key, string content)
    {
        return js.InvokeAsync<object>("localStorage.setItem", key, content);
    }

    public static ValueTask<string> GetItemInLocalStorage(this IJSRuntime js, string key)
    {
        return js.InvokeAsync<string>("localStorage.getItem", key);
    }

    public static ValueTask<object> RemoveItemInLocalStorage(this IJSRuntime js, string key)
    {
        return js.InvokeAsync<object>("localStorage.removeItem", key);
    }
}