using Microsoft.AspNetCore.Builder;

namespace MyApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = builder.Build();

            app.MapGet("/message", () => "Hello from C#!");

            app.Run("http://localhost:5258");
        }
    }
}
