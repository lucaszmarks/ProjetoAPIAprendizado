using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoAPIAprendizado.DTOs;
using ProjetoAPIAprendizado.Models;
using ProjetoAPIAprendizado.Repositories;

namespace ProjetoAPIAprendizado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        // Injetando o repositório
        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }


        // POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {
                // Aqui vai chamar o Repositório para guardar a imagem 
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription
                };

                // 2. Chamar o Repositório para guardar a imagem
                await _imageRepository.Upload(imageDomainModel);

                // 3. Retornar a imagem guardada (com o ID gerado e a URL pronta)
                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }

            
        

        // Método privado para validar o tamanho e a extensão do ficheiro
        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            // Verifica se a extensão é permitida
            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Extensão não suportada. Use apenas .jpg, .jpeg ou .png.");
            }

            // Verifica o tamanho máximo (exemplo: 10 Megabytes)
            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "O tamanho do ficheiro não pode ser superior a 10MB.");
            }
        }
    }
}