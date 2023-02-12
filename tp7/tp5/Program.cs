global using tp5.Repositories;
global using tp5.Models;
global using tp5.ViewModels;
global using static tp5.Util.SessionUtil;
global using NLog;
global using AutoMapper;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Session;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Data.Sqlite;
global using System.Diagnostics;
global using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddLogging();
//Lineas para el uso de Sesiones

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
options.IdleTimeout = TimeSpan.FromMinutes(1);
options.Cookie.HttpOnly = true;
options.Cookie.IsEssential = true;
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddTransient<IRepositorioPedido, RepositorioPedido>();
builder.Services.AddTransient<IRepositorio<Cadete>, RepositorioCadete>();
builder.Services.AddTransient<IRepositorio<Cliente>, RepositorioCliente>();
builder.Services.AddTransient<IRepositorioUsuario, RepositorioUsuario>();
//builder.Services.AddTransient<IRepositorio<Usuario>, RepositorioUsuario>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();