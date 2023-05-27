//using BusinessLayer.Services.OrderService;
//using BusinessLayer.Services.StockService;
//using DataAccessLayer.Data;
//using DataAccessLayer.Models;
//using DataAccessLayer.Repositors;
//using DataAccessLayer.Repositors.Base;
//using EDIG_Api_Task.Entity;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//options.UseSqlServer(connectionString));



//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();

////builder.Services.AddScoped(typeof(IGenericRepo<Stock>), typeof(GenericRepo<Stock>));
////builder.Services.AddScoped(typeof(IGenericRepo<Order>), typeof(GenericRepo<Order>));
//builder.Services.AddScoped(typeof(IStockService), typeof(StockService));
//builder.Services.AddScoped(typeof(IOrderService), typeof(OrderService));
//builder.Services.AddScoped(typeof(IGenericRepo< >), typeof(GenericRepo< >));
//builder.Services.AddSignalR()
//                .AddHubOptions<StockPriceHub>(options =>
//                {
//                    options.EnableDetailedErrors = true; // Enable detailed error messages
//                });
////builder.Services.AddCors(options =>
////{
////    options.AddDefaultPolicy(builder =>
////    {
////        builder.AllowAnyOrigin()
////               .AllowAnyHeader()
////               .AllowAnyMethod();
////    });
////});
//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(builder =>
//    {
//        builder.WithOrigins("http://localhost:4200") // Specify the allowed origin(s)
//               .AllowAnyHeader()
//               .AllowAnyMethod()
//               .AllowCredentials(); // Allow credentials (e.g., cookies, authorization headers)
//    });
//});


//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//    app.UseDeveloperExceptionPage();

//}
//else
//{
//    app.UseExceptionHandler("/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();

////app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
//app.UseCors();
//app.UseRouting(); // Add this line for routing

//app.UseAuthorization();

////app.MapControllers();
////app.UseEndpoints(endpoints =>
////{
////    endpoints.MapHub<StockPriceHub>("/stockPriceHub"); // Map SignalR hub endpoint
////    endpoints.MapControllers();
////});

////app.MapControllers();

////app.MapHub<StockPriceHub>("/stockpricehub");
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//    endpoints.MapHub<StockPriceHub>("/stockpricehub");
//});

//app.Run();

using BusinessLayer.Services.OrderService;
using BusinessLayer.Services.StockService;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using DataAccessLayer.Repositors.Base;
using DataAccessLayer.Repositors;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped(typeof(IStockService), typeof(StockService));
builder.Services.AddScoped(typeof(IOrderService), typeof(OrderService));
builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
builder.Services.AddSignalR()
                .AddHubOptions<StockPriceHub>(options =>
                {
                    options.EnableDetailedErrors = true; // Enable detailed error messages
                });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Specify the allowed origin(s)
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials(); // Allow credentials (e.g., cookies, authorization headers)
    });
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors();
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<StockPriceHub>("/stockpricehub");
});

app.Run();
