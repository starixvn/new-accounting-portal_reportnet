namespace BetCount.Model;

public class ProductModel
{
    public int Id { get; set; }

    public string Name { get; set; }


    public List<ProductModel> Items { get; set; }
}