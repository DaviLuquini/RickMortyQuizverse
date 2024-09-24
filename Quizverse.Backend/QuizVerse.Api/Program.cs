var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();

// Add services to the container.
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAnyOriginPolicy",
        builder => {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

// Adiciona servi�o de sess�o
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(120); 
    options.Cookie.HttpOnly = true; 
    options.Cookie.IsEssential = true; 
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

app.UseHttpsRedirection();

// Middleware de sess�o deve ser adicionado antes da autoriza��o
app.UseSession();

app.UseCors("AllowAnyOriginPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
