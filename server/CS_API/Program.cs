    using System.Text.Json;
    using System.Threading.Tasks;
using MongoDB.Driver;

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

            var app = builder.Build();
                app.UseCors(policy => policy
                .AllowAnyOrigin());

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



            app.Run("http://localhost:5258");
            }
        }
    }
