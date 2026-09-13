using Microsoft.EntityFrameworkCore;
using ProjetoAPIAprendizado;
using ProjetoAPIAprendizado.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // 1. Cria o botão "Authorize" no Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT desta maneira: Bearer {seu token}"
    });

    // 2. Diz ao Swagger para enviar o token em todas as requisições trancadas
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
}); builder.Services.AddDbContext<ApiDbContext>(options =>options.UseSqlite("DataSource=meubanco.db"));
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// 1. Informamos ao C# que usaremos a Autenticação baseada em JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
// 2. Definimos as regras rigorosas de validação do Crachá VIP
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // Ignora quem emitiu o token (por enquanto)
        ValidateAudience = false, // Ignora para quem o token foi emitido
        ValidateLifetime = true, // Exige que o token não esteja vencido
        ValidateIssuerSigningKey = true, // Exige que o token tenha a nossa assinatura oficial

        // A "Caneta" oficial que assina os nossos crachás. 
        // ATENÇÃO: Em projetos reais, essa senha fica escondida no appsettings.json!
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ChaveSecretaDaSuaAPI-PrecisaSerLonga123!@#"))
    };
});

var app = builder.Build();
app.UseMiddleware<ProjetoAPIAprendizado.Middlewares.GlobalErrorMiddleware>();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
