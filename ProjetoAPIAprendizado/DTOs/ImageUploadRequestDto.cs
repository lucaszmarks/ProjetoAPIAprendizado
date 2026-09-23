using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


namespace ProjetoAPIAprendizado.DTOs
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string? FileDescription { get; set; }




    }
}
