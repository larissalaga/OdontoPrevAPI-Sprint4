using OdontoPrevAPI.Dtos;
using OdontoPrevAPI.Models;

namespace OdontoPrevAPI.Repositories.Interfaces
{
    public interface IPacienteDentistaRepository
    {
        Task<PacienteDentista> Create(int idDentista, int idPaciente);
        Task<bool> Delete(string dsCro, string nrCpf);
        Task<bool> DeleteByIdPaciente(int id_paciente);
        Task<List<PacienteDentista>> GetAll();
        Task<PacienteDentista> GetById(int id_paciente, int id_dentista);
        Task<List<PacienteDentista>> GetByPaciente(int id_paciente);        
    }
}
