using Microsoft.EntityFrameworkCore;
using RealEstate.WebService.Databases;
using RealEstate.WebService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRealEstateService, RealEstateService>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IImageService, ImageService>();

// Add services to the container.

builder.Services.AddDbContext<LibraryContext>(option =>
{
    string connectionString = builder.Configuration.GetConnectionString("Database");
    option.UseMySQL(connectionString);
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
