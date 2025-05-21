using OdontoPrevAPI.Dtos;
using OdontoPrevAPI.Models;

namespace OdontoPrevAPI.Repositories.Interfaces
{
    public interface IPerguntasRepository
    {
        Task<Perguntas> Create(PerguntasDtos perguntas);
        public void Delete(int IdPergunta);
        Task<List<Perguntas>> GetAll();
        Task<Perguntas> GetById(int IdPergunta);
        Task<Perguntas> GetPerguntaAleatoriaAsync();
        Task<Models.Perguntas> GetProximaPerguntaAsync(int idPerguntaAtual);
    }
}
