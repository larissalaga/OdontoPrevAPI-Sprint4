using Microsoft.EntityFrameworkCore;
using OdontoPrevAPI.Models;

namespace OdontoPrevAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        public DbSet<AnaliseRaioX> AnaliseRaioX { get; set; }
        public DbSet<CheckIn> CheckIn { get; set; }
        public DbSet<Dentista> Dentista { get; set; }
        public DbSet<ExtratoPontos> ExtratoPontos { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Perguntas> Perguntas { get; set; }
        public DbSet<Plano> Plano { get; set; }
        public DbSet<RaioX> RaioX { get; set; }
        public DbSet<Respostas> Respostas { get; set; }
        public DbSet<PacienteDentista> PacienteDentista { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence("SEQ_T_OPBD_PLANO").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<Plano>()
                .Property(p => p.IdPlano)
                .HasDefaultValueSql("SEQ_T_OPBD_PLANO.NEXTVAL");

            modelBuilder.HasSequence("SEQ_T_OPBD_DENTISTA").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<Dentista>()
                .Property(p => p.IdDentista)
                .HasDefaultValueSql("SEQ_T_OPBD_DENTISTA.NEXTVAL");

            modelBuilder.HasSequence("SEQ_T_OPBD_PERGUNTAS").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<Perguntas>()
                .Property(p => p.IdPergunta)
                .HasDefaultValueSql("SEQ_T_OPBD_PERGUNTAS.NEXTVAL");

            modelBuilder.HasSequence("SEQ_T_OPBD_PACIENTE").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<Paciente>()
                .Property(p => p.IdPaciente)
                .HasDefaultValueSql("SEQ_T_OPBD_PACIENTE.NEXTVAL")
                .ValueGeneratedOnAdd();

            modelBuilder.HasSequence("SEQ_T_OPBD_EXTRATO_PONTOS").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<ExtratoPontos>()
                .Property(p => p.IdExtratoPontos)
                .HasDefaultValueSql("SEQ_T_OPBD_EXTRATO_PONTOS.NEXTVAL");

            modelBuilder.HasSequence("SEQ_T_OPBD_RESPOSTAS").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<Respostas>()
                .Property(p => p.IdResposta)
                .HasDefaultValueSql("SEQ_T_OPBD_RESPOSTAS.NEXTVAL");

            modelBuilder.HasSequence("SEQ_T_OPBD_CHECK_IN").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<CheckIn>()
                .Property(p => p.IdCheckIn)
                .HasDefaultValueSql("SEQ_T_OPBD_CHECK_IN.NEXTVAL");

            modelBuilder.HasSequence("SEQ_T_OPBD_RAIO_X").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<RaioX>()
                .Property(p => p.IdRaioX)
                .HasDefaultValueSql("SEQ_T_OPBD_RAIO_X.NEXTVAL");

            modelBuilder.HasSequence("SEQ_T_OPBD_ANALISE_RAIO_X").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<AnaliseRaioX>()
                .Property(p => p.IdAnaliseRaioX)
                .HasDefaultValueSql("SEQ_T_OPBD_ANALISE_RAIO_X.NEXTVAL");



            base.OnModelCreating(modelBuilder);
        }

    }

}

