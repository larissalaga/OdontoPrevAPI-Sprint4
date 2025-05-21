using OdontoPrevAPI.Dtos;
using OdontoPrevAPI.Models;

namespace OdontoPrevAPI.Repositories.Interfaces
{
    public interface IAnaliseRaioXRepository
    {
        Task<AnaliseRaioX> Create(AnaliseRaioXDtos analiseRaioX);
        Task<AnaliseRaioX> Update(int idRaioX, AnaliseRaioXDtos analiseRaioX);
        public void Delete(int idRaioX);
        Task<List<AnaliseRaioX>> GetAll();
        Task<AnaliseRaioX> GetById(int idRaioX);
        Task<bool> DeleteByIdRaioX(int raioX);
    }
}
