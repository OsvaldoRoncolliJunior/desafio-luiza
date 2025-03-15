
public class ManterPedidoLogisticaService : IManterPedidoLogisticaService {

    private IManterPedidoLogisticaRepository _repository;
    public ManterPedidoLogisticaService(IManterPedidoLogisticaRepository repository) {
        this._repository = repository;
    }

    public List<User> getAllPedidosUsuarios() {

        return _repository.getAllPedidosUsuarios();
    }

    public User getPedidosUsuarioById(int id) {

        return _repository.getPedidosUsuarioById(id);
    }

    public List<User> getAllPedidosUsuariosByDatas(DateTime dataInicio, DateTime dataFim) {

        return _repository.getAllPedidosUsuariosByDatas(dataInicio, dataFim);
    }

    public void postPedidosUsuarioByFile(Stream arquivo)
    {
        try
        {
            _repository.postPedidosUsuarioByFile(arquivo);
        }
        catch (System.Exception ex)
        {
            Console.Write("Erro na converso do arquivo");
            Console.Write(ex.Message);
            throw;
        }
        
    }
}