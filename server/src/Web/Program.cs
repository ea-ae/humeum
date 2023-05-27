namespace Web;

/// <summary>
/// Program entrypoint. Used by test project hooks.
/// </summary>
public class Program {
    static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.ConfigureApplicationServices();
        builder.Services.ConfigureInfrastructureServices(builder.Configuration);
        builder.Services.ConfigureWebServices();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment()) {
            app.MigrateDatabase();
            app.UseExceptionHandler("/Error");
        }

        app.UseStaticFiles();
        app.UseOpenApi();

        if (!app.Environment.IsDevelopment()) {
            app.UseSwaggerUi3();
        }

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
