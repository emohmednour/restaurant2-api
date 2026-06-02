using Restaurants.Api.Extensions;
using Restaurants.Api.Middlewares;
using Restaurants.Application.Extensions;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Serilog;

try{
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//add two row to gen swagger  


builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.


var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeders>();

await seeder.Seed();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();
app.UseHttpsRedirection();
app.UseSerilogRequestLogging();



app.MapGroup("api/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

//for dev only not for prod
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
}
catch(Exception ex){
    log.Fital("Application Startup Failed");
}
finally(){
    log.CloseAndFlash();
}
public partial class Program { }