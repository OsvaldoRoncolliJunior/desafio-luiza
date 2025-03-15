
using System.Globalization;

public class ManterPedidoLogisticaRepository : IManterPedidoLogisticaRepository {

    static List<User> users = [];
    public List<User> getAllPedidosUsuarios() {

        return users; //GetUsers();
    }

    public User getPedidosUsuarioById(int id) {

        //List<User> users = GetUsers();
        return users.Where(x => x.user_id == id).FirstOrDefault(new User());
    }

    public List<User> getAllPedidosUsuariosByDatas(DateTime dataInicio, DateTime dataFim) {

        //List<User> users = GetUsers();
       /* return users.Where(x => x.orders.Any(y => y.date >= dataInicio && y.date <= dataFim))  // Ensure the user has matching orders
        .Select(x => new 
        {
            User = x,
            Orders = x.orders.Where(y => y.date >= dataInicio && y.date <= dataFim).ToList()  // Filter orders for the user
        })
        .ToList();*/

        foreach (var user in users)
        {
            // Filtra as order conforme a data para cada usuario
            user.orders = user.orders
                .Where(y => y.date >= dataInicio && y.date <= dataFim) 
                .ToList();
        }

         // retorona dos usario que possuirem ordem apos o filtro
        return users.Where(x => x.orders.Any()).ToList();
    }

    private List<User> GetUsers() {
        List<User> users = [];
        Product product1 = new(){
            product_id=111,
            value=512.24M
        };
        Product product2 = new(){
            product_id=122,
            value=512.24M
        };
        List<Product> products = [product1, product2];
        List<Order> orders1 = [];
        orders1.Add(new Order() {
            order_id = 123,
            total = 1024.48M,
            date = new DateTime(2021,12,01),
            products = products
        });

        User user1 = new User(){user_id = 1, name= "Zarelli", orders = orders1};
        
        
        product1 = new(){
            product_id=111,
            value=256.24M
        };
        product2 = new(){
            product_id=122,
            value=256.24M
        };
        products = [product1, product2];
        List<Order> orders2 = [];
        orders2.Add(new Order() {
            order_id = 123,
            total = 512.48M,
            date = new DateTime(2026,12,01),
            products = products
        });

        User user2 = new User(){user_id = 2, name= "Medeiros", orders = orders2};
        
        users.Add(user1);
        users.Add(user2);

        return users;
    }

    public void postPedidosUsuarioByFile(Stream arquivo)
    {
        using (var stream = new StreamReader(arquivo, System.Text.Encoding.UTF8))
        {
            // Limpa a lista estática antes de adicionar novos itens
            //users.Clear();

            string line;
            // Lê cada linha do arquivo e adiciona na lista estática
            while ((line = stream.ReadLine()) != null)
            {
                if (line.Length >= 95) {
                    string codUser = line.Substring(0, 10);
                    string nomeUser = line.Substring(10, 45).TrimStart();
                    string codPedido = line.Substring(55, 10);
                    string codProduto = line.Substring(65, 10);
                    string vlProduto = line.Substring(75, 12).TrimStart();
                    string dtCompra = line.Substring(87, 8);

                    int idUser;
                    int idPedido;
                    int idProduto;
                    DateTime dataCompra;

                    int.TryParse(codUser, out idUser);
                    int.TryParse(codPedido, out idPedido);
                    int.TryParse(codProduto, out idProduto);
                    decimal valorProduto = Decimal.Parse(vlProduto,  new CultureInfo("en-US"));
                    dataCompra = DateTime.ParseExact(dtCompra, "yyyyMMdd", CultureInfo.InvariantCulture);

                    User user = users.Where(x => x.user_id.Equals(idUser)).FirstOrDefault(new User());
                    if (user.user_id == null) {
                        adicionaItemLista(idUser, nomeUser, idPedido, idProduto, valorProduto, dataCompra);
                    } else {
                        Order order = user.orders.Where(x => x.order_id.Equals(idPedido)).
                            FirstOrDefault(new Order());
                        if (order.order_id == null) {
                             Product product = new Product(){
                                product_id = idProduto,
                                value = valorProduto
                            };
                            Order orderItem = new Order() {
                                order_id = idPedido,
                                date = dataCompra,
                                total = valorProduto,
                                products = new List<Product>()
                            };
                            orderItem.products.Add(product);
                            user.orders.Add(orderItem);
                        } else {
                            Product product = order.products.Where(x => x.product_id.Equals(idProduto)).
                                FirstOrDefault(new Product());
                            if (product.product_id == null) {
                                order.date = dataCompra;
                                order.total = order.total + valorProduto;
                                Product productItem = new Product(){
                                    product_id = idProduto,
                                    value = valorProduto
                                };
                                order.products.Add(productItem);
                            }
                        }
                    }

                } else {
                    Console.WriteLine("Tamanho da linha invalido!");
                }
                
            }
        }
    }

    private void adicionaItemLista(int idUser, string nomeUser, int idPedido, int idProduto,
        decimal valorProduto, DateTime dataCompra) {

        Product product = new(){
            product_id=idProduto,
            value=valorProduto
        };
      
        List<Product> products = [product];
        List<Order> orders = [];
        orders.Add(new Order() {
            order_id = idPedido,
            total = valorProduto,
            date = dataCompra,
            products = products
        });

        User user = new User(){user_id = idUser, name = nomeUser, orders = orders};

        users.Add(user);
    }
}