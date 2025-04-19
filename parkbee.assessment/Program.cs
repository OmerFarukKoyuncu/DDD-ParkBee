using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using parkbee.Application.Extensions;
using ParkBee.Application.Contracts;
using ParkBee.Application.Contracts.Implementations;
using ParkBee.Domain.Contracts;
using ParkBee.Domain.Entities;
using ParkBee.Infrastructure;
using ParkBee.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGarageRepository, GarageRepository>();
builder.Services.AddScoped<ISessionRepository, ParkingSessionRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGarageService, GarageService>();
builder.Services.AddScoped<IParkingSessionService, ParkingSessionService>();
builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();
builder.Services.AddScoped<IPingService, PingService>();
builder.Services.AddScoped<IPedestrianService, PedestrianService>();

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("ParkbeeDb"));
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("x-api-key", new OpenApiSecurityScheme
    {
        Name = "x-api-key",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme",
        In = ParameterLocation.Header,
        Description = "ApiKey must appear in header"
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "x-api-key"
                },
                In = ParameterLocation.Header
            },
            new string[]{}
        }
    });
});
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

app.UseExceptionMiddleware();
app.UseHttpsRedirection();
AddSeedData(app);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static void AddSeedData(WebApplication app)
{
    var scope = app.Services.CreateScope();
    var parkingDbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

    parkingDbContext.Garages.AddRange(new List<Garage>()
    {
        new Garage{
        GarageId = Guid.Parse("eeebe60f-89c8-45e2-bc98-060c90d70f28"),
        CreatedDate = DateTime.Now,
        Capacity = 1,
        Name = $"Garage {Guid.NewGuid()}",
        Doors = new List<Door>()
               {
                  new Door {DoorId= Guid.NewGuid(),Description="Entrance", DoorType = DoorType.Entry, DoorIpAddress = "192.168.1.1"},
                  new Door {DoorId= Guid.NewGuid(),Description="Exit", DoorType = DoorType.Exit, DoorIpAddress = "192.168.1.2"},
                  new Door {DoorId= Guid.NewGuid(),Description="Pedestrian", DoorType = DoorType.Pedestrian, DoorIpAddress = "192.168.1.3"},
               }
        },
        new Garage{
        GarageId = Guid.Parse("1eed6fa6-0cf9-4a63-92ff-321cf62bb7f8"),
        CreatedDate = DateTime.Now,
        Capacity = 1,
        Name = $"Garage {Guid.NewGuid()}",
        Doors = new List<Door>()
               {
                  new Door {DoorId= Guid.NewGuid(),Description="Entrance", DoorType = DoorType.Entry, DoorIpAddress = "192.168.1.1"},
                  new Door {DoorId= Guid.NewGuid(),Description="Exit", DoorType = DoorType.Exit, DoorIpAddress = "192.168.1.2"},
                  new Door {DoorId= Guid.NewGuid(),Description="Pedestrian", DoorType = DoorType.Pedestrian, DoorIpAddress = "192.168.1.3"},
               }
        }
    });

    parkingDbContext.Users.AddRange(Enumerable.Range(0,4).Select(x => 
    new User()
    {
        FirstName = "Omer",
        LastName = "Koyuncu",
        Email = "..@gmail.com",
        PhoneNumber = "1234567",
        CreatedDate = DateTime.UtcNow,
    }));
    parkingDbContext.SaveChanges();
}