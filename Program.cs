using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 👇 Agregar servicios MVC y API
builder.Services.AddControllersWithViews();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddEndpointsApiExplorer(); // Necesario para Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mi API Híbrida",
        Version = "v1",
        Description = "API para login y operaciones"
    });
});

var app = builder.Build();



app.UseStaticFiles(); // Para css, js, imagenes
app.UseRouting();

app.UseAuthorization();

// 👇 Activar Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API Híbrida v1");
        c.RoutePrefix = "swagger"; // Accede a /swagger
    });
}

// 👇 Mapear Controllers (API y MVC)
app.MapControllers(); // Para API

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Index}/{id?}");

app.Run();