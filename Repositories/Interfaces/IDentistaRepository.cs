using OdontoPrevAPI.Dtos;
using OdontoPrevAPI.Models;

namespace OdontoPrevAPI.Repositories.Interfaces
{
    public interface IDentistaRepository
    {
        Task<Dentista> Create(DentistaDtos dentista);
        Task<Dentista> UpdateByCRO(string dsCro, DentistaDtos dentista);
        Task<Dentista> UpdateById(int id, DentistaDtos dentista);
        Task<bool> DeleteByCRO(string dsCro);
        Task<bool> DeleteById(int id);
        Task<List<Dentista>> GetAll();
        Task<Dentista> GetByDsCro(string dsCro);
        Task<Models.Dentista> GetById(int id);
        Task<List<Models.Dentista>> GetByIdList(List<int> id);
    }
}
