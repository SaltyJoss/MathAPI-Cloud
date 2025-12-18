using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
var rewrite = new RewriteOptions()
    .AddRewrite("^models/?$", "models.html", true)
    .AddRewrite("^home/?$", "index.html", true);

//app.UseHttpsRedirection();
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

app.UseDefaultFiles();      // Serve default files (index.html)

app.UseRewriter(rewrite);   // Add URL rewrite rules

app.UseStaticFiles();       // Serve static files

app.UseAuthorization();     // Authorization middleware

app.MapControllers();       // Map controller routes

app.Run();                  // Run the application