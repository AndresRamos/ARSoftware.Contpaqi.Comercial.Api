using System.Reflection;
using System.Text.Json.Serialization;
using Api.Core.Application;
using Api.Core.Application.Requests.Commands.CreateApiRequest;
using Api.Core.Domain.Common;
using Api.Infrastructure;
using Api.Infrastructure.Persistence;
using Api.Presentation.WebApi.Authentication;
using ARSoftware.Contpaqi.Api.Common.Domain;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
    {
        options.ReturnHttpNotAcceptable = true;
        //options.Filters.Add<ApiKeyAuthFilter>();
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.TypeInfoResolver = new PolymorphicTypeResolver();
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddApplicationServices().AddInfrastructureServices(builder.Configuration);
//builder.Services.ConfigureHttpJsonOptions(options => { options.SerializerOptions.TypeInfoResolver = new PolymorphicTypeResolver(); });

builder.Services.AddScoped<ApiKeyAuthFilter>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "AR Software - CONTPAQi Comercial API",
            Version = "v1",
            Description = "Use this API to create requests to be processed in CONTPAQi Comercial.",
            Contact = new OpenApiContact { Name = "AR Software", Email = "andres@arsoft.net", Url = new Uri("https://www.arsoft.net") }
        });

    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(CreateApiRequestCommand).Assembly.GetName().Name}.xml"));
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(ApiRequest).Assembly.GetName().Name}.xml"));

    c.UseAllOfForInheritance();
    c.UseOneOfForPolymorphism();

    //c.SelectSubTypesUsing(baseType =>
    //{
    //    if (baseType == typeof(ContpaqiRequest))
    //        return typeof(ApiRequest).Assembly.GetTypes()
    //            .Where(type => typeof(ContpaqiRequest).IsAssignableFrom(type) && type != typeof(ContpaqiRequest) && !type.IsAbstract)
    //            .ToArray();

    //    if (baseType == typeof(ContpaqiResponse))
    //        return typeof(ApiRequest).Assembly.GetTypes()
    //            .Where(type => typeof(ContpaqiResponse).IsAssignableFrom(type) && type != typeof(ContpaqiResponse) && !type.IsAbstract)
    //            .ToArray();

    //    return Enumerable.Empty<Type>();
    //});
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    //app.UseDeveloperExceptionPage();
    // Initialise and seed database
    using (IServiceScope scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync();
    }

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
