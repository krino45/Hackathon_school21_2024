using System.Text.Json;
using System.Threading.Tasks;
using MyApi.Models;
using MongoDB.Driver;
using System.Diagnostics.Eventing.Reader;

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
                
            app.MapGet("/api/preferences", async (TagService tagService) =>
            {
                var jsonResult = await tagService.GetTagCollectionJSON();
                return Results.Ok(jsonResult);
            });

                app.MapGet("/api/get_all_events", async (EventService eventService) =>
                {
                    var jsonResult = await eventService.GetAllEventsAsyncJSON();
                    return Results.Ok(jsonResult);
                });

                    app.MapGet("/api/events", async (UserService userService) =>
                    {
                        var jsonResult = await userService.GetUserJSON();
                        return Results.Ok(jsonResult);
                    });


            app.MapPost("/api/settings", async (UserSettings userSettings, UserService userService) =>
            {
                if (userSettings == null)
                    return Results.BadRequest("Invalid data.");
                
                var result = await userService.SaveChanges(userSettings);

                return Results.Ok(result);
            });

            app.Run("http://localhost:5258");
        }
    }
}
