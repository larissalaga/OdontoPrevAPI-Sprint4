using OdontoPrevAPI.Dtos;
using OdontoPrevAPI.Models;

namespace OdontoPrevAPI.Repositories.Interfaces
{
    public interface IPlanoRepository
    {
        Task<Plano> Create(PlanoDtos plano);
        Task<Plano> Update(int id, PlanoDtos plano);
        Task<Plano> UpdateByCodigo(string dsCodigoPlano, string nmPlano);
        Task<bool> DeleteById(int id);
        Task<bool> DeleteByCodigoPlano(string dsCodigoPlano);
        Task<List<Plano>> GetAll();
        Task<Plano> GetById(int id);
        Task<Models.Plano> GetByDsCodigoPlano(string dsCodigoPlano);
    }
}
