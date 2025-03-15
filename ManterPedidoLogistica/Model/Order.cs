public class Order
{
    public int? order_id { get; set; }
    public decimal total { get; set; }
    public DateTime date { get; set; }
    public List<Product> products { get; set; }
}
