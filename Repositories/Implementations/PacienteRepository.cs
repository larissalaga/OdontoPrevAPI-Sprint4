using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OdontoPrevAPI.Data;
using OdontoPrevAPI.Dtos;
using OdontoPrevAPI.Models;
using OdontoPrevAPI.Repositories.Interfaces;

namespace OdontoPrevAPI.Repositories.Implementations
{
    public class PacienteRepository : IPacienteRepository
    {
        private DataContext _context;

        public PacienteRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Models.Paciente> Create(PacienteDtos paciente)
        {
            var getPaciente = await _context.Paciente.FirstOrDefaultAsync(x => x.NrCpf == paciente.NrCpf);
            if (getPaciente != null)
            {
                throw new Exception("Paciente já cadastrado.");
            }
            else
            {
                var dtNasc = DateOnly.FromDateTime(paciente.DtNascimento);
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    BEGIN
                        pkg_crud_paciente.INSERT_PACIENTE(
                            {paciente.NmPaciente},
                            TO_DATE({dtNasc},'YYYY-MM-DD'),
                            {paciente.NrCpf},
                            {paciente.DsSexo},
                            {paciente.NrTelefone},
                            {paciente.DsEmail},
                            {paciente.IdPlano}
                        );
                    END;
                ");

                var newPaciente = await _context.Paciente.FirstOrDefaultAsync(x => x.NrCpf == paciente.NrCpf);
                return newPaciente;
            }
        }
        public async Task<bool> DeleteById(int id)
        {
            var getPaciente = await _context.Paciente.FirstOrDefaultAsync(x => x.IdPaciente == id);
            if (getPaciente == null)
            {
                return false;
            }
            else
            {
                _context.Paciente.Remove(getPaciente);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DeleteByCPF(string nr_cpf)
        {
            var getPaciente = await _context.Paciente.FirstOrDefaultAsync(x => x.NrCpf == nr_cpf);
            if (getPaciente == null)
            {
                return false;
            }
            else
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    BEGIN
                        pkg_crud_paciente.DELETE_PACIENTE({nr_cpf});
                    END;
                ");

                return true;
            }
        }

        public async Task<List<Models.Paciente>> GetAll()
        {
            var getPaciente = await _context.Paciente.ToListAsync();
            if (getPaciente != null)
            {
                foreach (var paciente in getPaciente)
                {
                    paciente.Plano = await _context.Plano.FirstOrDefaultAsync(x => x.IdPlano == paciente.IdPlano);
                }
                return getPaciente;
            }
            else
            {
                throw new Exception("Não há pacientes cadastrados.");
            }
        }

        public async Task<Models.Paciente> GetByNrCpf(string nrCpf)
        {
            var getPaciente = await _context.Paciente.FirstOrDefaultAsync(x => x.NrCpf == nrCpf);
            if (getPaciente == null)
                throw new Exception("Paciente não encontrado.");
            else
                getPaciente.Plano = await _context.Plano.FirstOrDefaultAsync(x => x.IdPlano == getPaciente.IdPlano);
            return getPaciente;
        }

        public async Task<Models.Paciente> GetById(int id)
        {
            var getPaciente = await _context.Paciente.FirstOrDefaultAsync(x => x.IdPaciente == id);
            if (getPaciente == null)
                throw new Exception("Paciente não encontrado.");
            else
                getPaciente.Plano = await _context.Plano.FirstOrDefaultAsync(x => x.IdPlano == getPaciente.IdPlano);
                return getPaciente;
        }

        public async Task<Models.Paciente> UpdateById(int id, PacienteDtos paciente)
        {
            var getPaciente = await _context.Paciente.FirstOrDefaultAsync(x => x.IdPaciente == id);
            if (getPaciente == null)
            {
                throw new Exception("Paciente não encontrado.");
            }
            else
            {
                getPaciente.NmPaciente = string.IsNullOrEmpty(paciente.NmPaciente) ? getPaciente.NmPaciente : paciente.NmPaciente;
                getPaciente.NrCpf = string.IsNullOrEmpty(paciente.NrCpf) ? getPaciente.NrCpf : paciente.NrCpf;
                getPaciente.NrTelefone = string.IsNullOrEmpty(paciente.NrTelefone) ? getPaciente.NrTelefone : paciente.NrTelefone;
                getPaciente.DsEmail = string.IsNullOrEmpty(paciente.DsEmail) ? getPaciente.DsEmail : paciente.DsEmail;
                getPaciente.DtNascimento = (paciente.DtNascimento == null) ? getPaciente.DtNascimento : paciente.DtNascimento;
                getPaciente.DsSexo = string.IsNullOrEmpty(paciente.DsSexo) ? getPaciente.DsSexo : paciente.DsSexo;
                getPaciente.IdPlano = paciente.IdPlano == 0 ? getPaciente.IdPlano : paciente.IdPlano;

                await _context.SaveChangesAsync();
                return getPaciente;
            }
        }

        public async Task<Models.Paciente> UpdateByCPF(string nrCpf, PacienteDtos paciente)
        {
            var getPaciente = await _context.Paciente.FirstOrDefaultAsync(x => x.NrCpf == nrCpf);
            if (getPaciente == null)
            {
                throw new Exception("Paciente não encontrado.");
            }
            else
            {
                var dtNasc = DateOnly.FromDateTime(paciente.DtNascimento);
                // Use the UPDATE_PACIENTE procedure from PKG_CRUD_PACIENTE
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    BEGIN
                        PKG_CRUD_PACIENTE.UPDATE_PACIENTE(
                            {nrCpf},
                            {paciente.NmPaciente},
                            {paciente.NrTelefone},
                            {paciente.DsEmail},
                            TO_DATE({dtNasc},'YYYY-MM-DD'),
                            {paciente.DsSexo},
                            {paciente.Plano?.DsCodigoPlano}
                        );
                    END;
                ");

                // Fetch the updated patient record
                var updatedPaciente = await _context.Paciente.FirstOrDefaultAsync(x => x.NrCpf == nrCpf);
                return updatedPaciente;
            }
        }

        public async Task<Models.Paciente> Update(string nrCpf , PacienteDtos paciente)
        {
            var getPaciente = await _context.Paciente.FirstOrDefaultAsync(x => x.NrCpf == paciente.NrCpf);
            if (getPaciente == null)
            {
                throw new Exception("Paciente não encontrado.");
            }
            else
            {
                getPaciente.NmPaciente = paciente.NmPaciente;
                getPaciente.NrCpf = paciente.NrCpf;
                getPaciente.NrTelefone = paciente.NrTelefone;
                getPaciente.DsEmail = paciente.DsEmail;
                getPaciente.DtNascimento = paciente.DtNascimento;
                getPaciente.DsSexo = paciente.DsSexo;
                getPaciente.IdPlano = paciente.IdPlano;
                await _context.SaveChangesAsync();
                return getPaciente;
            }
        }
    }
}
