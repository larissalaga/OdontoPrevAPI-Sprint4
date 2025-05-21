using Microsoft.EntityFrameworkCore;
using OdontoPrevAPI.Data;
using OdontoPrevAPI.Models;
using OdontoPrevAPI.Repositories.Interfaces;

namespace OdontoPrevAPI.Repositories.Implementations
{
    public class PacienteDentistaRepository : IPacienteDentistaRepository
    {
        private DataContext _context;
        public PacienteDentistaRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<PacienteDentista> Create(int idDentista, int idPaciente)
        {
            // Validate that the dentist exists
            var dentista = await _context.Dentista.FirstOrDefaultAsync(d => d.IdDentista == idDentista);
            if (dentista == null)
            {
                throw new Exception("Dentista não encontrado.");
            }

            // Validate that the patient exists
            var paciente = await _context.Paciente.FirstOrDefaultAsync(p => p.IdPaciente == idPaciente);
            if (paciente == null)
            {
                throw new Exception("Paciente não encontrado.");
            }

            // Check if the relationship already exists
            var existingRelationship = await _context.PacienteDentista
                .FirstOrDefaultAsync(pd => pd.IdDentista == idDentista && pd.IdPaciente == idPaciente);
                
            if (existingRelationship != null)
            {
                throw new Exception("Relação entre Paciente e Dentista já existe.");
            }

            // Use the INSERT_PACIENTE_DENTISTA procedure from PKG_CRUD_PACIENTE_DENTISTA
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                BEGIN
                    PKG_CRUD_PACIENTE_DENTISTA.INSERT_PACIENTE_DENTISTA(
                        {idDentista},
                        {idPaciente}
                    );
                END;
            ");

            // Fetch the newly created relationship
            var newRelationship = await _context.PacienteDentista
                .FirstOrDefaultAsync(pd => pd.IdDentista == idDentista && pd.IdPaciente == idPaciente);

            if (newRelationship == null)
            {
                throw new Exception("Erro ao recuperar a relação Paciente-Dentista criada.");
            }

            return newRelationship;
        }

        public async Task<bool> Delete(string dsCro, string nrCpf)
        {
            // Validate inputs
            if (string.IsNullOrEmpty(dsCro))
            {
                throw new Exception("CRO do dentista não informado.");
            }

            if (string.IsNullOrEmpty(nrCpf))
            {
                throw new Exception("CPF do paciente não informado.");
            }

            // Check if dentist with this CRO exists
            var dentista = await _context.Dentista.FirstOrDefaultAsync(d => d.DsCro == dsCro);
            if (dentista == null)
            {
                throw new Exception("Dentista não encontrado com o CRO informado.");
            }

            // Check if patient with this CPF exists
            var paciente = await _context.Paciente.FirstOrDefaultAsync(p => p.NrCpf == nrCpf);
            if (paciente == null)
            {
                throw new Exception("Paciente não encontrado com o CPF informado.");
            }

            // Use the DELETE_PACIENTE_DENTISTA procedure from PKG_CRUD_PACIENTE_DENTISTA
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                BEGIN
                    PKG_CRUD_PACIENTE_DENTISTA.DELETE_PACIENTE_DENTISTA(
                        {dsCro},
                        {nrCpf}
                    );
                END;
            ");

            // Check if the relationship was deleted
            var relationship = await _context.PacienteDentista
                .FirstOrDefaultAsync(pd => pd.IdDentista == dentista.IdDentista && pd.IdPaciente == paciente.IdPaciente);

            return relationship == null;
        }

        public async Task<bool> DeleteByIdPaciente(int id_paciente)
        {
            var getPacienteDentista = await _context.PacienteDentista.FirstOrDefaultAsync(x => x.IdPaciente == id_paciente);
            if (getPacienteDentista == null)
            {
                return true;
            }
            else
            {
                _context.PacienteDentista.Remove(getPacienteDentista);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<List<PacienteDentista>> GetAll()
        {
            var getPacienteDentista = await _context.PacienteDentista.ToListAsync();
            if (getPacienteDentista != null)
            {
                return getPacienteDentista;
            }
            else
            {
                throw new Exception("PacienteDentista não encontrado.");
            }
        }

        public async Task<List<PacienteDentista>> GetByPaciente(int id_paciente)
        {
            var getPacienteDentista = await _context.PacienteDentista.Where(x => x.IdPaciente == id_paciente).ToListAsync();
            if (getPacienteDentista != null)
            {
                return getPacienteDentista;
            }
            else
            {
                throw new Exception("PacienteDentista não encontrado.");
            }
        }

        public async Task<PacienteDentista> GetById(int id_paciente, int id_dentista)
        {
            var getPacienteDentista = await _context.PacienteDentista.FirstOrDefaultAsync(x => x.IdPaciente == id_paciente && x.IdDentista == id_dentista);
            if (getPacienteDentista != null)
            {
                return getPacienteDentista;
            }
            else
            {
                throw new Exception("PacienteDentista não encontrado.");
            }
        }

    }
}
