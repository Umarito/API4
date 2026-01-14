
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookLoanService, BookLoanService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ApplicationDBContext>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.SetMinimumLevel(LogLevel.Information);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();