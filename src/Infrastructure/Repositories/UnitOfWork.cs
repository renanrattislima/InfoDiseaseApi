namespace Infrastructure.Repositories
{
    using Domain.Interfaces;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextClass _dbContext;

        public IRelatorioRepository Relatorios { get; }
        public ISolicitanteRepository Solicitantes { get; }

        public IDadoEpidemiologicoRepository DadoEpidemiologicos { get; }

        public UnitOfWork(DbContextClass dbContext,
                          IRelatorioRepository relatorioRepository,
                          ISolicitanteRepository solicitanteRepository,
                          IDadoEpidemiologicoRepository dadoEpidemiologicos)
        {
            _dbContext = dbContext;
            Relatorios = relatorioRepository;
            Solicitantes = solicitanteRepository;
            DadoEpidemiologicos = dadoEpidemiologicos;
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
