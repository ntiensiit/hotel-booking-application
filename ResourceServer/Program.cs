using Infrastructure.MediatR;
using MediatR;
using Port.Driven.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Scan(scan => scan
    .FromAssembliesOf(typeof(IDomainEventHandler<>))
    .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
    .AsImplementedInterfaces()
    .WithScopedLifetime());

builder.Services.AddTransient(typeof(INotificationHandler<>), typeof(MediatRDomainEventAdapter<>));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(MediatRDomainEventPublisher).Assembly);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();