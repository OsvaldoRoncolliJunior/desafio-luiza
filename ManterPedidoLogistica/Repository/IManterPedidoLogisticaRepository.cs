public interface IManterPedidoLogisticaRepository
{
    List<User> getAllPedidosUsuarios();

    User getPedidosUsuarioById(int id);

    List<User> getAllPedidosUsuariosByDatas(DateTime dataInicio, DateTime dataFim);

    void postPedidosUsuarioByFile(Stream arquivo);
}