using Microsoft.EntityFrameworkCore;
using OdontoPrevAPI.Data;
using OdontoPrevAPI.Dtos;
using OdontoPrevAPI.Repositories.Interfaces;

namespace OdontoPrevAPI.Repositories.Implementations
{
    public class DentistaRepository : IDentistaRepository
    {
        private DataContext _context;
        public DentistaRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Models.Dentista> Create(DentistaDtos dentista)
        {
            var getDentista = await _context.Dentista.FirstOrDefaultAsync(x => x.DsCro == dentista.DsCro);
            if (getDentista != null) {
                throw new Exception("Dentista já cadastrado.");
            }
            else
            {
                // Use the INSERT_DENTISTA procedure from PKG_CRUD_DENTISTA
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    BEGIN
                        PKG_CRUD_DENTISTA.INSERT_DENTISTA(
                            {dentista.NmDentista},
                            {dentista.DsCro},
                            {dentista.DsEmail},
                            {dentista.NrTelefone},
                            {dentista.DsDocIdentificacao}
                        );
                    END;
                ");

                // Fetch the newly created dentist
                var newDentista = await _context.Dentista.FirstOrDefaultAsync(x => x.DsCro == dentista.DsCro);
                return newDentista;
            }
        }

        public async Task<bool> DeleteByCRO(string dsCro)
        {
            var getDentista = await _context.Dentista.FirstOrDefaultAsync(x => x.DsCro == dsCro);
            if (getDentista == null)
            {
                return false;
            }
            else
            {
                // Use the DELETE_DENTISTA procedure from PKG_CRUD_DENTISTA
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    BEGIN
                        PKG_CRUD_DENTISTA.DELETE_DENTISTA({dsCro});
                    END;
                ");
                return true;
            }
        }

        public async Task<bool> DeleteById(int id)
        {
            var getDentista = await _context.Dentista.FirstOrDefaultAsync(x => x.IdDentista == id);
            if (getDentista == null)
            {
                return false;
            }
            else
            {
                _context.Dentista.Remove(getDentista);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<Models.Dentista>> GetAll()
        {
            var getDentista = await _context.Dentista.ToListAsync();
            if (getDentista != null)
            {
                return getDentista;
            }
            else
            {
                throw new Exception("Nenhum dentista encontrado.");
            }
        }

        public async Task<Models.Dentista> GetById(int id)
        {
            var getDentista = await _context.Dentista.FirstOrDefaultAsync(x => x.IdDentista == id);
            if (getDentista == null)
            {
                throw new Exception("Dentista não encontrado.");
            }
            else
            {
                return getDentista;
            }
        }

        public async Task<List<Models.Dentista>> GetByIdList(List<int> id)
        {
            var getDentista = await _context.Dentista.Where(x => id.Contains(x.IdDentista)).ToListAsync();
            if (getDentista == null)
            {
                throw new Exception("Dentista não encontrado.");
            }
            else
            {
                return getDentista;
            }
        }

        public async Task<Models.Dentista> GetByDsCro(string dsCro)
        {
            var getDentista = await _context.Dentista.FirstOrDefaultAsync(x => x.DsCro == dsCro);
            if (getDentista == null)
            {
                throw new Exception("Dentista não encontrado.");
            }
            else
            {
                return getDentista;
            }
        }

        public async Task<Models.Dentista> UpdateByCRO(string dsCro, DentistaDtos dentista)
        {
            var getDentista = await _context.Dentista.FirstOrDefaultAsync(x => x.DsCro == dsCro);
            if (getDentista == null)
            {
                throw new Exception("Dentista não encontrado.");
            }
            else
            {
                // Use the UPDATE_DENTISTA procedure from PKG_CRUD_DENTISTA
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    BEGIN
                        PKG_CRUD_DENTISTA.UPDATE_DENTISTA(
                            {dentista.NmDentista},
                            {dentista.DsCro},
                            {dentista.DsEmail},
                            {dentista.NrTelefone},
                            {dentista.DsDocIdentificacao}
                        );
                    END;
                ");

                // Fetch the updated dentist
                var updatedDentista = await _context.Dentista.FirstOrDefaultAsync(x => x.DsCro == (string.IsNullOrEmpty(dentista.DsCro) ? dsCro : dentista.DsCro));
                return updatedDentista;
            }
        }

        public async Task<Models.Dentista> UpdateById(int id, DentistaDtos dentista)
        {
            var getDentista = await _context.Dentista.FirstOrDefaultAsync(x => x.IdDentista == id);
            if (getDentista == null)
            {
                throw new Exception("Dentista não encontrado.");
            }
            else
            {
                getDentista.NmDentista = dentista.NmDentista;
                getDentista.DsCro = dentista.DsCro;
                getDentista.NrTelefone = dentista.NrTelefone;
                getDentista.DsEmail = dentista.DsEmail;
                getDentista.DsDocIdentificacao = dentista.DsDocIdentificacao;
                await _context.SaveChangesAsync();
                return getDentista;
            }
        }
    }
}
