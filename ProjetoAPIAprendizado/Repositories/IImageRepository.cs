using ProjetoAPIAprendizado.Models;


namespace ProjetoAPIAprendizado.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);

    }
}
