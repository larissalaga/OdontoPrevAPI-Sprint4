using Microsoft.EntityFrameworkCore;
using OdontoPrevAPI.Data;
using OdontoPrevAPI.Dtos;
using OdontoPrevAPI.Models;
using OdontoPrevAPI.Repositories.Interfaces;

namespace OdontoPrevAPI.Repositories.Implementations
{
    public class ExtratoPontosRepository : IExtratoPontosRepository
    {
        private DataContext _context;
        public ExtratoPontosRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Models.ExtratoPontos> Create(ExtratoPontosDtos extratoPontos)
        {   
            // First, validate if the patient exists
            var paciente = await _context.Paciente.FirstOrDefaultAsync(p => p.IdPaciente == extratoPontos.IdPaciente);
            if (paciente == null)
            {
                throw new Exception("Paciente não encontrado.");
            }

            // Format the date for Oracle (yyyy-MM-dd)
            string formattedDate = extratoPontos.DtExtrato.ToString("yyyy-MM-dd");

            // Use the INSERT_EXTRATO_PONTOS procedure from PKG_CRUD_EXTRATO_PONTOS
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                BEGIN
                    PKG_CRUD_EXTRATO_PONTOS.INSERT_EXTRATO_PONTOS(
                        TO_DATE({formattedDate}, 'YYYY-MM-DD'),
                        {extratoPontos.NrNumeroPontos},
                        {extratoPontos.DsMovimentacao},
                        {extratoPontos.IdPaciente}
                    );
                END;
            ");

            // Fetch the newly created ExtratoPontos record (most recent one for this patient)
            var newExtratoPontos = await _context.ExtratoPontos
                .Where(e => e.IdPaciente == extratoPontos.IdPaciente)
                .OrderByDescending(e => e.IdExtratoPontos)
                .FirstOrDefaultAsync();

            if (newExtratoPontos == null)
            {
                throw new Exception("Erro ao recuperar o Extrato de Pontos criado.");
            }

            return newExtratoPontos;            
        }
        
        public async Task<bool> DeleteByIdPacient(int idPaciente)
        {
            var getExtratoPontos = await _context.ExtratoPontos.FirstOrDefaultAsync(x => x.IdPaciente == idPaciente);
            if (getExtratoPontos == null)
            {
                return true;
            }
            else
            {
                _context.ExtratoPontos.Remove(getExtratoPontos);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        
        public async void Delete(int IdExtratoPontos)
        {
            var getExtratoPontos = await _context.ExtratoPontos.FirstOrDefaultAsync(x => x.IdExtratoPontos == IdExtratoPontos);
            if (getExtratoPontos == null)
            {
                throw new Exception("Extrato de pontos não encontrado.");
            }
            else
            {
                // Use the DELETE_EXTRATO_PONTOS procedure from PKG_CRUD_EXTRATO_PONTOS
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    BEGIN
                        PKG_CRUD_EXTRATO_PONTOS.DELETE_EXTRATO_PONTOS({IdExtratoPontos});
                    END;
                ");
            }
        }

        public async Task<List<Models.ExtratoPontos>> GetAll()
        {
            var getExtratoPontos = await _context.ExtratoPontos.ToListAsync();
            if (getExtratoPontos != null)
            {
                return getExtratoPontos;
            }
            else
            {
                throw new Exception("Nenhum extrato de pontos encontrado.");
            }
        }

        public async Task<List<Models.ExtratoPontos>> GetById(int idPaciente)
        {
            var getExtratoPontos = await _context.ExtratoPontos.Where(x => x.IdPaciente == idPaciente).ToListAsync();
            if (getExtratoPontos == null || !getExtratoPontos.Any())
            {
                throw new Exception("Extratos de pontos não encontrados.");
            }
            else
            {
                return getExtratoPontos;
            }
        }
        
        public async Task<int> GetTotalPontosByPacienteId(int idPaciente)
        {
            var totalPontos = await _context.ExtratoPontos
                .Where(x => x.IdPaciente == idPaciente)
                .SumAsync(x => (int?)x.NrNumeroPontos) ?? 0;

            return totalPontos;
        }
        
        public async Task<Models.ExtratoPontos> Update(int IdExtratoPontos, ExtratoPontosDtos extratoPontos)
        {
            var getExtratoPontos = await _context.ExtratoPontos.FirstOrDefaultAsync(x => x.IdExtratoPontos == IdExtratoPontos);
            if (getExtratoPontos == null)
            {
                throw new Exception("Extrato de pontos não encontrado.");
            }
            
            // Format the date for Oracle (yyyy-MM-dd)
            string formattedDate = extratoPontos.DtExtrato.ToString("yyyy-MM-dd");
            
            // Use the UPDATE_EXTRATO_PONTOS procedure from PKG_CRUD_EXTRATO_PONTOS
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                BEGIN
                    PKG_CRUD_EXTRATO_PONTOS.UPDATE_EXTRATO_PONTOS(
                        {extratoPontos.IdPaciente},
                        {IdExtratoPontos},
                        TO_DATE({formattedDate}, 'YYYY-MM-DD'),
                        {extratoPontos.NrNumeroPontos},
                        {extratoPontos.DsMovimentacao}
                    );
                END;
            ");
            
            // Fetch the updated ExtratoPontos record
            var updatedExtratoPontos = await _context.ExtratoPontos.FirstOrDefaultAsync(x => x.IdExtratoPontos == IdExtratoPontos);
            return updatedExtratoPontos;
        }
    }
}
