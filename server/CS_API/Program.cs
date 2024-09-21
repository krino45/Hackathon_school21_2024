using System.Text.Json;
using System.Threading.Tasks;
using MyApi.Models;
using MongoDB.Driver;
using System.Diagnostics.Eventing.Reader;
using Newtonsoft.Json;

namespace MyApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors();
            builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(Environment.GetEnvironmentVariable("MONGODB_URI")));
            builder.Services.AddScoped<TagService>();
            builder.Services.AddScoped<EventService>();
            builder.Services.AddScoped<UserService>();

            var app = builder.Build();
            app.UseCors(policy => policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.MapGet("/api/get_user", async (UserService userService, string userJsonId) =>
            {
                var jsonResult = await userService.GetUserByIdAsyncJSON(userJsonId);
                return Results.Ok(jsonResult);
            });

            app.MapGet("/api/get_user_preferences", async (TagService tagService) =>
            {
                var jsonResult = await tagService.GetTagCollectionJSON();
                return Results.Ok(jsonResult);
            });

            app.MapGet("/api/get_all_events", async (EventService eventService) =>
            {
                var jsonResult = await eventService.GetAllEventsAsyncJSON();
                return Results.Ok(jsonResult);
            });

            app.MapPost("/api/post_user_email", async (HttpRequest request, UserService userService) =>
            {
                using var reader = new StreamReader(request.Body);
                var jsonString = await reader.ReadToEndAsync();
                var result = await userService.UpdateUserEmailAsyncJSON(jsonString);
                return Results.Ok(result);
            });

            app.MapPost("/api/post_user_password", async (HttpRequest request, UserService userService) =>
            {
                using var reader = new StreamReader(request.Body);
                var jsonString = await reader.ReadToEndAsync();
                Console.WriteLine(jsonString);
                var result = await userService.ChangePasswordAsyncJSON(jsonString);
                return Results.Ok(result);
            });

            app.MapPost("/api/post_user_preferences", async (HttpRequest request, UserService userService) =>
            {
                using var reader = new StreamReader(request.Body);
                var jsonString = await reader.ReadToEndAsync();
                var result = await userService.ChangePreferencesAsyncJSON(jsonString);
                return Results.Ok(result);
            });

            app.Run("http://localhost:5258");
        }
    }
}
