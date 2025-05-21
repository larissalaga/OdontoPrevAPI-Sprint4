using Microsoft.EntityFrameworkCore;
using OdontoPrevAPI.Data;
using OdontoPrevAPI.Dtos;
using OdontoPrevAPI.Models;
using OdontoPrevAPI.Repositories.Interfaces;

namespace OdontoPrevAPI.Repositories.Implementations
{
    public class PlanoRepository : IPlanoRepository
    {
        private DataContext _context;

        public PlanoRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Models.Plano> Create(PlanoDtos plano)
        {
            var getPlano = await _context.Plano.FirstOrDefaultAsync(x => x.DsCodigoPlano == plano.DsCodigoPlano);
            if (getPlano != null)
            {
                throw new Exception("Código de plano já registrado.");
            }
            else
            {
                // Use the INSERT_PLANO procedure from PKG_CRUD_PLANO
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    BEGIN
                        PKG_CRUD_PLANO.INSERT_PLANO(
                            {plano.DsCodigoPlano},
                            {plano.NmPlano}
                        );
                    END;
                ");

                // Fetch the newly created plan
                var newPlano = await _context.Plano.FirstOrDefaultAsync(x => x.DsCodigoPlano == plano.DsCodigoPlano);
                return newPlano;
            }
        }

        public async Task<bool> DeleteById(int idPlano)
        {
            var getPlano = await _context.Plano.FirstOrDefaultAsync(x => x.IdPlano == idPlano);
            if (getPlano == null)
            {
                return false;
            }
            else
            {
                _context.Plano.Remove(getPlano);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DeleteByCodigoPlano(string dsCodigoPlano)
        {
            var getPlano = await _context.Plano.FirstOrDefaultAsync(x => x.DsCodigoPlano == dsCodigoPlano);
            if (getPlano == null)
            {
                return false;
            }
            else
            {
                // Use the DELETE_PLANO procedure from PKG_CRUD_PLANO
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    BEGIN
                        PKG_CRUD_PLANO.DELETE_PLANO({dsCodigoPlano});
                    END;
                ");
                return true;
            }
        }

        public async Task<List<Models.Plano>> GetAll()
        {
            var getPlano = await _context.Plano.ToListAsync();
            if (getPlano != null)
            {
                return getPlano;
            }
            else
            {
                throw new Exception("Nenhum plano encontrado.");
            }
        }

        public async Task<Models.Plano> GetById(int idPlano)
        {
            var getPlano = await _context.Plano.FirstOrDefaultAsync(x => x.IdPlano == idPlano);
            if (getPlano == null)
            {
                throw new Exception("Plano não encontrado.");
            }
            return getPlano;
        }
        public async Task<Models.Plano> GetByDsCodigoPlano(string dsCodigoPlano)
        {
            var getPlano = await _context.Plano.FirstOrDefaultAsync(x => x.DsCodigoPlano == dsCodigoPlano);
            if (getPlano == null)
            {
                throw new Exception("Plano não encontrado.");
            }
            return getPlano;
        }
        public async Task<Models.Plano> Update(int idPlano, PlanoDtos plano)
        {
            var getPlano = await _context.Plano.FirstOrDefaultAsync(x => x.IdPlano == idPlano);
            if (getPlano == null)
            {
                throw new Exception("Plano não encontrado.");
            }
            else
            {
                getPlano.NmPlano = plano.NmPlano;
                getPlano.DsCodigoPlano = plano.DsCodigoPlano;
                await _context.SaveChangesAsync();
                return getPlano;
            }
        }

        public async Task<Models.Plano> UpdateByCodigo(string dsCodigoPlano, string nmPlano)
        {
            var getPlano = await _context.Plano.FirstOrDefaultAsync(x => x.DsCodigoPlano == dsCodigoPlano);
            if (getPlano == null)
            {
                throw new Exception("Plano não encontrado.");
            }
            else
            {
                // Use the UPDATE_PLANO procedure from PKG_CRUD_PLANO
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    BEGIN
                        PKG_CRUD_PLANO.UPDATE_PLANO(
                            {nmPlano},
                            {dsCodigoPlano}
                        );
                    END;
                ");

                // Fetch the updated plan
                var updatedPlano = await _context.Plano.FirstOrDefaultAsync(x => x.DsCodigoPlano == dsCodigoPlano);
                return updatedPlano;
            }
        }
    }
}
