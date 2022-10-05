using CoinDispenserServices.Interfaces;
using CoinDispenserServices.Output;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICoinDispenser, CoinDispenserServices.CoinDispenser>();
builder.Services.AddScoped(typeof(ISuccessOrErrorActionResultPresenter<,>), typeof(SuccessOrErrorRestPresenter<,>));
builder.Services.AddCors(options =>
{
    options.AddPolicy("corsPolicy",
        build =>
        {
            build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("corsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
