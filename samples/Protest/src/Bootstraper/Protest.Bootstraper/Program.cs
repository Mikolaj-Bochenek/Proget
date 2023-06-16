var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddProget(builder.Configuration, "proget")
    .AddModules(builder.Configuration)
    .AddProgetFramework();

var app = builder.Build();

app.UseProgetFramework();

app.MapControllers();

app.Run();
