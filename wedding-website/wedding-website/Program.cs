using wedding_website.Data;
using wedding_website.Models;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://127.0.0.1:5500");
                          policy.AllowAnyMethod();
                          policy.AllowAnyHeader();
                          policy.AllowCredentials();
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MongoDbService>();
// Add services to the container.
builder.Services.Configure<WeddingDatabaseSettings>(
    builder.Configuration.GetSection("WeddingStoreDatabase"));

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


app.UseCors("AllowLocalhost"); // Enable CORS before Authorization



app.UseAuthorization();

app.MapControllers();



app.Run();
