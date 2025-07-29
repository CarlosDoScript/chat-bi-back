using Chat.Bi.API.Extensions;
using Chat.Bi.API.Middlewares;
using Chat.Bi.Application.Commands.Usuario.CriarContaUsuario;
using Chat.Bi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(options => options.Filters.Add(typeof(FluentValidationMensagensFilter)))
       .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CriarContaUsuarioCommand>());

builder.Services.ResolveDependencias(builder.Configuration);
builder.Services.AdicionarSwaggerDocV1();
builder.AddAutenticacaoOption(builder.Configuration);

var app = builder.Build();

Migration(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}    

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();
app.Run();

static IServiceScope Migration(WebApplication app)
{
    var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ChatBiDbContext>();
    context.Database.Migrate();
    return scope;
}