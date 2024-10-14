using OperacaoCuriosidadeMVC.Generate;
using OperacaoCuriosidadeMVC.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using OperacaoCuriosidadeMVC.Persistence.JsonData;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Http.Features;
using static OperacaoCuriosidadeMVC.Models.UserModel;
using OperacaoCuriosidadeMVC.Validation;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 1048576; // 1MB
});

builder.Services.Configure<FormOptions>(options =>
{
    options.KeyLengthLimit = 20000; // Aumentando o limite para 4096 caracteres

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddSingleton<UserDbContext>();
builder.Services.AddSingleton<OperacaoDbContext>();
builder.Services.AddScoped<GenerateId>();
builder.Services.AddScoped<GenerateOperationItemsId>();
builder.Services.AddScoped<ValidationLoginData>();
builder.Services.AddControllers();
builder.Logging.AddConsole();
builder.Services.AddSingleton<JsonFileService>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Image Upload API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


