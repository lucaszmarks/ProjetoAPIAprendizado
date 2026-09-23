using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ProjetoAPIAprendizado.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using ProjetoAPIAprendizado.Context;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Adiciona o botão de cadeado e a configuração do Token
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT que você recebeu no Login."
    });

    // Avisa o Swagger que ele precisa mandar o token em todas as requisições protegidas
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
}); 


builder.Services.AddDbContext<ApiDbContext>(options =>options.UseSqlite("DataSource=meubanco.db"));
//  Registra o contexto de segurança apontando para a string nova
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("AuthConnectionString")));
// Configura o motor do Identity
builder.Services.AddIdentityCore<IdentityUser>().AddRoles<IdentityRole>().AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("MinhaAPI").AddEntityFrameworkStores<AuthDbContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Permite que o repositório acesse os dados da requisição (URL, Host, etc)
builder.Services.AddHttpContextAccessor();

// Injeta o Repositório de Imagens
builder.Services.AddScoped<IImageRepository, LocalImageRepository>();

// Informamos ao C# que usaremos a Autenticação baseada em JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
//  Definimos as regras rigorosas de validação do Crachá VIP
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
    options.Events = new JwtBearerEvents
    {
        // Dispara quando o usuário não tem o Token (Erro 401)
        OnChallenge = context =>
        {
            context.HandleResponse(); 
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            var result = System.Text.Json.JsonSerializer.Serialize(new { erro = "Acesso negado. Você precisa estar logado (Token ausente ou inválido)." });
            return context.Response.WriteAsync(result);
        },
        // Dispara quando o usuário tem o Token, mas não tem o Cargo certo (Erro 403)
        OnForbidden = context =>
        {
            context.Response.StatusCode = 403;
            context.Response.ContentType = "application/json";
            var result = System.Text.Json.JsonSerializer.Serialize(new { erro = "Permissão negada. Apenas Administradores podem realizar esta ação." });
            return context.Response.WriteAsync(result);
        }
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

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});

app.Run();
