using System.Text.Json;
using System.Threading.Tasks;
using MyApi.Models;
using MongoDB.Driver;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Identity.Data;
using System.Runtime.InteropServices;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MyApi.Services;

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
            builder.Services.AddScoped<VenueService>();

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

            app.MapGet("/api/get_user/safe", async (UserService userService, string userJsonId) =>
            {
                var jsonResult = await userService.GetUserByIdAsyncJSON_safe(userJsonId);
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

            app.MapGet("/api/get_all_venues", async (VenueService venueService) =>
            {
                await Console.Out.WriteLineAsync("api/get_all_venues");
                var jsonResult = await venueService.GetAllVenuesAsyncJSON();
                return Results.Ok(jsonResult);
            });

            app.MapGet("/api/get_event", async (EventService eventService, string json) =>
            {
                var jsonResult = await eventService.GetEventByIdAsyncJSON(json);
                return Results.Ok(jsonResult);
            });

            app.MapGet("/api/get_all_venues_strings", async (VenueService venueService) =>
            {
                await Console.Out.WriteLineAsync("api/get_all_venues_strings");
                var not_jsonResult = await venueService.GetAllVenuesAsync_StringsJSON();
                Console.WriteLine(not_jsonResult);
                return Results.Ok(not_jsonResult);
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
                await Console.Out.WriteLineAsync(registerRequest);

                var result = await userService.RegisterUserAsyncJSON(registerRequest);
                await Console.Out.WriteLineAsync(result);
                await Console.Out.WriteLineAsync("hi4");
                var email = await userService.GetEmailAsyncJSON(registerRequest);
                await Console.Out.WriteLineAsync("email: " + email);
                var userjson = await userService.GetUserByEmailAsyncJSON(email);
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
                var email = await userService.GetEmailAsyncJSON(loginRequest);
                await Console.Out.WriteLineAsync("email: " + "{\"email\": \"" + email + "\"}");
                var userjson = await userService.GetUserByEmailAsyncJSON("{\"email\": \"" + email + "\"}");
                await Console.Out.WriteLineAsync("userjson: " + userjson);
                await Console.Out.WriteLineAsync("message: " + userService.GetUserIDAsyncJSON(userjson));
                return Results.Ok(new { message = userService.GetUserIDAsyncJSON(userjson) });
                }
                else
                {
                    return Results.Unauthorized();
                }
            });

            app.MapPost("/api/post_user_email", async (HttpRequest request, UserService userService) =>
            {
                using var reader = new StreamReader(request.Body);
                var jsonString = await reader.ReadToEndAsync();
                var result = await userService.UpdateUserEmailAsyncJSON(jsonString);
                Console.WriteLine(jsonString);
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

            app.MapPost("/api/post_event", async (HttpRequest request, EventService eventService) =>
            {
                using var reader = new StreamReader(request.Body);
                var jsonString = await reader.ReadToEndAsync();
                var result = await eventService.AddEventAsyncJSON(jsonString);
                return Results.Ok(result);
            });

            app.MapPost("/api/post_subscribe_user", async (HttpRequest request, EventService eventService, UserService userService) =>
            {
                using var reader = new StreamReader(request.Body);
                var jsonString = await reader.ReadToEndAsync();
                Console.WriteLine(jsonString);
                var eventResult = await eventService.AddAttendeeAsyncJSON(jsonString);
                var userResult = await userService.AddEventAsyncJSON(jsonString);
                return Results.Ok(eventResult + "\n" + userResult);
            });

            app.Run("http://localhost:5258");
        }
    }
}
