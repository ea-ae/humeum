using Application.Common.Interfaces;

using Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
var dbPath = Path.Combine(appDataPath, "humeum.sqlite"); // todo: move outta here

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<IAppDbContext, AppDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
} else {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();
