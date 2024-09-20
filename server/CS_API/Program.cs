using System.Text.Json;
using System.Threading.Tasks;
using MyApi.Controllers;

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

            app.MapGet("/api/preferences", async (TagController tagService) =>
            {
                var jsonResult = await tagService.GetTagCollectionJSON();
                return Results.Ok(jsonResult);
            });

            app.Run("http://localhost:5258");
        }
    }
}
