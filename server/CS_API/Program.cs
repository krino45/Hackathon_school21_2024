using Microsoft.AspNetCore.Builder;
using System.Text.Json;

namespace MyApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors();

            var app = builder.Build();
            app.UseCors(policy => policy
            .AllowAnyOrigin());

            app.MapGet("/message", () => JsonDocument.Parse("{\"message\": \"Hello from C#!\"}"));

            app.Run("http://localhost:5258");
        }
    }
}
