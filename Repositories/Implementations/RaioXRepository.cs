using Microsoft.EntityFrameworkCore;
using OdontoPrevAPI.Data;
using OdontoPrevAPI.Dtos;
using OdontoPrevAPI.Models;
using OdontoPrevAPI.Repositories.Interfaces;
using System.Reflection.Metadata;

namespace OdontoPrevAPI.Repositories.Implementations
{
    public class RaioXRepository : IRaioXRepository
    {
        private DataContext _context;

        public RaioXRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Models.RaioX> Create(RaioXDtos raioX)
        {
            // First, validate if the patient exists
            var paciente = await _context.Paciente.FirstOrDefaultAsync(p => p.IdPaciente == raioX.IdPaciente);
            if (paciente == null)
            {
                throw new Exception("Paciente não encontrado.");
            }

            // Format the date for Oracle (yyyy-MM-dd)
            string formattedDate = raioX.DtDataRaioX.ToString("yyyy-MM-dd");

            // Use the INSERT_RAIO_X procedure from PKG_CRUD_RAIO_X
            // NULL temporario para a Imagem do RaioX
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                BEGIN
                    PKG_CRUD_RAIO_X.INSERT_RAIO_X(
                        {raioX.DsRaioX},
                        NULL, 
                        TO_DATE({formattedDate}, 'YYYY-MM-DD'),
                        {raioX.IdPaciente}
                    );
                END;
            ");

            // Fetch the newly created RaioX record (most recent one for this patient)
            var newRaioX = await _context.RaioX
                .Where(r => r.IdPaciente == raioX.IdPaciente)
                .OrderByDescending(r => r.IdRaioX)
                .FirstOrDefaultAsync();

            if (newRaioX == null)
            {
                throw new Exception("Erro ao recuperar o Raio-X criado.");
            }

            return newRaioX;
        }

        public async void Delete(int idRaioX)
        {
            var getRaioX = await _context.RaioX.FirstOrDefaultAsync(x => x.IdRaioX == idRaioX);
            if (getRaioX == null)
            {
                throw new Exception("RaioX não encontrado.");
            }
            else
            {
                // Use the DELETE_RAIO_X procedure from PKG_CRUD_RAIO_X
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    BEGIN
                        PKG_CRUD_RAIO_X.DELETE_RAIO_X({idRaioX});
                    END;
                ");
            }
        }

        public async Task<bool> DeleteByIdPaciente(int idPaciente)
        {
            var getRaioX = await _context.RaioX.FirstOrDefaultAsync(x => x.IdPaciente == idPaciente);
            if (getRaioX == null)
            {
                return true;
            }
            else
            {
                // Use the DELETE_RAIO_X procedure for each raio-x of this patient
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    BEGIN
                        PKG_CRUD_RAIO_X.DELETE_RAIO_X({getRaioX.IdRaioX});
                    END;
                ");
                return true;
            }
        }
        
        public async Task<List<Models.RaioX>> GetByIdPaciente(int idPaciente)
        {
            var getRaioX = await _context.RaioX.Where(x => x.IdPaciente == idPaciente).ToListAsync();
            if (getRaioX == null || !getRaioX.Any())
            {
                throw new Exception("RaioX não encontrado.");
            }
            else
            {
                return getRaioX;
            }
        }
        
        public async Task<List<Models.RaioX>> GetAll()
        {
            var getRaioX = await _context.RaioX.ToListAsync();
            if (getRaioX != null)
            {
                return getRaioX;
            }
            else
            {
                throw new Exception("Nenhum raioX encontrado.");
            }
        }

        public async Task<Models.RaioX> GetById(int idRaioX)
        {
            var getRaioX = await _context.RaioX.FirstOrDefaultAsync(x => x.IdRaioX == idRaioX);
            if (getRaioX == null)
            {
                throw new Exception("RaioX não encontrado.");
            }
            else
            {
                return getRaioX;
            }
        }

        public async Task<Models.RaioX> Update(int idRaioX, RaioXDtos raioX)
        {
            var getRaioX = await _context.RaioX.FirstOrDefaultAsync(x => x.IdRaioX == idRaioX);
            if (getRaioX == null)
            {
                throw new Exception("RaioX não encontrado.");
            }
            
            // Format the date for Oracle (yyyy-MM-dd)
            string formattedDate = raioX.DtDataRaioX.ToString("yyyy-MM-dd");
            
            // Use the UPDATE_RAIO_X procedure from PKG_CRUD_RAIO_X
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                BEGIN
                    PKG_CRUD_RAIO_X.UPDATE_RAIO_X(
                        {raioX.IdPaciente},
                        {idRaioX},
                        {raioX.DsRaioX},
                        {raioX.ImRaioX},
                        TO_DATE({formattedDate}, 'YYYY-MM-DD')
                    );
                END;
            ");
            
            // Fetch the updated RaioX record
            var updatedRaioX = await _context.RaioX.FirstOrDefaultAsync(x => x.IdRaioX == idRaioX);
            return updatedRaioX;
        }
    }
}
