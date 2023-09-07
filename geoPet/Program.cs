using geoPet.Helpers;
using geoPet.Repositories;
using geoPet.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<GeoPetContext>();
builder.Services.AddScoped<PositionRepository>();
builder.Services.AddScoped<PositionService>();
builder.Services.AddScoped<OwerRepository>();
builder.Services.AddScoped<OwerService>();
builder.Services.AddScoped<PetRepository>();
builder.Services.AddScoped<PetService>();
builder.Services.AddScoped<SearcheAddressService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
