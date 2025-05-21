using OdontoPrevAPI.Dtos;
using OdontoPrevAPI.Models;

namespace OdontoPrevAPI.Repositories.Interfaces
{
    public interface IPacienteRepository
    {
        Task<Paciente> Create(PacienteDtos paciente);
        Task<Paciente> Update(string nrCpf, PacienteDtos paciente);
        Task<Models.Paciente> UpdateById(int id, PacienteDtos paciente);
        Task<Models.Paciente> UpdateByCPF(string cpf, PacienteDtos paciente);
        
        Task<List<Paciente>> GetAll();
        Task<Paciente> GetByNrCpf(string nrCpf);
        Task<Paciente> GetById(int id);
        Task<bool> DeleteById(int id);
        Task<bool> DeleteByCPF(string nr_cpf);
    }
}
