var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddProget(builder.Configuration, "proget")
    .AddCore(builder.Configuration)
    .AddCommands()
    .AddEvents()
    .AddMessaging(medium => medium.AddRabbitMq());

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
