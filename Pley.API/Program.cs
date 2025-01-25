using Microsoft.EntityFrameworkCore;
using Pley.API.Model;
using Pley.API.Service;
using Pley.API.Repo;
using Pley.API.Util;
using Pley.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add cbcontext and connect it to connection string
builder.Services.AddDbContext<PleyContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("PLEY")));

//Add service dependencies
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

//Add repo dependencies
builder.Services.AddScoped<IStoreRepo, StoreRepo>();
builder.Services.AddScoped<IReviewRepo, ReviewRepo>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();

//Use singleton for utilies
builder.Services.AddSingleton<Utility>(); 

//Add controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();