using OdontoPrevAPI.Dtos;
using OdontoPrevAPI.Models;

namespace OdontoPrevAPI.Repositories.Interfaces
{
    public interface ICheckInRepository
    {
        Task<CheckIn> Create(CheckInDtos checkIn);
        public void Delete(int id);
        Task<List<CheckIn>> GetAll();
        Task<CheckIn> GetById(int id);
        Task<List<CheckIn>> GetByIdPaciente(int idPaciente);
        Task<List<Models.CheckIn>> GetByIdPacienteLastCheckins(int idPaciente, int nrCheckins);
        Task<bool> DeleteByIdPaciente(int idPaciente);
    }
}
