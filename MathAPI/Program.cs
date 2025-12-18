using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();


var rewrite = new RewriteOptions()
    .AddRedirect("^models\\.html$", "/models", statusCode: 301)
    .AddRedirect("^index\\.html$", "/", statusCode: 301)
    .AddRewrite("^models/?$", "models.html", true)
    .AddRewrite("^home/?$", "index.html", true);

// Configure the HTTP request pipeline.
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(new
        {
            error = "Internal server error",
            detail = "An unexpected error occurred."
        });
    });
});

app.UseRewriter(rewrite);   // Add URL rewrite rules

app.UseDefaultFiles();      // Serve default files (index.html)
app.UseStaticFiles();       // Serve static files

app.MapControllers();       // Map controller routes

app.Run();                  // Run the application