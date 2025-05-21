using OdontoPrevAPI.Models;
using OdontoPrevAPI.Dtos;

namespace OdontoPrevAPI.Repositories.Interfaces
{
    public interface IRaioXRepository
    {
        Task<RaioX> Create(RaioXDtos raioX);
        Task<RaioX> Update(int id, RaioXDtos raioX);
        public void Delete(int id);
        Task<List<RaioX>> GetAll();
        Task<RaioX> GetById(int id);
        Task<bool> DeleteByIdPaciente(int idPaciente);
        Task<List<Models.RaioX>> GetByIdPaciente(int idPaciente);
    }
}
