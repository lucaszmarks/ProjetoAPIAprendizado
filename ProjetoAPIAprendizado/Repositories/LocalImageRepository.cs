using ProjetoAPIAprendizado.Context; 
using ProjetoAPIAprendizado.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;




namespace ProjetoAPIAprendizado.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApiDbContext _dbContext;

        // Injetamos o ambiente (para saber onde é a pasta wwwroot), 
        // o contexto HTTP (para montar a URL) e o banco de dados
        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, ApiDbContext dbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public async Task<Image> Upload(Image image)
        {
            // Define o caminho físico onde a imagem será guardada (pasta "Images" dentro do wwwroot)
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtension}");
            
            // Copia o ficheiro da memória para a pasta física
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // Monta a URL pública (ex: https://localhost:7276/Images/nome_da_foto.jpg)
            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            // Salva apenas os metadados (caminho, nome, tamanho) na base de dados SQLite
            await _dbContext.Images.AddAsync(image);
            await _dbContext.SaveChangesAsync();

            return image;

        }
    }
}
