using Microsoft.EntityFrameworkCore;
using OdontoPrevAPI.Data;
using OdontoPrevAPI.Dtos;
using OdontoPrevAPI.Repositories.Interfaces;

namespace OdontoPrevAPI.Repositories.Implementations
{
    public class AnaliseRaioXRepository : IAnaliseRaioXRepository
    {
        private static AnaliseRaioXRepository _instance;
        private static readonly object _lock = new object();
        private DataContext _context;

        public AnaliseRaioXRepository(DataContext context)
        {
            _context = context;
        }

        public static AnaliseRaioXRepository GetInstance(DataContext context)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new AnaliseRaioXRepository(context);
                    }
                }
            }
            return _instance;
        }

        public async Task<Models.AnaliseRaioX> Create(AnaliseRaioXDtos analiseRaioX)
        {
            var getAnaliseRaioX = await _context.AnaliseRaioX.FirstOrDefaultAsync(x => x.IdRaioX == analiseRaioX.IdRaioX);
            if (getAnaliseRaioX != null)
            {
                throw new Exception("Análise de Raio-X já cadastrada.");
            }
            
            // Get the associated RaioX to validate and get the patient ID
            var raioX = await _context.RaioX.FirstOrDefaultAsync(r => r.IdRaioX == analiseRaioX.IdRaioX);
            if (raioX == null)
            {
                throw new Exception("Raio-X não encontrado.");
            }
            
            // Format the date for Oracle (yyyy-MM-dd)
            string formattedDate = analiseRaioX.DtAnaliseRaioX.ToString("yyyy-MM-dd");

            // Use the INSERT_ANALISE_RAIO_X procedure from PKG_CRUD_ANALISE_RAIO_X
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                BEGIN
                    PKG_CRUD_ANALISE_RAIO_X.INSERT_ANALISE_RAIO_X(
                        {analiseRaioX.DsAnaliseRaioX},
                        TO_DATE({formattedDate}, 'YYYY-MM-DD'),
                        {analiseRaioX.IdRaioX},
                        {raioX.IdPaciente}
                    );
                END;
            ");

            // Fetch the newly created AnaliseRaioX
            var newAnaliseRaioX = await _context.AnaliseRaioX
                .Where(a => a.IdRaioX == analiseRaioX.IdRaioX)
                .OrderByDescending(a => a.IdAnaliseRaioX)
                .FirstOrDefaultAsync();

            if (newAnaliseRaioX == null)
            {
                throw new Exception("Erro ao recuperar a Análise de Raio-X criada.");
            }

            return newAnaliseRaioX;
        }
        
        public async Task<bool> DeleteByIdRaioX(int raioX)
        {
            var getAnaliseRaioX = await _context.AnaliseRaioX.FirstOrDefaultAsync(x => x.IdRaioX == raioX);
            if (getAnaliseRaioX == null)
            {
                return true;
            }
            else
            {
                // Use the DELETE_ANALISE_RAIO_X procedure from PKG_CRUD_ANALISE_RAIO_X
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    BEGIN
                        PKG_CRUD_ANALISE_RAIO_X.DELETE_ANALISE_RAIO_X({getAnaliseRaioX.IdAnaliseRaioX});
                    END;
                ");
                return true;
            }
        }
        
        public async void Delete(int idRaioX)
        {
            var getAnaliseRaioX = await _context.AnaliseRaioX.FirstOrDefaultAsync(x => x.IdRaioX == idRaioX);
            if (getAnaliseRaioX == null)
            {
                throw new Exception("Análise de Raio-X não encontrada.");
            }
            else
            {
                // Use the DELETE_ANALISE_RAIO_X procedure from PKG_CRUD_ANALISE_RAIO_X
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    BEGIN
                        PKG_CRUD_ANALISE_RAIO_X.DELETE_ANALISE_RAIO_X({getAnaliseRaioX.IdAnaliseRaioX});
                    END;
                ");
            }
        }

        public async Task<List<Models.AnaliseRaioX>> GetAll()
        {
            var getAnaliseRaioX = await _context.AnaliseRaioX.ToListAsync();
            if (getAnaliseRaioX != null)
            {
                return getAnaliseRaioX;
            }
            else
            {
                throw new Exception("Nenhuma análise de Raio-X encontrada.");
            }
        }

        public async Task<Models.AnaliseRaioX> GetById(int idRaioX)
        {
            var getAnaliseRaioX = await _context.AnaliseRaioX.FirstOrDefaultAsync(x => x.IdRaioX == idRaioX);
            if (getAnaliseRaioX == null)
                throw new Exception("Análise de Raio-X não encontrada.");
            else
                return getAnaliseRaioX;
        }

        public async Task<Models.AnaliseRaioX> Update(int idRaioX, AnaliseRaioXDtos analiseRaioX)
        {
            var getAnaliseRaioX = await _context.AnaliseRaioX.FirstOrDefaultAsync(x => x.IdRaioX == idRaioX);
            if (getAnaliseRaioX == null)
            {
                throw new Exception("Análise de Raio-X não encontrada.");
            }
            
            // Get the associated RaioX to get the patient ID
            var raioX = await _context.RaioX.FirstOrDefaultAsync(r => r.IdRaioX == idRaioX);
            if (raioX == null)
            {
                throw new Exception("Raio-X não encontrado.");
            }
            
            // Format the date for Oracle (yyyy-MM-dd)
            string formattedDate = analiseRaioX.DtAnaliseRaioX.ToString("yyyy-MM-dd");
            
            // Use the UPDATE_ANALISE_RAIO_X procedure from PKG_CRUD_ANALISE_RAIO_X
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                BEGIN
                    PKG_CRUD_ANALISE_RAIO_X.UPDATE_ANALISE_RAIO_X(
                        {raioX.IdPaciente},
                        {idRaioX},
                        {analiseRaioX.DsAnaliseRaioX},
                        TO_DATE({formattedDate}, 'YYYY-MM-DD')
                    );
                END;
            ");
            
            // Fetch the updated AnaliseRaioX
            var updatedAnaliseRaioX = await _context.AnaliseRaioX.FirstOrDefaultAsync(x => x.IdRaioX == idRaioX);
            return updatedAnaliseRaioX;
        }
    }
}
