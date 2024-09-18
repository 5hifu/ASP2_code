using BlazorApp.Components;
using BlazorApp.Data;
using BlazorApp.HostedServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<PeopleDbContext>(options =>
    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-blazorapp;Trusted_Connection=True;MultipleActiveResultSets=true"));

builder.Services.AddControllers();

builder.Services.AddScoped<CounterService>();

builder.Services.AddScoped<PeopleDataset>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<EmailsHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

//app.MapGet("/seed", (PeopleDataset ds, PeopleDbContext db) =>
//{
//    try
//    {
//        var people = ds.GetPeople();
//        db.People.AddRange(people);
//        db.SaveChanges();
//        return "ok";
//    }
//    catch(Exception ex)
//    {
//        return ex.Message;
//    }

//});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAntiforgery();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
