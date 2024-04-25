using Microsoft.AspNetCore.Components;
using Shared.DTO;

namespace WebApp.Pages.Login;

public class LoginViewModel: ComponentBase
{
    protected LoginDTO Login { get; } = new();

    protected void Submit()
    {
        Console.WriteLine($"Data: {Login.Email}, {Login.Password}");
    }
    
}