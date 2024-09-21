using System.Text.Json;
using System.Threading.Tasks;
using MyApi.Models;
using MongoDB.Driver;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Identity.Data;
using System.Runtime.InteropServices;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;

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

            app.MapPost("/register", async (HttpContext context) =>
            {
                await Console.Out.WriteLineAsync("hi1");
                var userService = context.RequestServices.GetRequiredService<UserService>();
                context.Request.EnableBuffering();
                string registerRequest;
                await Console.Out.WriteLineAsync("hi2");

                using (var reader = new StreamReader(context.Request.Body, leaveOpen: true))
                {
                    registerRequest = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }
                await Console.Out.WriteLineAsync("hi3");


                var result = await userService.RegisterUserAsyncJSON(registerRequest);
                await Console.Out.WriteLineAsync(result);
                await Console.Out.WriteLineAsync("hi4");

                var userjson = await userService.GetUserByEmailAsyncJSON(await userService.GetEmailAsyncJSON(registerRequest));
                await Console.Out.WriteLineAsync("userjson: " + userjson);
                await Console.Out.WriteLineAsync("message: " + userService.GetUserIDAsyncJSON(userjson));
                return Results.Ok(new { message = userService.GetUserIDAsyncJSON(userjson) });
            });


            app.MapPost("/login", async (HttpContext context) =>
            {
                var userService = context.RequestServices.GetRequiredService<UserService>();
                context.Request.EnableBuffering();
                string loginRequest;
                using (var reader = new StreamReader(context.Request.Body, leaveOpen: true))
                {
                    loginRequest = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0; 
                }

                await Console.Out.WriteLineAsync("log: " + loginRequest);
                if (loginRequest == null)
                {
                    return Results.BadRequest(new { message = "Invalid request" });
                }
                var result = await userService.ValidateLoginAsyncJSON(loginRequest);

                if (result)
                {
                    var userjson = await userService.GetUserByEmailAsyncJSON(await userService.GetEmailAsyncJSON(loginRequest));
                    await Console.Out.WriteLineAsync("email: " + await userService.GetEmailAsyncJSON(loginRequest));
                    await Console.Out.WriteLineAsync("json: " + loginRequest);
                    await Console.Out.WriteLineAsync("userjson: " + userjson);
                    await Console.Out.WriteLineAsync("message: " + userService.GetUserIDAsyncJSON(userjson));
                    return Results.Ok(new { message = userService.GetUserIDAsyncJSON(userjson) });
                }
                else
                {
                    return Results.Unauthorized();
                }
            });




            /*           app.MapPost("/api/settings", async (UserSettings userSettings, UserService userService) =>
                       {
                           if (userSettings == null)
                               return Results.BadRequest("Invalid data.");

                           var result = await userService.SaveChanges(userSettings);

                           return Results.Ok(result);
                       });*/

            app.Run("http://localhost:5258");
        }
    }
}
