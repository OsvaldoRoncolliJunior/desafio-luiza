public class User
{
    public int? user_id { get; set; }
    public string name { get; set; }
    public List<Order> orders { get; set; }
}