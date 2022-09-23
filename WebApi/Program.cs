using Infrastructure.DataContext;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DataContext>();
<<<<<<< HEAD
builder.Services.AddScoped<QuoteServices>();
=======
builder.Services.AddScoped<StudentServices>();
builder.Services.AddScoped<MentorService>();
>>>>>>> bf14f8975681761f5ffc1d44900d0f47ce01a437
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
