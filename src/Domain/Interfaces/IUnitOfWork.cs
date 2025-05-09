namespace Domain.Interfaces
{
    using System;

    public interface IUnitOfWork : IDisposable
    {
        IRelatorioRepository Relatorios { get; }
        ISolicitanteRepository Solicitantes { get; }
        IDadoEpidemiologicoRepository DadoEpidemiologicos { get; }
        int Save();
    }
}
