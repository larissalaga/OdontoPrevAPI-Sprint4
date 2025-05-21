using OdontoPrevAPI.Dtos;
using OdontoPrevAPI.Models;

namespace OdontoPrevAPI.Repositories.Interfaces
{
    public interface IRespostasRepository
    {
        Task<Respostas> Create(RespostasDtos respostas);
        Task<Respostas> Update(int id, RespostasDtos respostas);
        Task<bool> Delete(int id);
        Task<List<Respostas>> GetAll();
        Task<Respostas> GetById(int id);

    }
}
