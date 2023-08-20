#region ns 
using System;
using System.Text;
using Application.Abstractions;
using Application.Abstractions.RepositoryInterfaces;
using Application.Abstractions.ServiceInterfaces;
using Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.AppDbContext;
using Persistence.RepositoryImplementations;
#endregion

#region bc
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddCors(c => c
                .AddPolicy("EasyProduce", builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EasyProduce", Version = "v1" });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

string connectionString = builder.Configuration.GetConnectionString("EasyProduceConnectionString");
builder.Services.AddDbContextPool<Context>(option => option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//builder.Services.AddDbContext<Context>();
#endregion

#region slt
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<IFarmerRepository, FarmerRepository>();
builder.Services.AddScoped<IFarmerService, FarmerService>();

builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
builder.Services.AddScoped<IManagerService, ManagerService>();

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
//builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddScoped<ITransactionProduceTypeRepository, TransactionProduceTypeRepository>();
// builder.Services.AddScoped<ITransactionProduceService, TransactionProduceService>();

builder.Services.AddScoped<IProduceRepository, ProduceRepository>();
builder.Services.AddScoped<IProduceService, ProduceService>();

builder.Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
//builder.Services.AddScoped<IProductTypeService, ProductTypeService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<IProductService, ProductService>();

 builder.Services.AddScoped<IOrderRepository, OrderRepository>();
//builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IOrderProductTypeRepository, OrderProductTypeRepository>();
// builder.Services.AddScoped<IOrderProductService, OrderProductService>();

builder.Services.AddScoped<IProduceTypeRepository, ProduceTypeRepository>();
builder.Services.AddScoped<IProduceTypeService, ProduceTypeService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
//builder.Services.AddScoped<ICartItemService, CartItemService>();

builder.Services.AddScoped<ICartItemForOrderRepository, CartItemForOrderRepository>();
//builder.Services.AddScoped<ICartItemForOrderService, CartItemForOrderService>();

builder.Services.AddScoped<IFarmerProduceTypeRepository, FarmerProduceTypeRepository>();

builder.Services.AddScoped<IJwtAuthenticationManager, JwtAuthenticationManager>();

builder.Services.AddScoped<IFileUploadServiceForWWWRoot, FileUploadServiceForWWWRoot>();

builder.Services.AddScoped<IMailService, MailService>();

builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IChatService, ChatService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
#endregion

#region mdw
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EasyProduce v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("EasyProduce");

 AdminMocking.Mock(app);    

app.Run();
#endregion
