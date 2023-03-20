namespace Web;

/// <summary>
/// Program entrypoint. Used by test project hooks.
/// </summary>
public class Program {
    static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // builder.Configuration.AddEnvironmentVariables();

        builder.Services.ConfigureApplicationServices();
        builder.Services.ConfigureInfrastructureServices(builder.Configuration);
        builder.Services.ConfigureWebServices();

        // builder.Logging.AddConsole().AddFilter(level => level >= LogLevel.Trace);

        var app = builder.Build();

        if (app.Environment.IsDevelopment()) {
            //app.UseSwagger();
            //app.UseSwaggerUI();
        } else {
            app.MigrateDatabase();
            app.UseExceptionHandler("/Error");
            //app.UseHsts();
        }

        //app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseOpenApi();
        app.UseSwaggerUi3();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
