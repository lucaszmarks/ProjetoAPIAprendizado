using System.Text.Json;

namespace ProjetoAPIAprendizado.Middlewares
{
    public class GlobalErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorMiddleware(RequestDelegate next)
        {
            _next = next; // _next representa o "próximo passo" do túnel
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Tenta deixar a requisição passar normalmente pro Controller
                await _next(context);
            }
            catch (Exception ex)
            {
                // Se QUALQUER erro estourar na API inteira, cai aqui!
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                // Montamos um JSON amigável para o usuário
                var resposta = new
                {
                    mensagem = "Ops! Ocorreu um erro interno no nosso servidor. Tente novamente mais tarde.",
                    detalheTecnico = ex.Message // Em produção, a gente esconderia isso!
                };

                var json = JsonSerializer.Serialize(resposta);
                await context.Response.WriteAsync(json);
            }
        }
    }
}